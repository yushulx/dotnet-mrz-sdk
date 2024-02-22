namespace Dynamsoft;

using System;
using System.Text.RegularExpressions;

public static class MrzParser
{
    private static readonly Regex TypeRegex = new Regex(@"[I|P|V]");
    private static readonly Regex NumericRegex = new Regex(@"[0-9]");
    private static readonly Regex AlphaRegex = new Regex(@"[A-Za-z]");
    private static readonly Regex GenderRegex = new Regex(@"[M|F|x|<]");

    private static string ExtractAndClean(string source, int start, int length)
    {
        if (length < 0) return source;
        return source.Substring(start, length).Replace('<', ' ').Trim();
    }

    private static string ExtractDate(string source, int start)
    {
        string dateString = source.Substring(start, 6);
        if (AlphaRegex.IsMatch(dateString)) return "N/A";

        int year = int.Parse(dateString.Substring(0, 2));
        int month = int.Parse(dateString.Substring(2, 2));
        int day = int.Parse(dateString.Substring(4, 2));

        // Adjust the year
        year += year > DateTime.Now.Year % 100 ? 1900 : 2000;

        try
        {
            return new DateTime(year, month, day).ToString("yyyy/MM/dd");
        }
        catch
        {
            return "N/A";
        }
    }

    private static string ExtracExpiration(string source, int start)
    {
        string dateString = source.Substring(start, 6);
        if (AlphaRegex.IsMatch(dateString)) return "N/A";

        int year = int.Parse(dateString.Substring(0, 2));
        int month = int.Parse(dateString.Substring(2, 2));
        int day = int.Parse(dateString.Substring(4, 2));

        // Adjust the year
        year += year >= 60 ? 1900 : 2000;

        try
        {
            return new DateTime(year, month, day).ToString("yyyy/MM/dd");
        }
        catch
        {
            return "N/A";
        }
    }

    public static MrzResult Parse(string[] lines)
    {
        if (lines.Length == 2) return ParseTwoLines(lines[0], lines[1]);
        if (lines.Length == 3) return ParseThreeLines(lines[0], lines[1], lines[2]);
        return new MrzResult();
    }

    public static MrzResult ParseTwoLines(string line1, string line2)
    {
        MrzResult mrzInfo = new MrzResult();

        //if (!TypeRegex.IsMatch(line1[0].ToString())) return mrzInfo;

        switch (line1[0])
        {
            case 'P':
                mrzInfo.Type = "PASSPORT (TD-3)";
                break;
            case 'V':
                mrzInfo.Type = line1.Length == 44 ? "VISA (MRV-A)" : "VISA (MRV-B)";
                break;
            case 'I':
                mrzInfo.Type = "ID CARD (TD-2)";
                break;
        }

        mrzInfo.Nationality = ExtractAndClean(line1, 2, 3);
        mrzInfo.Surname = ExtractAndClean(line1, 5, line1.IndexOf("<<") - 5);
        mrzInfo.GivenName = ExtractAndClean(line1, mrzInfo.Surname.Length + 7, line1.Length - mrzInfo.Surname.Length - 7);
        mrzInfo.PassportNumber = ExtractAndClean(line2, 0, 9);
        mrzInfo.IssuingCountry = ExtractAndClean(line2, 10, 3);
        mrzInfo.BirthDate = ExtractDate(line2, 13);
        mrzInfo.Gender = GenderRegex.IsMatch(line2[20].ToString()) ? line2[20].ToString().Replace('<', 'X') : "N/A";
        mrzInfo.Expiration = ExtracExpiration(line2, 21);
        mrzInfo.Lines = $"{line1}\n{line2}";
        return mrzInfo;
    }

    public static MrzResult ParseThreeLines(string line1, string line2, string line3)
    {
        MrzResult mrzInfo = new MrzResult();

        //if (!TypeRegex.IsMatch(line1[0].ToString())) return mrzInfo;
        mrzInfo.Type = "ID CARD (TD-1)";

        mrzInfo.Nationality = ExtractAndClean(line2, 15, 3);
        mrzInfo.Surname = ExtractAndClean(line3, 0, line3.IndexOf("<<"));
        mrzInfo.GivenName = ExtractAndClean(line3, mrzInfo.Surname.Length + 2, line3.Length - mrzInfo.Surname.Length - 2);
        mrzInfo.PassportNumber = ExtractAndClean(line1, 5, 9);
        mrzInfo.IssuingCountry = ExtractAndClean(line1, 2, 3);
        mrzInfo.BirthDate = ExtractDate(line2, 0);
        mrzInfo.Gender = GenderRegex.IsMatch(line2[7].ToString()) ? line2[7].ToString().Replace('<', 'X') : "N/A";
        mrzInfo.Expiration = ExtracExpiration(line2, 8);
        mrzInfo.Lines = $"{line1}\n{line2}\n{line3}";
        return mrzInfo;
    }
}
