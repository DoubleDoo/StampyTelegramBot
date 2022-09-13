using HtmlAgilityPack;
using System.Diagnostics;
using System.Text.RegularExpressions;

public static class Parser
{
    static Parser() { }

    public static string ParseString(string str)
    {
        if(str!=null)
        {
            if (str == "") return "";
            str = Regex.Replace(str, "(&quot;|&lsquo;|&rsquo;|&ldquo;|&rdquo;|&sbquo;|&bdquo;|&rsaquo;|&lsaquo;|&raquo;|&laquo;)", "\"");
            str = Regex.Replace(str, "(&nbsp;)", " ");
            str = Regex.Replace(str, "(&ndash;|&mdash)", " ");
            str = Regex.Replace(str, "\n", "");
            str = Regex.Replace(str, "  ", "");
            return str.Trim();
        }
        return "-1";
    }

    public static decimal ParseDecimal(string str)
    {
        if (str != null)
        {
            if (str == "") return 0;
            Regex regex = new Regex(@"(\d)+((,|\.)(\d)+)?");
            Match match = regex.Match(str);
            str = match.Value.Replace(",", ".");
            decimal res;
            if(decimal.TryParse(str,out res))
            {
                return res;
            }
            return -1;
        }
        return -1;
    }
    public static DateOnly ParseDate(string str)
    {
        if (str != null)
        {
            Regex regex = new Regex(@"((\d){2}(\.|-)(\d){2}(\.|-)(\d){4})|((\d){2}(\.|-)(\d){2}(\.|-)(\d){2})");
            Match match = regex.Match(str);
            string[] split;
            if (str.IndexOf("-") >= 0)
            {
                split = match.Value.Split("-");
                if(int.Parse(split[2])<100) return new DateOnly(int.Parse(split[2])+2000, int.Parse(split[1]), int.Parse(split[0]));
                return new DateOnly(int.Parse(split[2]), int.Parse(split[1]), int.Parse(split[0]));
            }
            else if (str.IndexOf(".") >= 0)
            {
                split = match.Value.Split(".");
                if(int.Parse(split[2])<100) return new DateOnly(int.Parse(split[2])+2000, int.Parse(split[1]), int.Parse(split[0]));
                return new DateOnly(int.Parse(split[2]), int.Parse(split[1]), int.Parse(split[0]));
            }
        }
        return new DateOnly(2222,2,22);
    }
    public static double ParseDouble(string str)
    {
        if (str != null)
        {
            if (str == "") return 0;
            Regex regex = new Regex(@"(\d)+(,|\.)(\d)+");
            Match match = regex.Match(str);
            str = match.Value.Replace(",", ".");
            double res;
            if (double.TryParse(str, out res))
            {
                return res;
            }
            return -1;
        }
        return -1;
    }
    public static long ParseLong(string str)
    {
        if (str != null)
        {
            if(str=="") return 0;
            Regex regex = new Regex(@"(\d)+");
            Match match = regex.Match(str);
            str = match.Value;
            long res;
            if (long.TryParse(str, out res))
            {
                return res;
            }
            return -1;
        }
        return -1;
    }
}

   




