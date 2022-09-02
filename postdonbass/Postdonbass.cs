using HtmlAgilityPack;

///<summary>
///Статический клас для сбора данных с сайта https://shop.postdonbass.com
///</summary>
public static class Postdonbass
{
    ///<summary>
    ///Поле для хранения ссылки на ресурс в интернете
    ///</summary>
    ///<value>
    ///Ссылка на сайт
    ///</value>
    public static string linkBase = "https://shop.postdonbass.com";

    ///<summary>
    ///Поле для хранения ссылки на каталог для поиска
    ///</summary>
    ///<value>
    ///Каталог сайта
    ///</value>
    public static string catalog = linkBase + "/filateliya";

    ///<summary>
    ///Поле для хранения привязанного телеграм канала
    ///</summary>
    ///<value>
    ///TelegramChannel привязанный к ресурсу
    ///</value>
    public static TelegramChannel channel = new TelegramChannel("Почта Донбасса марки", "Марки выпущенные Почтой Донбасса", "postdonbassstamps", "postdonbass.jpg");

    ///<summary>
    ///Статический Конструктор для создания нового объекта класса
    ///</summary>
    ///<returns>
    ///Объект класса
    ///</returns>
    static Postdonbass() { }

    ///<summary>
    ///Функция для построения запроса к ресурсу для поиска объектов
    ///</summary>
    ///<remarks>
    ///Функция строит запрос для получения списка всех обьектов которые опубликованы ресурсом
    ///</remarks>
    ///<returns>
    ///Строка запроса
    ///</returns>
    public static string BuildRequest(string category, int limit, int page)
    {
        string req = catalog +
        $"/{category}" +
        $"/god-vvoda-v-oborot/2021"+//убрать
        $"?limit={limit}&page={page}";
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
        List<string> links = new List<string>();
        int i = 0;
        bool go = true;
        foreach (string req in new string[]{ "listy-marok-1", "marki", "Почтовые_блоки", "scepki-marok" })
        {
            i = 0;
            go = true;
            while (true)
            {
                i++;
                HtmlDocument doc = Request.BalansedRequest(BuildRequest(req, 24, i));
                HtmlNodeCollection res = doc.DocumentNode.SelectNodes("//div[contains(@class, 'caption')]/h4/a");
                if (res!=null)
                {
                    foreach (HtmlNode el in res)
                    {
                        string atr = el.GetAttributeValue("href", "");
                        if (atr != "")
                        { links.Add(atr.Split("?")[0]); }
                    }
                }
                else break;
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
        Task<List<Stamp>> cns = StampRequest.Get();
        await Task.WhenAll(new Task[] { links, cns });
        List<string> ln = links.Result;
        List<Stamp> cn = cns.Result;

        if (cn.Count != 0)
            foreach (Stamp coin in cn)
            {
                int indx = ln.IndexOf(coin.Link);
                if (indx != -1)
                {
                    ln.RemoveAt(indx);
                }
            }
        //int dx = ln.IndexOf("https://www.cbr.ru/cash_circulation/memorable_coins/coins_base/ShowCoins/?cat_num=3213-0010");
        //if (dx != -1)
        //{
        //    ln.RemoveAt(dx);
        //}
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
    public static async Task<Stamp> Create(string link)
    {
        HtmlDocument doc = Request.BalansedRequest(link);
        Stamp cn = new Stamp(link,
                          PostdonbassParser.GetCatalogId(doc),
                          PostdonbassParser.GetName(doc),
                          PostdonbassParser.GetSeries(doc),
                          PostdonbassParser.GetMaterial(doc),
                          PostdonbassParser.GetNominal(doc),
                          PostdonbassParser.GetCirculation(doc),
                          PostdonbassParser.GetDate(doc),
                          PostdonbassParser.GetFormat(doc),
                          PostdonbassParser.GetProtection(doc),
                          PostdonbassParser.GetPerforation(doc),
                          PostdonbassParser.GetPrintMetod(doc),
                          PostdonbassParser.GetDesign(doc),
                          PostdonbassParser.GetCountry(doc),
                          PostdonbassParser.GetObverse(doc)
        );
        return cn;
    }
}