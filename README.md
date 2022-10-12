# .NET MRZ Scanner SDK
The .NET MRZ Scanner SDK is a C# wrapper for [Dynamsoft C++ Label Recognizer SDK](https://www.dynamsoft.com/label-recognition/docs/introduction/?ver=latest). It is used to recognize MRZ information for passport, Visa, ID card and travel documents.


## License Activation
Click [here](https://www.dynamsoft.com/customer/license/trialLicense?product=dlr) to get a valid license key.

## Supported Platforms
- Windows (x64)
- Linux (x64)

## Download .NET 6 SDK
* [Windows](https://dotnet.microsoft.com/en-us/download#windowscmd)
* [Linux](https://dotnet.microsoft.com/en-us/download#linuxubuntu)

## Methods
- `public static void InitLicense(string license)`
- `public static MrzScanner Create()`
- `public Result[]? DetectFile(string filename)`
- `public Result[]? DetectBuffer(IntPtr pBufferBytes, int width, int height, int stride, ImagePixelFormat format)`
- `public static string? GetVersionInfo()`
- `public int LoadModel()`

## Usage
- Set the license key:
    
    ```csharp
    MrzScanner.InitLicense("DLS2eyJoYW5kc2hha2VDb2RlIjoiMjAwMDAxLTE2NDk4Mjk3OTI2MzUiLCJvcmdhbml6YXRpb25JRCI6IjIwMDAwMSIsInNlc3Npb25QYXNzd29yZCI6IndTcGR6Vm05WDJrcEQ5YUoifQ=="); 
    ```
- Initialize the MRZ scanner object:
    
    ```csharp
    MrzScanner scanner = MrzScanner.Create();
    ```
- Detect MRZ from an image file:

    ```csharp
    Result[]? resultArray = scanner.DetectFile(filename);
    ```    
- Detect MRZ from a buffer:

    
    ```csharp
    Mat mat = Cv2.ImRead(filename, ImreadModes.Color);
    Result[]? resultArray = scanner.DetectBuffer(copy.Data, copy.Cols, copy.Rows, (int)copy.Step(), MrzScanner.ImagePixelFormat.IPF_RGB_888);
    ```     
- Get SDK version number:

    ```csharp
    string? version = MrzScanner.GetVersionInfo();
    ```
- Load the MRZ detection model:
    
    ```csharp
    scanner.LoadModel();
    ```

## Quick Start

```csharp
using System;
using System.Runtime.InteropServices;
using Dynamsoft;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            MrzScanner.InitLicense("DLS2eyJoYW5kc2hha2VDb2RlIjoiMjAwMDAxLTE2NDk4Mjk3OTI2MzUiLCJvcmdhbml6YXRpb25JRCI6IjIwMDAwMSIsInNlc3Npb25QYXNzd29yZCI6IndTcGR6Vm05WDJrcEQ5YUoifQ=="); // Get a license key from https://www.dynamsoft.com/customer/license/trialLicense?product=dlr
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

```


## Example
- [Command-line MRZ Scanner](https://github.com/yushulx/dotnet-mrz-sdk/tree/main/example/command-line) (**Windows & Linux**)
    
    ```bash
    # DEBUG
    dotnet run
    # RELEASE
    dotnet run --configuration Release
    ```    

- [Command-line MRZ Scanner with OpenCVSharp Windows runtime](https://github.com/yushulx/dotnet-mrz-sdk/tree/main/example/command-line-cv). To make it work on Linux, you need to install [OpenCVSharp4.runtime.ubuntu.18.04-x64](https://www.nuget.org/packages/OpenCvSharp4.runtime.ubuntu.18.04-x64) package.
    
    ```bash
    dotnet run
    ```

- [WinForms Desktop MRZ Scanner](https://github.com/yushulx/dotnet-mrz-sdk/tree/main/example/desktop-gui) (**Windows Only**)
  
    ```bash
    dotnet run
    ```
    
    ![.NET WinForms MRZ Scanner](https://www.dynamsoft.com/codepool/img/2022/10/dotnet-mrz-scanner.png)

## Building NuGet Package from Source Code

```bash
dotnet build --configuration Release
```