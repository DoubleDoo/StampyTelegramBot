using System;
using System.Text.RegularExpressions;
using HtmlAgilityPack;
using Npgsql;

public static class Cbr
{

    public static string linkBase = "https://www.cbr.ru";
    public static string catalog = linkBase + "/cash_circulation/memorable_coins/coins_base/";
    public static TelegramChannel channel = new TelegramChannel("CBR Coins", "Coins from cbr.ru", "cbrcoins", "cbr.jpg");

    static Cbr()
    {

    }

    public static string BuildRequest(bool posted, string searchPhrase, int year, int serie_id, int nominal, int metal_id, int tab, int page, int sort, string sort_direction)
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


    public static async Task<List<string>> ScrapPage()
    {
        HtmlDocument doc = Request.BalansedRequest(Cbr.BuildRequest(true, "", 2022, 0, -1, 0, 1, 1, 99, "down"));
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

    public static async Task<List<string>> ScrapPageNew()
    {
        Task<List<string>> links = ScrapPage();
        Task<List<Coin>> cns = CoinRequest.Get();
        await Task.WhenAll(new Task[] { links, cns });
        List<string> ln = links.Result;
        List<Coin> cn = cns.Result;

        if (cn.Count != 0)
            foreach (Coin coin in cn)
            {
                int indx = ln.IndexOf(coin.Link);
                if (indx != -1)
                {
                    ln.RemoveAt(indx);
                }
            }
        int dx = ln.IndexOf("https://www.cbr.ru/cash_circulation/memorable_coins/coins_base/ShowCoins/?cat_num=3213-0010");
        if (dx != -1)
        {
            ln.RemoveAt(dx);
        }
        return ln;
    }

    public static async Task<Coin> Create(string link)
    {
        HtmlDocument doc = Request.BalansedRequest(link);
        Coin cn = new Coin(link,
                          CbrParser.GetCatalogIdFromHtml(doc),
                          CbrParser.GetNameFromHtml(doc),
                          CbrParser.GetSeriesFromHtml(doc),
                          CbrParser.GetMetalFromHtml(doc),
                          CbrParser.GetNominalIdFromHtml(doc),
                          CbrParser.GetCirculationFromHtml(doc),
                          CbrParser.GetDateFromHtml(doc),
                          CbrParser.GetFirstDimentionFromHtml(doc),
                          CbrParser.GetSecondDimentionFromHtml(doc),
                          CbrParser.GetObverseFromHtml(doc),
                          CbrParser.GetReverseFromHtml(doc)
        );
        return cn;
    }
}