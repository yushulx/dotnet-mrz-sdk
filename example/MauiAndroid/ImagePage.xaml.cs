#if ANDROID || IOS
using Com.Dynamsoft.Dlr;
using Dynamsoft;
#endif

namespace MauiAndroidMrz;

public partial class ImagePage : ContentPage
{
#if ANDROID || IOS
    private MrzScanner mrzScanner;
#endif

    public ImagePage(string imagepath)
    {
        InitializeComponent();

        Image.Source = imagepath;

#if ANDROID || IOS
        mrzScanner = MrzScanner.Create();

        MrzScanner.Result[] results = mrzScanner.DetectFile(imagepath);
        MrzResult mrzResults = new MrzResult();

        if (results != null && results.Length > 0)
        {
            string[] lines = new string[results.Length];

            for (int i = 0; i < results.Length; i++)
            {
                lines[i] = results[i].Text;
            }

            try
            {
                Dynamsoft.MrzResult info = MrzParser.Parse(lines);
                Result.Text = info.ToString();
            }
            catch (Exception ex)
            {

            }
        }
                        

        //recognizer = new MRZRecognizer();
        //MRZResult? mrsResult = recognizer.RecognizeMRZFromFile(imagepath);
        //if (mrsResult != null)
        //{
        //    string mrzInfo = "";
        //    mrzInfo += "DocId: " + mrsResult.DocId + '\n';
        //    mrzInfo += "DocType: " + mrsResult.DocType + '\n';
        //    mrzInfo += "Nationality: " + mrsResult.Nationality + '\n';
        //    mrzInfo += "Issuer: " + mrsResult.Issuer + '\n';
        //    mrzInfo += "Birth: " + mrsResult.DateOfBirth + '\n';
        //    mrzInfo += "Expiration: " + mrsResult.DateOfExpiration + '\n';
        //    mrzInfo += "Gender: " + mrsResult.Gender + '\n';
        //    mrzInfo += "Surname: " + mrsResult.Surname + '\n';
        //    mrzInfo += "GivenName: " + mrsResult.GivenName + '\n';
        //    mrzInfo += "IsParsed: " + mrsResult.IsParsed + '\n';
        //    mrzInfo += "IsVerified: " + mrsResult.IsVerified + '\n';

        //    Result.Text = mrzInfo;
        //}
#endif

    }
}