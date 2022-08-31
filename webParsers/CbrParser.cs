using System;
using System.Text.RegularExpressions;
using HtmlAgilityPack;
using Npgsql;

public static class CbrParser
{
    static CbrParser()
	{

	}
    public static string getNameFromHtml(HtmlDocument doc)
    {
        HtmlNodeCollection res = doc.DocumentNode.SelectNodes("//span[contains(@class, 'referenceable')]");
        if (res != null)
            return parseName(res[0].InnerText);
        return parseName("");
    }
    public static string parseName(string nameHtml)
    {
        nameHtml = nameHtml.Replace("&nbsp;", " ");
        return nameHtml;
    }

    public static DateOnly getDateFromHtml(HtmlDocument doc)
    {
        HtmlNodeCollection res = doc.DocumentNode.SelectNodes("//div[contains(@class, 'money_option_title')]");
        if (res != null)
            foreach (HtmlNode node in res)
            {
                if (node.InnerText.Contains("Дата выпуска"))
                {
                    return parseDate(node.SelectSingleNode("..//div[contains(@class, 'money_option_value')]").InnerText);
                }
            }
        return parseDate("");
    }
    public static DateOnly parseDate(string dateHtml)
    {
        string[] spt = dateHtml.Split(".");
        return new DateOnly(int.Parse(spt[2]), int.Parse(spt[1]), int.Parse(spt[0]));
    }

    public static string getSeriesFromHtml(HtmlDocument doc)
    {
        HtmlNodeCollection res = doc.DocumentNode.SelectNodes("//div[contains(@class, 'commemor-coin_intro_text')]");
        if (res != null)
            foreach (HtmlNode node in res)
            {
                if (node.InnerText.Contains("Серия"))
                {
                    return parseSeries(node.InnerText);
                }
            }
        return parseSeries("");
    }
    public static string parseSeries(string seriesHtml)
    {
        if (seriesHtml != "")
        {
            seriesHtml = seriesHtml.Replace("Серия:", "");
            seriesHtml = seriesHtml.Replace("Cерия", "");
            return seriesHtml.Trim();
        }
        return "";
    }

    public static string getCatalogIdFromHtml(HtmlDocument doc)
    {
        HtmlNodeCollection res = doc.DocumentNode.SelectNodes("//div[contains(@class, 'money_option_title')]");
        if (res != null)
            foreach (HtmlNode node in res)
            {
                if (node.InnerText.Contains("Каталожный номер"))
                {
                    return parseCatalogId(node.SelectSingleNode("..//div[contains(@class, 'money_option_value')]").InnerText);
                }
            }
        return parseCatalogId("notfound");
    }
    public static string parseCatalogId(string catalogIdHtml)
    {
        return catalogIdHtml;
    }

    public static decimal getNominalIdFromHtml(HtmlDocument doc)
    {
        HtmlNodeCollection res = doc.DocumentNode.SelectNodes("//div[contains(@class, 'characteristic_denomenation')]");
        if (res != null)
            foreach (HtmlNode node in res)
            {
                if (node.InnerText.Contains("Номинал"))
                {
                    return parseNominal(node.SelectSingleNode("..//div[contains(@class, 'characteristic_value')]").InnerText);
                }
            }
        return parseNominal("");
    }
    public static decimal parseNominal(string nominalHtml)
    {
        string[] spt = nominalHtml.Split(" ");
        return decimal.Parse(spt[0]);
    }

    public static double getFirstDimentionFromHtml(HtmlDocument doc)
    {
        HtmlNodeCollection res = doc.DocumentNode.SelectNodes("//div[contains(@class, 'characteristic_denomenation')]");
        if (res != null)
            foreach (HtmlNode node in res)
            {
                if (node.InnerText.Contains("Диаметр") || node.InnerText.Contains("Длина"))
                {
                    return parseDimention(node.SelectSingleNode("..//div[contains(@class, 'characteristic_value')]").InnerText);
                }
            }
        return parseDimention("");
    }

    public static double getSecondDimentionFromHtml(HtmlDocument doc)
    {
        HtmlNodeCollection res = doc.DocumentNode.SelectNodes("//div[contains(@class, 'characteristic_denomenation')]");
        if (res != null)
            foreach (HtmlNode node in res)
            {
                if (node.InnerText.Contains("Ширина"))
                {
                    return parseDimention(node.SelectSingleNode("..//div[contains(@class, 'characteristic_value')]").InnerText);
                }
            }
        return parseDimention("");
    }
    public static double parseDimention(string diameterHtml)
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


    public static string getMetalFromHtml(HtmlDocument doc)
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

    public static long getCirculationFromHtml(HtmlDocument doc)
    {
        HtmlNodeCollection res = doc.DocumentNode.SelectNodes("//div[contains(@class, 'characteristic_denomenation')]");
        if (res != null)
            foreach (HtmlNode node in res)
            {
                if (node.InnerText.Contains("Тираж"))
                {
                    return parseCirculation(node.SelectSingleNode("..//div[contains(@class, 'characteristic_value')]").InnerText);
                }
            }
        return parseCirculation("");
    }
    public static long parseCirculation(string circulationHtml)
    {
        Regex regex = new Regex(@"\d(\d|\s)+");
        MatchCollection matches = regex.Matches(circulationHtml);
        circulationHtml = matches[0].Value.Replace(" ", "");
        return long.Parse(circulationHtml);
    }


    public static string getObverseFromHtml(HtmlDocument doc)
    {
        HtmlNodeCollection res2 = doc.DocumentNode.SelectNodes(".//div[contains(@class, 'commemor-coin_images')]");
        if (res2 != null)
        {
            return Cbr.linkBase + res2[0].SelectNodes(".//img")[0].GetAttributeValue("src", "");
        }
        return "";
    }

    public static string getReverseFromHtml(HtmlDocument doc)
    {
        HtmlNodeCollection res2 = doc.DocumentNode.SelectNodes(".//div[contains(@class, 'commemor-coin_images')]");
        if (res2 != null)
        {
            return Cbr.linkBase + res2[0].SelectNodes(".//img")[1].GetAttributeValue("src", "");
        }
        return "";
    }

}