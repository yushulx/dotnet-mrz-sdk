using DynamsoftCore;
using Com.Dynamsoft.Dlr;
using MRZRecognizer;
using System.Runtime.InteropServices;
using Foundation;

namespace Dynamsoft
{
    public class MrzScanner
    {
        private DynamsoftMRZRecognizer? recognizer;
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

        public class LicenseVerification : LicenseVerificationListener
        {
            public override void LicenseVerificationCallback(bool isSuccess, NSError error)
            {
                if (!isSuccess)
                {
                    System.Console.WriteLine(error.UserInfo);
                }
            }
        }

        public static void InitLicense(string license, object? context = null)
        {
            DynamsoftLicenseManager.InitLicense(license, new LicenseVerification());
        }

        private MrzScanner()
        {
            recognizer = new DynamsoftMRZRecognizer();
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
            return DynamsoftLabelRecognizer.Version;
        }

        public Result[]? DetectFile(string filename)
        {
            if (recognizer == null) return null;

            NSError error;
            iDLRResult[]? mrzResult = recognizer.RecognizeMrzFile(filename, out error);
            return GetResults(mrzResult);
        }

        public Result[]? DetectBuffer(byte[] buffer, int width, int height, int stride, ImagePixelFormat format)
        {
            if (recognizer == null) return null;

            NSData converted = NSData.FromArray(buffer);

            iImageData imageData = new iImageData()
            {
                Bytes = converted,
                Width = width,
                Height = height,
                Stride = stride,
                Format = (EnumImagePixelFormat)format,
            };
            NSError error;
            iDLRResult[]? mrzResult = recognizer.RecognizeMrzBuffer(imageData, out error);

            return GetResults(mrzResult);
        }

        private Result[]? GetResults(iDLRResult[]? mrzResult)
        {
            if (mrzResult != null && mrzResult[0].LineResults != null)
            {
                iDLRLineResult[] lines = mrzResult[0].LineResults.ToArray();
                Result[] result = new Result[lines.Length];
                for (int i = 0; i < lines.Length; i++)
                {
                    result[i] = new Result()
                    {
                        Confidence = (int)lines[i].Confidence,
                        Text = lines[i].Text ?? "",
                        Points = new int[8]
                        {
                             (int)((NSValue)lines[i].Location.Points[0]).CGPointValue.X,
                             (int)((NSValue)lines[i].Location.Points[0]).CGPointValue.Y,
                             (int)((NSValue)lines[i].Location.Points[1]).CGPointValue.X,
                             (int)((NSValue)lines[i].Location.Points[1]).CGPointValue.Y,
                             (int)((NSValue)lines[i].Location.Points[2]).CGPointValue.X,
                             (int)((NSValue)lines[i].Location.Points[2]).CGPointValue.Y,
                             (int)((NSValue)lines[i].Location.Points[3]).CGPointValue.X,
                             (int)((NSValue)lines[i].Location.Points[3]).CGPointValue.Y
                        }
                    };
                }

                return result;
            }

            return null;
        }
    }
}


