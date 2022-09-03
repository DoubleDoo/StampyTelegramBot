using HtmlAgilityPack;
using System.Diagnostics;
using System.Text.RegularExpressions;

public static class Parser
{
    static Parser() { }

    public static string ParseString(string str)
    {
        string buffer = str;
        try
        {
            buffer = Regex.Replace(buffer, "(&quot;|&lsquo;|&rsquo;|&ldquo;|&rdquo;|&sbquo;|&bdquo;|&rsaquo;|&lsaquo;|&raquo;|&laquo;)", "\"");
            buffer = Regex.Replace(buffer, "(&nbsp;)", " ");
            return buffer.Trim();
        }
        catch (ArgumentNullException ex)
        {
            ExceptionMessage.ExceptionString(ex,"Argument str is null");
        }
        catch (Exception ex)
        {
            ExceptionMessage.UnknownExceptionString(ex, str);
        }
        return "-1";
    }

    public static decimal ParseDecimal(string str)
    {
        string buffer = str;
        try
        {
            Regex regex = new Regex(@"(\d)+((,|\.)(\d)+)?");
            Match match = regex.Match(buffer);
            buffer = match.Value.Replace(",", ".");
            return decimal.Parse(buffer);
        }
        catch (ArgumentNullException ex)
        {
            ExceptionMessage.ExceptionString(ex, "Argument str is null");
        }
        catch (FormatException ex)
        {
            ExceptionMessage.ExceptionString(ex,str, "Error converting to decimal");
        }
        catch (OverflowException ex)
        {
            ExceptionMessage.ExceptionString(ex, str, "Value is too large or too small for decimal");
        }
        catch (Exception ex)
        {
            ExceptionMessage.UnknownExceptionString(ex, str);
        }
        return -1;
    }
    public static DateOnly ParseDate(string str)
    {
        string buffer = str;
        try
        {
            Regex regex = new Regex(@"(\d){2}\.(\d){2}\.(\d){4}");
            Match match = regex.Match(buffer);
            string[] split = match.Value.Split(".");
            return new DateOnly(int.Parse(split[2]), int.Parse(split[1]), int.Parse(split[0]));
        }
        catch (ArgumentNullException ex)
        {
            ExceptionMessage.ExceptionString(ex, "Argument str is null");
        }
        catch (FormatException ex)
        {
            ExceptionMessage.ExceptionString(ex, str, "Error converting to DateOnly");
        }
        catch (ArgumentOutOfRangeException ex)
        {
            ExceptionMessage.ExceptionString(ex, str, "Incorrect Year, Month or Day");
        }
        catch (Exception ex)
        {
            ExceptionMessage.UnknownExceptionString(ex, str);
        }
        return new DateOnly(2222,2,22);
    }
    public static double ParseDouble(string str)
    {
        string buffer = str;
        try
        {
            Regex regex = new Regex(@"(\d)+(,|\.)(\d)+");
            Match match = regex.Match(buffer);
            buffer = match.Value.Replace(",", ".");
            return double.Parse(buffer);
        }
        catch (ArgumentNullException ex)
        {
            ExceptionMessage.ExceptionString(ex, "Argument str is null");
        }
        catch (FormatException ex)
        {
            ExceptionMessage.ExceptionString(ex, str, "Error converting to double");
        }
        catch (OverflowException ex)
        {
            ExceptionMessage.ExceptionString(ex, str, "Value is too large or too small for double");
        }
        catch (Exception ex)
        {
            ExceptionMessage.UnknownExceptionString(ex, str);
        }
        return -1;
    }
    public static long ParseLong(string str)
    {
        string buffer = str;
        try
        {
            Regex regex = new Regex(@"(\d)+");
            Match match = regex.Match(buffer);
            buffer = match.Value;
            return long.Parse(buffer);
        }
        catch (ArgumentNullException ex)
        {
            ExceptionMessage.ExceptionString(ex, "Argument str is null");
        }
        catch (FormatException ex)
        {
            ExceptionMessage.ExceptionString(ex, str, "Failed converting to long");
        }
        catch (OverflowException ex)
        {
            ExceptionMessage.ExceptionString(ex, str, "Value is too large or too small for long");
        }
        catch (Exception ex)
        {
            ExceptionMessage.UnknownExceptionString(ex, str);
        }
        return -1;
    }

}

   




