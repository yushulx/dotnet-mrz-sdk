# .NET MRZ Scanner SDK
The .NET MRZ Scanner SDK is a C# wrapper for [Dynamsoft C++ Label Recognizer SDK](https://www.dynamsoft.com/label-recognition/docs/introduction/?ver=latest). It is used to recognize MRZ information from passport, Visa, ID card and travel documents.


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
- `public Result[]? DetectBuffer(byte[] buffer, int width, int height, int stride, ImagePixelFormat format)`
- `public static string? GetVersionInfo()`
- `public int LoadModel(string modelPath)`

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

    int length = mat.Cols * mat.Rows * mat.ElemSize();
    byte[] bytes = new byte[length];
    Marshal.Copy(mat.Data, bytes, 0, length);

    Result[]? resultArray = scanner.DetectBuffer(bytes, mat.Cols, mat.Rows, (int)mat.Step(), MrzScanner.ImagePixelFormat.IPF_RGB_888);
    ```     
- Get SDK version number:

    ```csharp
    string? version = MrzScanner.GetVersionInfo();
    ```
- Load the MRZ detection model. The model has been added to the NuGet package.
    
    ```csharp
    scanner.LoadModel("path/to/model/folder");
    ```
- Parse the MRZ information:

    ```csharp
    string[] lines = new string[_results.Length];
    var index = 0;
    foreach (Result result in _results)
    {
        lines[index++] = result.Text;
    }

    MrzResult info = MrzParser.Parse(lines);
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
            string? assemblyPath = System.IO.Path.GetDirectoryName(
                System.Reflection.Assembly.GetExecutingAssembly().Location
            );

            if (assemblyPath == null) {
                return;
            }

            MrzScanner.InitLicense("DLS2eyJoYW5kc2hha2VDb2RlIjoiMjAwMDAxLTE2NDk4Mjk3OTI2MzUiLCJvcmdhbml6YXRpb25JRCI6IjIwMDAwMSIsInNlc3Npb25QYXNzd29yZCI6IndTcGR6Vm05WDJrcEQ5YUoifQ=="); // Get a license key from https://www.dynamsoft.com/customer/license/trialLicense?product=dlr
            Console.WriteLine("Version: " + MrzScanner.GetVersionInfo());
            MrzScanner scanner = MrzScanner.Create();
            int ret = scanner.LoadModel(Path.Join(assemblyPath, "model"));
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
    
    ![.NET WinForms MRZ Scanner](https://camo.githubusercontent.com/b34f832e96c87a343abda0be70da3aaf2f709a8b0b9037744ed91a13bd0e7ade/68747470733a2f2f7777772e64796e616d736f66742e636f6d2f636f6465706f6f6c2f696d672f323032322f31302f646f746e65742d6d727a2d7363616e6e65722e706e67)

## Building NuGet Package from Source Code

```bash
dotnet build --configuration Release
```
