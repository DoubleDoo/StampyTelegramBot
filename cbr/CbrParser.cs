using System;
using System.Text.RegularExpressions;
using HtmlAgilityPack;
using Npgsql;

public static class CbrParser
{
    static CbrParser(){}
    public static string GetNameFromHtml(HtmlDocument doc)
    {
        HtmlNodeCollection res = doc.DocumentNode.SelectNodes("//span[contains(@class, 'referenceable')]");
        if (res != null)
            return ParseName(res[0].InnerText);
        return ParseName("");
    }
    public static string ParseName(string nameHtml)
    {
        nameHtml = nameHtml.Replace("&nbsp;", " ");
        return nameHtml;
    }

    public static DateOnly GetDateFromHtml(HtmlDocument doc)
    {
        HtmlNodeCollection res = doc.DocumentNode.SelectNodes("//div[contains(@class, 'money_option_title')]");
        if (res != null)
            foreach (HtmlNode node in res)
            {
                if (node.InnerText.Contains("Дата выпуска"))
                {
                    return ParseDate(node.SelectSingleNode("..//div[contains(@class, 'money_option_value')]").InnerText);
                }
            }
        return ParseDate("");
    }
    public static DateOnly ParseDate(string dateHtml)
    {
        string[] spt = dateHtml.Split(".");
        return new DateOnly(int.Parse(spt[2]), int.Parse(spt[1]), int.Parse(spt[0]));
    }

    public static string GetSeriesFromHtml(HtmlDocument doc)
    {
        HtmlNodeCollection res = doc.DocumentNode.SelectNodes("//div[contains(@class, 'commemor-coin_intro_text')]");
        if (res != null)
            foreach (HtmlNode node in res)
            {
                if (node.InnerText.Contains("Серия"))
                {
                    return ParseSeries(node.InnerText);
                }
            }
        return ParseSeries("");
    }
    public static string ParseSeries(string seriesHtml)
    {
        if (seriesHtml != "")
        {
            seriesHtml = seriesHtml.Replace("Серия:", "");
            seriesHtml = seriesHtml.Replace("Cерия", "");
            return seriesHtml.Trim();
        }
        return "";
    }

    public static string GetCatalogIdFromHtml(HtmlDocument doc)
    {
        HtmlNodeCollection res = doc.DocumentNode.SelectNodes("//div[contains(@class, 'money_option_title')]");
        if (res != null)
            foreach (HtmlNode node in res)
            {
                if (node.InnerText.Contains("Каталожный номер"))
                {
                    return ParseCatalogId(node.SelectSingleNode("..//div[contains(@class, 'money_option_value')]").InnerText);
                }
            }
        return ParseCatalogId("notfound");
    }
    public static string ParseCatalogId(string catalogIdHtml)
    {
        return catalogIdHtml;
    }

    public static decimal GetNominalIdFromHtml(HtmlDocument doc)
    {
        HtmlNodeCollection res = doc.DocumentNode.SelectNodes("//div[contains(@class, 'characteristic_denomenation')]");
        if (res != null)
            foreach (HtmlNode node in res)
            {
                if (node.InnerText.Contains("Номинал"))
                {
                    return ParseNominal(node.SelectSingleNode("..//div[contains(@class, 'characteristic_value')]").InnerText);
                }
            }
        return ParseNominal("");
    }
    public static decimal ParseNominal(string nominalHtml)
    {
        string[] spt = nominalHtml.Split(" ");
        return decimal.Parse(spt[0]);
    }

    public static double GetFirstDimentionFromHtml(HtmlDocument doc)
    {
        HtmlNodeCollection res = doc.DocumentNode.SelectNodes("//div[contains(@class, 'characteristic_denomenation')]");
        if (res != null)
            foreach (HtmlNode node in res)
            {
                if (node.InnerText.Contains("Диаметр") || node.InnerText.Contains("Длина"))
                {
                    return ParseDimention(node.SelectSingleNode("..//div[contains(@class, 'characteristic_value')]").InnerText);
                }
            }
        return ParseDimention("");
    }

    public static double GetSecondDimentionFromHtml(HtmlDocument doc)
    {
        HtmlNodeCollection res = doc.DocumentNode.SelectNodes("//div[contains(@class, 'characteristic_denomenation')]");
        if (res != null)
            foreach (HtmlNode node in res)
            {
                if (node.InnerText.Contains("Ширина"))
                {
                    return ParseDimention(node.SelectSingleNode("..//div[contains(@class, 'characteristic_value')]").InnerText);
                }
            }
        return ParseDimention("");
    }
    public static double ParseDimention(string diameterHtml)
    {
        try
        {
            Regex regex = new Regex(@"\d(\d|,)+");
            MatchCollection matches = regex.Matches(diameterHtml);
            diameterHtml = matches[0].Value.Replace(",", ".");
            return double.Parse(diameterHtml);
        }
        catch (Exception e)
        {
            return -1;
        }
    }


    public static string GetMetalFromHtml(HtmlDocument doc)
    {
        HtmlNodeCollection res = doc.DocumentNode.SelectNodes("//div[contains(@class, 'characteristic_denomenation')]");
        if (res != null)
            foreach (HtmlNode node in res)
            {
                if (node.InnerText.Contains("Сплав") || node.InnerText.Contains("Материал") || node.InnerText.Contains("Металл"))
                {
                    return parseMetal(node.SelectSingleNode("..//div[contains(@class, 'characteristic_value')]").InnerText);
                }
            }
        return parseMetal("");
    }
    public static string parseMetal(string metalHtml)
    {
        return metalHtml;
    }

    public static long GetCirculationFromHtml(HtmlDocument doc)
    {
        HtmlNodeCollection res = doc.DocumentNode.SelectNodes("//div[contains(@class, 'characteristic_denomenation')]");
        if (res != null)
            foreach (HtmlNode node in res)
            {
                if (node.InnerText.Contains("Тираж"))
                {
                    return ParseCirculation(node.SelectSingleNode("..//div[contains(@class, 'characteristic_value')]").InnerText);
                }
            }
        return ParseCirculation("");
    }
    public static long ParseCirculation(string circulationHtml)
    {
        Regex regex = new Regex(@"\d(\d|\s)+");
        MatchCollection matches = regex.Matches(circulationHtml);
        circulationHtml = matches[0].Value.Replace(" ", "");
        return long.Parse(circulationHtml);
    }


    public static string GetObverseFromHtml(HtmlDocument doc)
    {
        HtmlNodeCollection res2 = doc.DocumentNode.SelectNodes(".//div[contains(@class, 'commemor-coin_images')]");
        if (res2 != null)
        {
            return Cbr.linkBase + res2[0].SelectNodes(".//img")[0].GetAttributeValue("src", "");
        }
        return "";
    }

    public static string GetReverseFromHtml(HtmlDocument doc)
    {
        HtmlNodeCollection res2 = doc.DocumentNode.SelectNodes(".//div[contains(@class, 'commemor-coin_images')]");
        if (res2 != null)
        {
            return Cbr.linkBase + res2[0].SelectNodes(".//img")[1].GetAttributeValue("src", "");
        }
        return "";
    }

}