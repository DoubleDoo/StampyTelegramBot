using HtmlAgilityPack;

///<summary>
///Статический клас для сбора данных с сайта https://pochta-lnr.ru
///</summary>
public static class PochtaLnr
{
    ///<summary>
    ///Поле для хранения ссылки на ресурс в интернете
    ///</summary>
    ///<value>
    ///Ссылка на сайт
    ///</value>
    public static string linkBase = "https://pochta-lnr.ru";

    ///<summary>
    ///Поле для хранения ссылки на каталог для поиска
    ///</summary>
    ///<value>
    ///Каталог сайта
    ///</value>
    public static string catalog = linkBase + "/uslugi/filateliya/";

    ///<summary>
    ///Поле для хранения привязанного телеграм канала
    ///</summary>
    ///<value>
    ///TelegramChannel привязанный к ресурсу
    ///</value>
    public static TelegramChannel channel = new TelegramChannel("Почта ЛНР марки", "Марки выпущенные Почтой ЛНР", "pochtalnrstamps", "pochta-lnr.jpg");
    ///<summary>
    ///Статический Конструктор для создания нового объекта класса
    ///</summary>
    ///<returns>
    ///Объект класса
    ///</returns>
    static PochtaLnr() { }

    ///<summary>
    ///Функция для построения запроса к ресурсу для поиска объектов
    ///</summary>
    ///<remarks>
    ///Функция строит запрос для получения списка всех обьектов которые опубликованы ресурсом
    ///</remarks>
    ///<returns>
    ///Строка запроса
    ///</returns>
    public static string BuildRequest()
    {
        string req = catalog;
        //$"?active={sub}#{sub}{year}";
        return req;
    }

    public static string BuildLink(HtmlNode node)
    {
        string id = node.ParentNode.Id;
        string year = id.Substring(id.Length - 4, 4);
        id = id.Substring(0, id.Length - 4);
        id = "to" + char.ToUpper(id[0]) + id.Substring(1);
        return catalog + $"?active={id}#{id}{year}";
    }

    ///<summary>
    ///Функция для сбора всех сылок ведущих на объекты ресурса
    ///</summary>
    ///<returns>
    ///Список ссылок
    ///</returns>
    public static async Task<List<HtmlNode>> ScrapPage()
    {
        HtmlDocument doc = Request.BalansedRequest(BuildRequest());
        HtmlNodeCollection res = doc.DocumentNode.SelectNodes("//div[contains(@class, 'declare')]");
        List<HtmlNode> links = new List<HtmlNode>();
        //Console.WriteLine("Types: " + res.Count);
        foreach (HtmlNode node in res)
        {
            if (node.Id == "mark" | node.Id == "block" | node.Id == "series")
            {
                //Console.WriteLine("Node: "+node.Id);
                HtmlNodeCollection res2 = node.SelectNodes("./ul[contains(@class, 'sub-year')]");
                //Console.WriteLine("     Years: " + res2.Count);
                foreach (HtmlNode node2 in res2)
                {
                    //Console.WriteLine("         Year: " + node2.Id);
                    HtmlNodeCollection res3 = node2.SelectNodes("./li[contains(@class, 'product')]");
                    //Console.WriteLine("             Products: " + res3.Count);
                    foreach (HtmlNode node3 in res3)
                    {
                        links.Add(node3);
                        //Console.WriteLine(node3.InnerHtml);
                        //Console.WriteLine("...");
                        //Console.WriteLine("________________________");
                    }
                }
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
    public static async Task<List<HtmlNode>> ScrapPageNew()
    {
        Task<List<HtmlNode>> links = ScrapPage();
        Task<List<Stamp>> cns = StampRequest.Get();
        await Task.WhenAll(new Task[] { links, cns });
        List<HtmlNode> ln = links.Result;
        List<Stamp> cn = cns.Result;

        List<string> lnstring = new List<string>();


        Console.WriteLine("Found " + ln.Count + " stamps and " + cn.Count + " already exist");

        foreach (HtmlNode nd in ln)
        {
            lnstring.Add(PochtaLnrParser.GetName(nd));
        }

        if (cn.Count != 0)
            foreach (Stamp stamp in cn)
            {
                int indx = lnstring.IndexOf(stamp.Name);
                if (indx != -1)
                {
                    lnstring.RemoveAt(indx);
                    ln.RemoveAt(indx);
                }
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
    public static async Task<Stamp> Create(HtmlNode node)
    {
        Console.WriteLine("__________________________");
        Console.WriteLine(BuildLink(node));
        Console.WriteLine(PochtaLnrParser.GetName(node));
        Console.WriteLine("__________________________");
        Stamp cn = new Stamp(BuildLink(node),
                        PochtaLnrParser.GetCatalogId(node),
                        PochtaLnrParser.GetName(node),
                        PochtaLnrParser.GetSeries(node),
                        PochtaLnrParser.GetMaterial(node),
                        PochtaLnrParser.GetNominal(node),
                        PochtaLnrParser.GetCirculation(node),
                        PochtaLnrParser.GetCountry(node),
                        PochtaLnrParser.GetDate(node),
                        PochtaLnrParser.GetFirstDimention(node),
                        PochtaLnrParser.GetSecondDimention(node),
                        PochtaLnrParser.GetProtection(node),
                        PochtaLnrParser.GetPerforation(node),
                        PochtaLnrParser.GetPrintMetod(node),
                        PochtaLnrParser.GetDesign(node),
                        PochtaLnrParser.GetObverse(node)
        );
        return cn;
    }
}