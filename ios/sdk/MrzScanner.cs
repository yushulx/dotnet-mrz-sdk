using Com.Dynamsoft.Core;
using Com.Dynamsoft.Dlr;

namespace Dynamsoft
{
    public class MrzScanner
    {
        private MRZRecognizer? recognizer;
        public class Result
        {
            public int Confidence { get; set; }
            public string Text { get; set; }
            public int[] Points { get; set; }

            public Result()
            {
                Points = new int[8];
                Text = "";
            }
        }

        public enum ImagePixelFormat
        {
            IPF_BINARY,

            /**0:White, 1:Black */
            IPF_BINARYINVERTED,

            /**8bit gray */
            IPF_GRAYSCALED,

            /**NV21 */
            IPF_NV21,

            /**16bit with RGB channel order stored in memory from high to low address*/
            IPF_RGB_565,

            /**16bit with RGB channel order stored in memory from high to low address*/
            IPF_RGB_555,

            /**24bit with RGB channel order stored in memory from high to low address*/
            IPF_RGB_888,

            /**32bit with ARGB channel order stored in memory from high to low address*/
            IPF_ARGB_8888,

            /**48bit with RGB channel order stored in memory from high to low address*/
            IPF_RGB_161616,

            /**64bit with ARGB channel order stored in memory from high to low address*/
            IPF_ARGB_16161616,

            /**32bit with ABGR channel order stored in memory from high to low address*/
            IPF_ABGR_8888,

            /**64bit with ABGR channel order stored in memory from high to low address*/
            IPF_ABGR_16161616,

            /**24bit with BGR channel order stored in memory from high to low address*/
            IPF_BGR_888
        }

        public class LicenseVerificationListener : Java.Lang.Object, ILicenseVerificationListener
        {
            public void LicenseVerificationCallback(bool isSuccess, CoreException ex)
            {
                if (!isSuccess)
                {
                    throw new Exception(ex.ToString());
                }
            }
        }

        public static void InitLicense(string license, object? context = null)
        {
            if (context == null) { return; }

            LicenseManager.InitLicense(license, (Android.Content.Context)context, new LicenseVerificationListener());
        }

        private MrzScanner()
        {
            recognizer = new MRZRecognizer();
        }

        public static MrzScanner Create()
        {
            return new MrzScanner();
        }

        ~MrzScanner()
        {
            Destroy();
        }

        public void Destroy()
        {
            recognizer = null;
        }

        public static string? GetVersionInfo()
        {
            return MRZRecognizer.Version;
        }

        public Result[]? DetectFile(string filename)
        {
            if (recognizer == null) return null;

            DLRResult[]? mrzResult = recognizer.RecognizeFile(filename);
            return GetResults(mrzResult);
        }

        public Result[]? DetectBuffer(byte[] buffer, int width, int height, int stride, ImagePixelFormat format)
        {
            if (recognizer == null) return null;

            ImageData imageData = new ImageData()
            {
                Bytes = buffer,
                Width = width,
                Height = height,
                Stride = stride,
                Format = (int)format,
            };
            DLRResult[]? mrzResult = recognizer.RecognizeBuffer(imageData);
            return GetResults(mrzResult);
        }

        private Result[]? GetResults(DLRResult[]? mrzResult)
        {
            if (mrzResult != null && mrzResult[0].LineResults != null)
            {
                DLRLineResult[] lines = mrzResult[0].LineResults.ToArray();
                Result[] result = new Result[lines.Length];
                for (int i = 0; i < lines.Length; i++)
                {
                    result[i] = new Result()
                    {
                        Confidence = lines[i].Confidence,
                        Text = lines[i].Text ?? "",
                        Points = new int[8]
                        {
                            lines[i].Location.Points[0].X,
                            lines[i].Location.Points[0].Y,
                            lines[i].Location.Points[1].X,
                            lines[i].Location.Points[1].Y,
                            lines[i].Location.Points[2].X,
                            lines[i].Location.Points[2].Y,
                            lines[i].Location.Points[3].X,
                            lines[i].Location.Points[3].Y
                        }
                    };
                }

                return result;
            }

            return null;
        }
    }
}


