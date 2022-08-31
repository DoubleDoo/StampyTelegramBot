using System;
using System.Text.RegularExpressions;
using HtmlAgilityPack;
using Npgsql;

public static class Cbr
{

    private static string linkBase = "https://www.cbr.ru";
    private static string catalog = linkBase + "/cash_circulation/memorable_coins/coins_base/";

    static Cbr()
    {

    }

    public static string buildSearchRequest(bool posted, string searchPhrase, int year, int serie_id, int nominal, int metal_id, int tab, int page, int sort, string sort_direction)
    {
        string req = catalog +
        $"?UniDbQuery.Posted={posted}" +
        $"&UniDbQuery.SearchPhrase={searchPhrase}" +
        $"&UniDbQuery.year={year}" +
        $"&UniDbQuery.serie_id={serie_id}" +
        $"&UniDbQuery.nominal={nominal}" +
        $"&UniDbQuery.metal_id={metal_id}" +
        $"&UniDbQuery.tab={tab}" +
        $"&UniDbQuery.page={page}" +
        $"&UniDbQuery.sort={sort}" +
        $"&UniDbQuery.sort_direction={sort_direction}";
        return req;
    }


    public static List<string> getPageLinks()
    {
        HtmlDocument doc = Request.balansedRequest(Cbr.buildSearchRequest(true, "", 2022, 0, -1, 0, 1, 1, 99, "down"));
        HtmlNodeCollection res = doc.DocumentNode.SelectNodes("//div[contains(@class, 'coins-tile_item')]/a");
        List<string> links = new List<string>();
        if (res != null)
        {
            foreach (HtmlNode el in res)
            {
                string atr = el.GetAttributeValue("href", "");
                if (atr != "")
                { links.Add(catalog + atr); }

            }
        }
        return links;
    }

    public static async Task<Coin> getObjDataFromLink(string link)
    {
        HtmlDocument doc = Request.balansedRequest(link);
        Console.WriteLine(link);
        Coin cn= new Coin(
            getNameFromHtml(doc),
            getDateFromHtml(doc),
            getSeriesFromHtml(doc),
            getCatalogIdFromHtml(doc),
            getNominalIdFromHtml(doc),
            getFirstDimentionFromHtml(doc),
            getSecondDimentionFromHtml(doc),
            getMetalFromHtml(doc),
            getCirculationFromHtml(doc),
            getObverseFromHtml(doc),
            getReverseFromHtml(doc),
            link
        );
        return cn;
    }

    public static async Task<List<Coin>> getAllObjDataFromLink()
    {
        List<Coin> coins = new List<Coin>();
        foreach (string lnk in getPageLinks())
        {
            if (lnk != "https://www.cbr.ru/cash_circulation/memorable_coins/coins_base/ShowCoins/?cat_num=3213-0010")
            {
                Coin c = await getObjDataFromLink(lnk);
                coins.Add(c);
            }
        }
        return coins;
    }

    public static async Task<List<Coin>> getNewLinks()
    {
        List<string> links = getPageLinks();
        List<Coin> cns =  await CoinRequest.getCoins();
        List<Coin> coins = new List<Coin>();
        if (coins!=null)
        foreach (Coin coin in cns)
        {
            int indx = links.IndexOf(coin.Link);
            if (indx!=-1)
            {
                links.RemoveAt(indx);
            }
        }
        int i = 0;
        int count= links.Count;
        foreach (string lnk in links)
        {
            if (lnk != "https://www.cbr.ru/cash_circulation/memorable_coins/coins_base/ShowCoins/?cat_num=3213-0010")
            {

                Coin cn = await getObjDataFromLink(lnk);
                //await cn.load();
                //await cn.post(Telegram.getChannelDto(0));
                Console.WriteLine(cn);
                i++;
                Console.WriteLine("_______"+i+"/"+ count + "_______");
                coins.Add(cn);
            }
        }
        foreach (Coin cn in coins)
        {
            Console.WriteLine("posted:"+cn.Name);
            await cn.load();
            await cn.post(Telegram.getChannelDto(0));
            await Task.Delay(5000);
            //Thread.Sleep(5000);
        }

        return coins;
    }
   
    //_____________________________________________________________________
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
        if (seriesHtml != "") {
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
                if (node.InnerText.Contains("Диаметр")|| node.InnerText.Contains("Длина"))
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
        try { 
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
            return linkBase + res2[0].SelectNodes(".//img")[0].GetAttributeValue("src", "");
        }
        return "";
    }

    public static string getReverseFromHtml(HtmlDocument doc)
    {
        HtmlNodeCollection res2 = doc.DocumentNode.SelectNodes(".//div[contains(@class, 'commemor-coin_images')]");
        if (res2 != null)
        {
            return linkBase + res2[0].SelectNodes(".//img")[1].GetAttributeValue("src", "");
        }
        return "";
    }

}