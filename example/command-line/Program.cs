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

            MrzScanner.Result[]? results = scanner.DetectFile("1.png");
            if (results != null)
            {
                foreach (MrzScanner.Result result in results)
                {
                    Console.WriteLine(result.Text);
                    Console.WriteLine(result.Points[0] + ", " +result.Points[1] + ", " + result.Points[2] + ", " + result.Points[3] + ", " + result.Points[4] + ", " + result.Points[5] + ", " + result.Points[6] + ", " + result.Points[7]);
                }
            }

        }
    }
}
