using System;
using HtmlAgilityPack;
using Npgsql;

/*
export const requests=[
    {
        posted: true,
        searchPhrase: "",
        year: 2022,
        serie_id: 0,
        nominal: -1,
        metal_id: 0,
        tab: 1,
        page: 1,
        sort: 99,
        sort_direction: "down"
    },
]*/

public static class Cbr
{
	
	private static string linkBase = "https://www.cbr.ru";
	private static string catalog = linkBase + "/cash_circulation/memorable_coins/coins_base/";

	static Cbr()
	{
		
	}

    public static string buildSearchRequest(bool posted,string searchPhrase,int year, int serie_id,int nominal,int metal_id, int tab, int page,int sort, string sort_direction)
    {
        string req = catalog +
        $"?UniDbQuery.Posted={posted}" +
        $"&UniDbQuery.SearchPhrase=${searchPhrase}" +
        $"&UniDbQuery.year=${year}" +
        $"&UniDbQuery.serie_id=${serie_id}" +
        $"&UniDbQuery.nominal=${nominal}" +
        $"&UniDbQuery.metal_id=${metal_id}" +
        $"&UniDbQuery.tab=${tab}" +
        $"&UniDbQuery.page=${page}" +
        $"&UniDbQuery.sort=${sort}" +
        $"&UniDbQuery.sort_direction=${sort_direction}";
        return req;
    }


    public static string[] getPageLinks()
    {
        HtmlDocument doc=Request.balansedRequest(Cbr.buildSearchRequest(true, "", 2022, 0, -1, 0, 1, 1, 99, "down"));
        Console.WriteLine(doc.DocumentNode.InnerHtml);
        HtmlNodeCollection res = doc.DocumentNode.SelectNodes("//div[contains(@class, 'coins-tile_item')]");
        if (res != null)
        {
            foreach (HtmlNode el in res)
            {
                Console.WriteLine((el.SelectSingleNode("//a")).GetAttributeValue("href","none"));
            }
        }
        return null;
    }
    
    

}


