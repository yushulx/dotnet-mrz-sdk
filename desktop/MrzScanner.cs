namespace Dynamsoft;

using System.Text;
using System.Runtime.InteropServices;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.RegularExpressions;

using System;
using System.IO;
using System.Reflection;

public class MrzScanner
{
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

    [DllImport("DynamsoftLabelRecognizerx64")]
    static extern int DLR_AppendSettingsFromString(IntPtr handler, string content, [Out] byte[] errorMsg, int errorMsgSize);
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

    [DllImport("DynamsoftLabelRecognizer")]
    static extern int DLR_GetAllResults(IntPtr hBarcode, ref IntPtr pDLR_ResultArray);

    [DllImport("DynamsoftLabelRecognizer")]
    static extern int DLR_RecognizeByFile(IntPtr handler, string sourceFilePath, string templateName);

    [DllImport("DynamsoftLabelRecognizer")]
    static extern int DLR_RecognizeByBuffer(IntPtr handler, IntPtr sourceImage, string templateName);

    [DllImport("DynamsoftLabelRecognizer")]
    static extern int DLR_AppendSettingsFromFile(IntPtr handler, string filename, [Out] byte[] errorMsg, int errorMsgSize);

    [DllImport("DynamsoftLabelRecognizer")]
    static extern int DLR_AppendSettingsFromString(IntPtr handler, string content, [Out] byte[] errorMsg, int errorMsgSize);
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
        LoadModel();
    }

    ~MrzScanner()
    {
        Destroy();
    }

    public static JsonNode? Parse(string[] lines)
    {
        JsonNode mrzInfo = new JsonObject();

        if (lines.Length == 0)
        {
            return null;
        }

        if (lines.Length == 2)
        {
            string line1 = lines[0];
            string line2 = lines[1];

            var type = line1.Substring(0, 1);
            if (!new Regex(@"[I|P|V]").IsMatch(type)) return null;
            if (type == "P")
            {
                mrzInfo["type"] = "PASSPORT (TD-3)";
            }
            else if (type == "V")
            {
                if (line1.Length == 44)
                {
                    mrzInfo["type"] = "VISA (MRV-A)";
                }
                else if (line1.Length == 36)
                {
                    mrzInfo["type"] = "VISA (MRV-B)";
                }
            }
            else if (type == "I")
            {
                mrzInfo["type"] = "ID CARD (TD-2)";
            }

            // Get issuing State infomation
            var nation = line1.Substring(2, 5);
            if (new Regex(@"[0-9]").IsMatch(nation)) return null;
            if (nation[nation.Length - 1] == '<')
            {
                nation = nation.Substring(0, 2);
            }
            mrzInfo["nationality"] = nation;
            // Get surname information
            line1 = line1.Substring(5);
            var pos = line1.IndexOf("<<");
            var surName = line1.Substring(0, pos);
            if (new Regex(@"[0-9]").IsMatch(surName)) return null;
            surName = surName.Replace("<", " ");
            mrzInfo["surname"] = surName;
            // Get givenname information
            var givenName = line1.Substring(surName.Length + 2);
            if (new Regex(@"[0-9]").IsMatch(givenName)) return null;
            givenName = givenName.Replace("<", " ");
            givenName = givenName.Trim();
            mrzInfo["givenname"] = givenName;
            // Get passport number information
            var passportNumber = "";
            passportNumber = line2.Substring(0, 9);
            passportNumber = passportNumber.Replace("<", " ");
            mrzInfo["passportnumber"] = passportNumber;
            // Get Nationality information
            var issueCountry = line2.Substring(10, 3);
            if (new Regex(@"[0-9]").IsMatch(issueCountry)) return null;
            if (issueCountry[issueCountry.Length - 1] == '<')
            {
                issueCountry = issueCountry.Substring(0, 2);
            }
            mrzInfo["issuecountry"] = issueCountry;
            // Get date of birth information
            var birth = line2.Substring(13, 6);
            var date = new DateTime();
            var currentYear = date.Year;
            if (Int32.Parse(birth.Substring(0, 2)) > (currentYear % 100))
            {
                birth = "19" + birth;
            }
            else
            {
                birth = "20" + birth;
            }
            birth = birth.Substring(0, 4) + "-" + birth.Substring(4, 2) + "-" + birth.Substring(6, 2);
            if (new Regex(@"[A-Za-z]").IsMatch(birth)) return null;
            mrzInfo["birth"] = birth;
            // Get gender information
            var gender = line2[20] + "";
            if (!(new Regex(@"[M|F|x|<]").IsMatch(gender))) return null;
            mrzInfo["gender"] = gender;
            // Get date of expiry information
            var expiry = line2.Substring(21, 6);
            if (new Regex(@"[A-Za-z]").IsMatch(expiry)) return null;
            if (Int32.Parse(expiry.Substring(0, 2)) >= 60)
            {
                expiry = "19" + expiry;
            }
            else
            {
                expiry = "20" + expiry;
            }
            expiry = expiry.Substring(0, 4) + "-" + expiry.Substring(4, 2) + "-" + expiry.Substring(6);
            mrzInfo["expiry"] = expiry;
        }
        else if (lines.Length == 3)
        {
            string line1 = lines[0];
            string line2 = lines[1];
            string line3 = lines[2];
            var type = line1.Substring(0, 1);
            if (!new Regex(@"[I|P|V]").IsMatch(type)) return null;
            mrzInfo["type"] = "ID CARD (TD-1)";
            // Get nationality infomation
            var nation = line2.Substring(15, 3);
            if (new Regex(@"[0-9]").IsMatch(nation)) return null;
            nation = nation.Replace("<", "");
            mrzInfo["nationality"] = nation;
            // Get surname information
            var pos = line3.IndexOf("<<");
            var surName = line3.Substring(0, pos);
            if (new Regex(@"[0-9]").IsMatch(surName)) return null;
            surName = surName.Replace("<", " ");
            surName.Trim();
            mrzInfo["surname"] = surName;
            // Get givenname information
            var givenName = line3.Substring(surName.Length + 2);
            if (new Regex(@"[0-9]").IsMatch(givenName)) return null;
            givenName = givenName.Replace("<", " ");
            givenName = givenName.Trim();
            mrzInfo["givenname"] = givenName;
            // Get passport number information
            var passportNumber = "";
            passportNumber = line1.Substring(5, 9);
            passportNumber = passportNumber.Replace("<", " ");
            mrzInfo["passportnumber"] = passportNumber;
            // Get issuing country or organization information
            var issueCountry = line1.Substring(2, 3);
            if (new Regex(@"[0-9]").IsMatch(issueCountry)) return null;
            issueCountry = issueCountry.Replace("<", "");
            mrzInfo["issuecountry"] = issueCountry;
            // Get date of birth information
            var birth = line2.Substring(0, 6);
            if (new Regex(@"[A-Za-z]").IsMatch(birth)) return null;
            var date = new DateTime();
            var currentYear = date.Year;
            if (Int32.Parse(birth.Substring(0, 2)) > (currentYear % 100))
            {
                birth = "19" + birth;
            }
            else
            {
                birth = "20" + birth;
            }
            birth = birth.Substring(0, 4) + "-" + birth.Substring(4, 2) + "-" + birth.Substring(6);
            mrzInfo["birth"] = birth;
            // Get gender information
            var gender = line2[7] + "";
            if (!(new Regex(@"[M|F|X|<]").IsMatch(gender))) return null;
            gender = gender.Replace("<", "X");
            mrzInfo["gender"] = gender;
            // Get date of expiry information
            var expiry = "20" + line2.Substring(8, 6);
            if (new Regex(@"[A-Za-z]").IsMatch(expiry)) return null;
            expiry = expiry.Substring(0, 4) + "-" + expiry.Substring(4, 2) + "-" + expiry.Substring(6);
            mrzInfo["expiry"] = expiry;
        }

        return mrzInfo;
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

    public static void InitLicense(string license, object? context = null)
    {
        byte[] errorMsg = new byte[512];
        licenseKey = license;
        int ret = DLR_InitLicense(license, errorMsg, 512);
#if DEBUG
        Console.WriteLine("InitLicense(): " + Encoding.ASCII.GetString(errorMsg));
#endif
        if (ret != 0)
        {
            throw new Exception("InitLicense(): " + Encoding.ASCII.GetString(errorMsg));
        }
    }

    public static string ExtractModelsToDocuments()
    {
        // Determine the path of the executing assembly (your application)
        string assemblyLocation = Assembly.GetExecutingAssembly().Location;
        string? assemblyPath = Path.GetDirectoryName(assemblyLocation);
        if (assemblyPath == null) throw new Exception("Cannot get assembly path.");

        // Define the relative path to the model folder in the NuGet package
        var modelFolderPath = Path.Combine(assemblyPath, "model");

        // Define the target path in the Documents folder
        var documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        var targetFolderPath = Path.Combine(documentsPath, "MyModels");

        // Create the target folder if it doesn't exist
        Directory.CreateDirectory(targetFolderPath);

        // Copy the model files
        foreach (var filePath in Directory.GetFiles(modelFolderPath))
        {
            var fileName = Path.GetFileName(filePath);
            var destFilePath = Path.Combine(targetFolderPath, fileName);
            File.Copy(filePath, destFilePath, overwrite: true); // Set overwrite to true or false as needed
        }

        return targetFolderPath;
    }

    private int LoadModel(string path = "")
    {

        if (handler == IntPtr.Zero) return -1;

        string assemblyLocation = Assembly.GetExecutingAssembly().Location;
        string? assemblyPath = Path.GetDirectoryName(assemblyLocation);
        if (assemblyPath == null) throw new Exception("Cannot get assembly path.");
        var targetFolderPath = Path.Combine(assemblyPath, "model").ToString();

        string modelPath = path == "" ? targetFolderPath : path;
        string config = Path.Join(modelPath, "MRZ.json");

        if (!File.Exists(config))
        {
            throw new Exception("Cannot find model config file.");
        }

        string contents = File.ReadAllText(config);

        JsonNode configNode = JsonNode.Parse(contents)!;

        if ((string)(configNode!["CharacterModelArray"]![0]!["DirectoryPath"]!) == "model")
        {
            configNode["CharacterModelArray"]![0]!["DirectoryPath"] = modelPath;

            var options = new JsonSerializerOptions { WriteIndented = true };

            contents = configNode.ToJsonString(options);

            // File.WriteAllText(config, contents);
        }

        byte[] errorMsg = new byte[512];
        int ret = DLR_AppendSettingsFromString(handler, contents, errorMsg, 512);
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

    public Result[]? DetectBuffer(byte[] buffer, int width, int height, int stride, ImagePixelFormat format)
    {
        if (handler == IntPtr.Zero) return null;

        int length = buffer.Length;
        IntPtr pBufferBytes = Marshal.AllocHGlobal(length);
        Marshal.Copy(buffer, 0, pBufferBytes, length);

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

        Marshal.FreeHGlobal(pBufferBytes);
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
