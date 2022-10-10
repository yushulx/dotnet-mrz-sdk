using System;
using System.Runtime.InteropServices;
using Dynamsoft;

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
            
            // scanner.SetParameters(MrzScanner.Templates.color);
            // MrzScanner.Result[]? resultArray = scanner.DetectFile("1.png");
            // if (resultArray != null)
            // {
            //     foreach (MrzScanner.Result result in resultArray)
            //     {
            //         Console.WriteLine("Confidence: " + result.Confidence);
            //         if (result.Points != null)
            //         {
            //             foreach (int point in result.Points)
            //             {
            //                 Console.WriteLine("Point: " + point);
            //             }

            //             MrzScanner.NormalizedImage image = scanner.NormalizeFile("1.png", result.Points);
            //             if (image != null)
            //             {
            //                 image.Save(DateTime.Now.ToFileTimeUtc() + ".png");
            //             }
            //         }

            //     }
            // }
        }
    }
}
