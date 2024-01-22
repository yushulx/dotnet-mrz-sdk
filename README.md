# .NET MRZ Scanner SDK
The .NET Machine-readable Zone (MRZ) Scanner SDK is a C# wrapper for [Dynamsoft Label Recognizer](https://www.dynamsoft.com/label-recognition/overview/?utm_content=nav-products), supporting **x64 Windows**, **x64 Linux** and **Android**. It is used to recognize MRZ information from **passport**, **Visa**, **ID card** and **travel documents**.


## License Activation
Click [here](https://www.dynamsoft.com/customer/license/trialLicense?product=dlr) to get a trial license key.

## Supported Platforms
- Windows (x64)
- Linux (x64)
- Android

## API
- `public static void InitLicense(string license)`: Initialize the license. It must be called before creating the MRZ scanner object.
- `public static MrzScanner Create()`: Create the MRZ scanner object.
- `public Result[]? DetectFile(string filename)`: Detect MRZ from an image file.
- `public Result[]? DetectBuffer(byte[] buffer, int width, int height, int stride, ImagePixelFormat format)`: Detect MRZ from a buffer.
- `public static string? GetVersionInfo()`: Get SDK version number.

## Usage
- Set the license key:
    
    ```csharp
    MrzScanner.InitLicense("LICENSE-KEY"); 
    ```
- Initialize the MRZ scanner object:
    
    ```csharp
    MrzScanner scanner = MrzScanner.Create();
    ```
- Detect MRZ from an image file:

    ```csharp
    Result[]? result = scanner.DetectFile(filename);
    ```    
- Detect MRZ from a buffer:

    ```csharp
    Result[]? result = scanner.DetectBuffer(bytes, width, height, stride, MrzScanner.ImagePixelFormat.IPF_RGB_888);
    ```     
- Get SDK version number:

    ```csharp
    string? version = MrzScanner.GetVersionInfo();
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
            MrzScanner.InitLicense("LICENSE-KEY"); 
            Console.WriteLine("Version: " + MrzScanner.GetVersionInfo());
            MrzScanner scanner = MrzScanner.Create();

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
    
    ![.NET WinForms MRZ Scanner](https://camo.githubusercontent.com/4b17e1e7b3ca4528eb4dd524df1e58f60f7ba397512da3485d08e79c80f733c2/68747470733a2f2f7777772e64796e616d736f66742e636f6d2f636f6465706f6f6c2f696d672f323032322f31302f646f746e65742d6d727a2d7363616e6e65722e706e67)

## Building NuGet Package from Source Code

```bash
# build dll for desktop
cd desktop
dotnet build --configuration Release

# build dll for android
cd android
dotnet build --configuration Release

# build nuget package
nuget pack .\MrzScannerSDK.nuspec
```
