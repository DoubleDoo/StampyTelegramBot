using HtmlAgilityPack;

///<summary>
///Статический клас для сбора данных с сайта https://www.cbr.ru
///</summary>
public static class Cbr
{
    ///<summary>
    ///Поле для хранения ссылки на ресурс в интернете
    ///</summary>
    ///<value>
    ///Ссылка на сайт
    ///</value>
    public static string linkBase = "https://www.cbr.ru";

    ///<summary>
    ///Поле для хранения ссылки на каталог для поиска
    ///</summary>
    ///<value>
    ///Каталог сайта
    ///</value>
    public static string catalog = linkBase + "/cash_circulation/memorable_coins/coins_base/";

    ///<summary>
    ///Поле для хранения привязанного телеграм канала
    ///</summary>
    ///<value>
    ///TelegramChannel привязанный к ресурсу
    ///</value>
    public static TelegramChannel channel = new TelegramChannel("Центробанк Росссии монеты", "Монеты выпущенные центробанком России", "cbrcoins", "cbr.jpg");

    ///<summary>
    ///Статический Конструктор для создания нового объекта класса
    ///</summary>
    ///<returns>
    ///Объект класса
    ///</returns>
    static Cbr() { }

    ///<summary>
    ///Функция для построения запроса к ресурсу для поиска объектов
    ///</summary>
    ///<remarks>
    ///Функция строит запрос для получения списка всех обьектов которые опубликованы ресурсом
    ///</remarks>
    ///<returns>
    ///Строка запроса
    ///</returns>
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

    ///<summary>
    ///Функция для сбора всех сылок ведущих на объекты ресурса
    ///</summary>
    ///<returns>
    ///Список ссылок
    ///</returns>
    public static async Task<List<string>> ScrapPage()
    {
        HtmlDocument doc = Request.BalansedRequest(Cbr.BuildRequest(true, "", 0, 0, -1, 0, 1, 1, 99, "down"));
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

    ///<summary>
    ///Функция для фильтрации только новых ссылок на объекты
    ///</summary>
    ///<remarks>
    ///Функция собирает ссылки с ресурса и с базы данных и выявляет ссылки отсутствующие в базе данных
    ///</remarks>
    ///<returns>
    ///Список новых для базы данных ссылок
    ///</returns>
    public static async Task<List<string>> ScrapPageNew()
    {
        Task<List<string>> links = ScrapPage();
        Task<List<Coin>> cns = CoinRequest.Get();
        await Task.WhenAll(new Task[] { links, cns });
        List<string> ln = links.Result;
        List<Coin> cn = cns.Result;
        Console.WriteLine("Found "+ln.Count+ " cbr coins and " + cn.Count +" already exist");
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

    ///<summary>
    ///Функция для создания объекта по распаршеным данным с его страницы на ресурсе
    ///</summary>
    ///<returns>
    ///Объект Coin
    ///</returns>
    ///<param name="link">
    ///Сылка на объект
    ///</param>
    public static async Task<Coin> Create(string link)
    {
        HtmlDocument doc = Request.BalansedRequest(link);
        Coin cn = new Coin(link,
                          CbrParser.GetCatalogId(doc),
                          CbrParser.GetName(doc),
                          CbrParser.GetSeries(doc),
                          CbrParser.GetMaterial(doc),
                          CbrParser.GetNominal(doc),
                          CbrParser.GetCirculation(doc),
                          CbrParser.GetCountry(doc),
                          CbrParser.GetDate(doc),
                          CbrParser.GetFirstDimention(doc),
                          CbrParser.GetSecondDimention(doc),
                          CbrParser.GetObverse(doc),
                          CbrParser.GetReverse(doc)
        );
        return cn;
    }
}