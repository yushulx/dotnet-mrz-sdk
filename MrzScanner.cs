namespace Dynamsoft;

using System.Text;
using System.Runtime.InteropServices;
using System.Text.Json;
using System.Text.Json.Nodes;

public class MrzScanner
{
    public class Result
    {
        public int Confidence { get; set; }
        public string Text { get; set; }
        public int[] Points { get; set; }

        public Result()
        {
            Points = new int[4];
            Text = "";
        }
    }

    private IntPtr handler;
    private static string? licenseKey;

#if _WINDOWS
    [DllImport("DynamsoftLabelRecognizerx64")]
    static extern int DLR_InitLicense(string license, [Out] byte[] errorMsg, int errorMsgSize);

    [DllImport("DynamsoftLabelRecognizerx64")]
    static extern IntPtr DLR_CreateInstance();

    [DllImport("DynamsoftLabelRecognizerx64")]
    static extern void DLR_DestroyInstance(IntPtr handler);

    [DllImport("DynamsoftLabelRecognizerx64")]
    static extern IntPtr DLR_GetVersion();

    [DllImport("DynamsoftLabelRecognizerx64")]
    static extern int DLR_FreeResults(ref IntPtr pDLR_ResultArray);

    [DllImport("DynamsoftLabelRecognizerx64")]
    static extern int DLR_GetAllResults(IntPtr hBarcode, ref IntPtr pDLR_ResultArray);

    [DllImport("DynamsoftLabelRecognizerx64")]
    static extern int DLR_RecognizeByFile(IntPtr handler, string sourceFilePath, string templateName);

    [DllImport("DynamsoftLabelRecognizerx64")]
    static extern int DLR_RecognizeByBuffer(IntPtr handler, IntPtr sourceImage, string templateName);

    [DllImport("DynamsoftLabelRecognizerx64")]
    static extern int DLR_AppendSettingsFromFile(IntPtr handler, string filename, [Out] byte[] errorMsg, int errorMsgSize);
#else
    [DllImport("DynamsoftLabelRecognizer")]
    static extern int DLR_InitLicense(string license, [Out] byte[] errorMsg, int errorMsgSize);

    [DllImport("DynamsoftLabelRecognizer")]
    static extern IntPtr DLR_CreateInstance();

    [DllImport("DynamsoftLabelRecognizer")]
    static extern void DLR_DestroyInstance(IntPtr handler);

    [DllImport("DynamsoftLabelRecognizer")]
    static extern IntPtr DLR_GetVersion();

    [DllImport("DynamsoftLabelRecognizer")]
    static extern int DLR_FreeResults(ref IntPtr pDLR_ResultArray);

    [DllImport("DynamsoftLabelRecognizerx64")]
    static extern int DLR_GetAllResults(IntPtr hBarcode, ref IntPtr pDLR_ResultArray);

    [DllImport("DynamsoftLabelRecognizer")]
    static extern int DLR_RecognizeByFile(IntPtr handler, string sourceFilePath, string templateName);

    [DllImport("DynamsoftLabelRecognizer")]
    static extern int DLR_RecognizeByBuffer(IntPtr handler, IntPtr sourceImage, string templateName);

    [DllImport("DynamsoftLabelRecognizer")]
    static extern int DLR_AppendSettingsFromFile(IntPtr handler, string filename, [Out] byte[] errorMsg, int errorMsgSize);
#endif

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    internal struct DLR_ResultArray
    {
        public IntPtr results;

        public int resultsCount;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    internal struct Quadrilateral
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public DM_Point[] points;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    internal struct DM_Point
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
        public int[] coordinate;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    internal struct DLR_CharacterResult
    {
        public char characterH;

        public char characterM;

        public char characterL;

        public Quadrilateral location;

        public int characterHConfidence;

        public int characterMConfidence;

        public int characterLConfidence;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
        char[] reserved;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    internal struct DLR_LineResult
    {
        public string lineSpecificationName;

        public string text;

        public string characterModelName;

        public Quadrilateral location;

        public int confidence;

        public int characterResultsCount;

        public IntPtr characterResults;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 64)]
        public char[] reserved;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    internal struct DLR_Result
    {
        public string referenceRegionName;

        public string textAreaName;

        public Quadrilateral location;

        public int confidence;

        public int lineResultsCount;

        public IntPtr lineResults;

        public int pageNumber;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 60)]
        public char[] reserved;
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

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    internal struct ImageData
    {
        public int bytesLength;

        public IntPtr bytes;

        public int width;

        public int height;

        public int stride;

        public ImagePixelFormat format;
    }

    private MrzScanner()
    {
        handler = DLR_CreateInstance();
    }

    ~MrzScanner()
    {
        Destroy();
    }

    public static string? GetVersionInfo()
    {
        return Marshal.PtrToStringUTF8(DLR_GetVersion());
    }

    public static MrzScanner Create()
    {
        if (licenseKey == null)
        {
            throw new Exception("Please call InitLicense first.");
        }
        return new MrzScanner();
    }

    public void Destroy()
    {
        if (handler != IntPtr.Zero)
        {
            DLR_DestroyInstance(handler);
            handler = IntPtr.Zero;
        }
    }

    public static int InitLicense(string license)
    {
        byte[] errorMsg = new byte[512];
        licenseKey = license;
        int ret = DLR_InitLicense(license, errorMsg, 512);
#if DEBUG
        Console.WriteLine("InitLicense(): " + Encoding.ASCII.GetString(errorMsg));
#endif
        return ret;
    }

    public int LoadModel()
    {
        if (handler == IntPtr.Zero) return -1;

        string dir = Directory.GetCurrentDirectory();
        string[] files = Directory.GetDirectories(dir, "model", SearchOption.AllDirectories);
        string modelPath = files[0];
        string config = Path.Join(modelPath.Split("model")[0], "MRZ.json");

        string contents = File.ReadAllText(config);

        JsonNode configNode = JsonNode.Parse(contents)!;

        if ((string)(configNode!["CharacterModelArray"]![0]!["DirectoryPath"]!) == "model")
        {
            configNode["CharacterModelArray"]![0]!["DirectoryPath"] = modelPath;

            var options = new JsonSerializerOptions { WriteIndented = true };

            contents = configNode.ToJsonString(options);

            File.WriteAllText(config, contents);
        }
        else
        {
            return 0;
        }

        byte[] errorMsg = new byte[512];
        int ret = DLR_AppendSettingsFromFile(handler, config, errorMsg, 512);
#if DEBUG
        Console.WriteLine("LoadModel(): " + Encoding.ASCII.GetString(errorMsg));
#endif
        return ret;
    }

    public Result[]? DetectFile(string filename)
    {
        if (handler == IntPtr.Zero) return null;

        int ret = DLR_RecognizeByFile(handler, filename, "locr");
#if DEBUG
        Console.WriteLine("DetectFile(): " + ret);
#endif
        return GetResults();
    }

    public Result[]? DetectBuffer(IntPtr pBufferBytes, int width, int height, int stride, ImagePixelFormat format)
    {
        if (handler == IntPtr.Zero) return null;

        IntPtr pResultArray = IntPtr.Zero;

        ImageData imageData = new ImageData();
        imageData.width = width;
        imageData.height = height;
        imageData.stride = stride;
        imageData.format = format;
        imageData.bytesLength = stride * height;
        imageData.bytes = pBufferBytes;

        IntPtr pimageData = Marshal.AllocHGlobal(Marshal.SizeOf(imageData));
        Marshal.StructureToPtr(imageData, pimageData, false);
        int ret = DLR_RecognizeByBuffer(handler, pimageData, "locr");
        Marshal.FreeHGlobal(pimageData);
#if DEBUG
        Console.WriteLine("DetectBuffer(): " + ret);
#endif
        return GetResults();
    }

    private Result[]? GetResults()
    {
        IntPtr pDLR_ResultArray = IntPtr.Zero;
        DLR_GetAllResults(handler, ref pDLR_ResultArray);

        if (pDLR_ResultArray != IntPtr.Zero)
        {
            List<Result> resultArray = new List<Result>();
            DLR_ResultArray? results = (DLR_ResultArray?)Marshal.PtrToStructure(pDLR_ResultArray, typeof(DLR_ResultArray));
            if (results != null)
            {
                int count = results.Value.resultsCount;

                if (count > 0)
                {
                    IntPtr[] mrzResults = new IntPtr[count];
                    Marshal.Copy(results.Value.results, mrzResults, 0, count);


                    for (int i = 0; i < count; i++)
                    {
                        DLR_Result result = (DLR_Result)Marshal.PtrToStructure(mrzResults[i], typeof(DLR_Result))!;
                        int lineResultsCount = result.lineResultsCount;
                        IntPtr lineResults = result.lineResults;
                        IntPtr[] lines = new IntPtr[lineResultsCount];
                        Marshal.Copy(lineResults, lines, 0, lineResultsCount);

                        for (int j = 0; j < lineResultsCount; j++)
                        {
                            Result mrzResult = new Result();
                            DLR_LineResult lineResult = (DLR_LineResult)Marshal.PtrToStructure(lines[j], typeof(DLR_LineResult))!;
                            mrzResult.Text = lineResult.text;
                            mrzResult.Confidence = lineResult.confidence;
                            DM_Point[] points = lineResult.location.points;
                            mrzResult.Points = new int[8];
                            for (int k = 0; k < 4; k++)
                            {
                                mrzResult.Points[k * 2] = points[k].coordinate[0];
                                mrzResult.Points[k * 2 + 1] = points[k].coordinate[1];
                            }

                            resultArray.Add(mrzResult);
                        }
                    }
                }
            }

            // Release memory of barcode results
            DLR_FreeResults(ref pDLR_ResultArray);

            return resultArray.ToArray();
        }

        return null;
    }
}
