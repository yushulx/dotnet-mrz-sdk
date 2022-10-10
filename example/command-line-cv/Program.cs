using System;
using System.Runtime.InteropServices;
using Dynamsoft;
using OpenCvSharp;
using OpenCvSharp.Extensions;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {

            MrzScanner.InitLicense("DLS2eyJoYW5kc2hha2VDb2RlIjoiMjAwMDAxLTE2NDk4Mjk3OTI2MzUiLCJvcmdhbml6YXRpb25JRCI6IjIwMDAwMSIsInNlc3Npb25QYXNzd29yZCI6IndTcGR6Vm05WDJrcEQ5YUoifQ=="); // Get a license key from https://www.dynamsoft.com/customer/license/trialLicense?product=ddn
            Console.WriteLine("Version: " + MrzScanner.GetVersionInfo());
            MrzScanner scanner = MrzScanner.Create();
            int ret = scanner.LoadModel();
            Console.WriteLine("LoadModel: " + ret);

            Mat mat = Cv2.ImRead("1.jpg", ImreadModes.Color);
            Mat copy = new Mat(mat.Rows, mat.Cols, MatType.CV_8UC3);
            mat.CopyTo(copy);
            
            MrzScanner.Result[]? resultArray = scanner.DetectBuffer(copy.Data, copy.Cols, copy.Rows, (int)copy.Step(), MrzScanner.ImagePixelFormat.IPF_RGB_888);
            if (resultArray != null)
            {
                foreach (MrzScanner.Result result in resultArray)
                {
                    Console.WriteLine(result.Text);
                    if (result.Points != null)
                    {
                        Point[] points = new Point[4];
                        for (int i = 0; i < 4; i++)
                        {
                            points[i] = new Point(result.Points[i * 2], result.Points[i * 2 + 1]);
                        }
                        Cv2.DrawContours(mat, new Point[][] { points }, 0, Scalar.Red, 2);
                        Cv2.ImShow("MRZ", mat);
                    }
                }
                Cv2.WaitKey(0);
                Cv2.DestroyAllWindows();
            }
        }
    }
}
