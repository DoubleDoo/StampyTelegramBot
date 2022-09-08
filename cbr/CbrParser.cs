using System.Text.RegularExpressions;
using HtmlAgilityPack;


///<summary>
///Статический клас для парсинга данных с сайта https://www.cbr.ru
///</summary>
public static class CbrParser
{
    ///<summary>
    ///Функция для получения данных для поля Coin.Name с интернет страницы объекта
    ///</summary>
    ///<remarks>
    ///Функция ищет на странице нужные данные и преобразует в строку для их дальнейшей обработки
    ///</remarks>
    ///<param name="doc">
    ///HtmlDocument страницы с данными
    ///</param>
    public static string GetName(HtmlDocument doc)
    {
        if (doc != null)
        {
            HtmlNode res = doc.DocumentNode.SelectSingleNode("//span[contains(@class, 'referenceable')]");
            if (res != null)
            {
                return ParseName(res.InnerText);
            }
            return "0";
        }
        return "-1";
    }

    ///<summary>
    ///Функция обработки данных для поля Coin.Name
    ///</summary>
    ///<remarks>
    ///Функция обрабатывает строковое представление информации, для дальнейшего хранения
    ///</remarks>
    ///<param name="str">
    ///Строка для обработки
    ///</param>
    public static string ParseName(string str)
    {
        return Parser.ParseString(str);
    }

    ///<summary>
    ///Функция для получения данных для поля Coin.Date с интернет страницы объекта
    ///</summary>
    ///<remarks>
    ///Функция ищет на странице нужные данные и преобразует в строку для их дальнейшей обработки
    ///</remarks>
    ///<param name="doc">
    ///HtmlDocument страницы с данными
    ///</param>
    public static DateOnly GetDate(HtmlDocument doc)
    {

        if (doc != null)
        {
            HtmlNodeCollection res = doc.DocumentNode.SelectNodes("//div[contains(@class, 'money_option_title')]");
            if (res != null)
            {
                foreach (HtmlNode node in res)
                {
                    if (node.InnerText.Contains("Дата выпуска"))
                    {
                        return ParseDate(node.SelectSingleNode("..//div[contains(@class, 'money_option_value')]").InnerText);
                    }
                }
            }
        }
        return new DateOnly(2222, 2, 22);
    }

    ///<summary>
    ///Функция обработки данных для поля Coin.Date
    ///</summary>
    ///<remarks>
    ///Функция обрабатывает строковое представление информации, для дальнейшего хранения
    ///</remarks>
    ///<param name="str">
    ///Строка для обработки
    ///</param>
    public static DateOnly ParseDate(string str)
    {
        return Parser.ParseDate(str);
    }

    ///<summary>
    ///Функция для получения данных для поля Coin.Series с интернет страницы объекта
    ///</summary>
    ///<remarks>
    ///Функция ищет на странице нужные данные и преобразует в строку для их дальнейшей обработки
    ///</remarks>
    ///<param name="doc">
    ///HtmlDocument страницы с данными
    ///</param>
    public static string GetSeries(HtmlDocument doc)
    {
        if (doc != null)
        {
            HtmlNodeCollection res = doc.DocumentNode.SelectNodes("//div[contains(@class, 'commemor-coin_intro_text')]");
            if (res != null)
            {
                foreach (HtmlNode node in res)
                {
                    if (node.InnerText.Contains("Серия"))
                    {
                        return ParseSeries(node.InnerText);
                    }
                }
            }
            return "0";
        }
        return "-1";
    }

    ///<summary>
    ///Функция обработки данных для поля Coin.Series
    ///</summary>
    ///<remarks>
    ///Функция обрабатывает строковое представление информации, для дальнейшего хранения
    ///</remarks>
    ///<param name="str">
    ///Строка для обработки
    ///</param>
    public static string ParseSeries(string str)
    {
        str = str.Replace("Серия:", "");
        str = str.Replace("Cерия", "");
        return Parser.ParseString(str);
    }

    ///<summary>
    ///Функция для получения данных для поля Coin.CatalogId с интернет страницы объекта
    ///</summary>
    ///<remarks>
    ///Функция ищет на странице нужные данные и преобразует в строку для их дальнейшей обработки
    ///</remarks>
    ///<param name="doc">
    ///HtmlDocument страницы с данными
    ///</param>
    public static string GetCatalogId(HtmlDocument doc)
    {
        if (doc != null)
        {
            HtmlNodeCollection res = doc.DocumentNode.SelectNodes("//div[contains(@class, 'money_option_title')]");
            if (res != null)
            {
                foreach (HtmlNode node in res)
                {
                    if (node.InnerText.Contains("Каталожный номер"))
                    {
                        return ParseCatalogId(node.SelectSingleNode("..//div[contains(@class, 'money_option_value')]").InnerText);
                    }
                }
            }
            return "0";
        }
        return "-1";
    }

    ///<summary>
    ///Функция обработки данных для поля Coin.CatalogId
    ///</summary>
    ///<remarks>
    ///Функция обрабатывает строковое представление информации, для дальнейшего хранения
    ///</remarks>
    ///<param name="str">
    ///Строка для обработки
    ///</param>
    public static string ParseCatalogId(string str)
    {
        return Parser.ParseString(str);
    }

    ///<summary>
    ///Функция для получения данных для поля Coin.Nominal с интернет страницы объекта
    ///</summary>
    ///<remarks>
    ///Функция ищет на странице нужные данные и преобразует в строку для их дальнейшей обработки
    ///</remarks>
    ///<param name="doc">
    ///HtmlDocument страницы с данными
    ///</param>
    public static decimal GetNominal(HtmlDocument doc)
    {
        if (doc != null)
        {
            HtmlNodeCollection res = doc.DocumentNode.SelectNodes("//div[contains(@class, 'characteristic_denomenation')]");
            if (res != null)
            {
                foreach (HtmlNode node in res)
                {
                    if (node.InnerText.Contains("Номинал"))
                    {
                        return ParseNominal(node.SelectSingleNode("..//div[contains(@class, 'characteristic_value')]").InnerText);
                    }
                }
            }
            return 0;
        }
        return -1;
    }

    ///<summary>
    ///Функция обработки данных для поля Coin.Nominal
    ///</summary>
    ///<remarks>
    ///Функция обрабатывает строковое представление информации, для дальнейшего хранения
    ///</remarks>
    ///<param name="str">
    ///Строка для обработки
    ///</param>
    public static decimal ParseNominal(string str)
    {
        return Parser.ParseDecimal(str);
    }

    ///<summary>
    ///Функция для получения данных для поля Coin.FirstDimention с интернет страницы объекта
    ///</summary>
    ///<remarks>
    ///Функция ищет на странице нужные данные и преобразует в строку для их дальнейшей обработки
    ///</remarks>
    ///<param name="doc">
    ///HtmlDocument страницы с данными
    ///</param>
    public static double GetFirstDimention(HtmlDocument doc)
    {

        if (doc != null)
        {
            HtmlNodeCollection res = doc.DocumentNode.SelectNodes("//div[contains(@class, 'characteristic_denomenation')]");
            if (res != null)
            {
                foreach (HtmlNode node in res)
                {
                    if (node.InnerText.Contains("Диаметр") || node.InnerText.Contains("Длина"))
                    {
                        return ParseDimention(node.SelectSingleNode("..//div[contains(@class, 'characteristic_value')]").InnerText);
                    }
                }
            }
            return 0;
        }
        return -1;
    }

    ///<summary>
    ///Функция для получения данных для поля Coin.SecondDimention с интернет страницы объекта
    ///</summary>
    ///<remarks>
    ///Функция ищет на странице нужные данные и преобразует в строку для их дальнейшей обработки
    ///</remarks>
    ///<param name="doc">
    ///HtmlDocument страницы с данными
    ///</param>
    public static double GetSecondDimention(HtmlDocument doc)
    {
        if (doc != null)
        {
            HtmlNodeCollection res = doc.DocumentNode.SelectNodes("//div[contains(@class, 'characteristic_denomenation')]");
            if (res != null)
            {
                foreach (HtmlNode node in res)
                {
                    if (node.InnerText.Contains("Ширина"))
                    {
                        return ParseDimention(node.SelectSingleNode("..//div[contains(@class, 'characteristic_value')]").InnerText);
                    }
                }
            }
            return 0;
        }
        return -1;
    }

    ///<summary>
    ///Функция обработки данных для поля Coin.FirstDimention и Coin.SecondDimention
    ///</summary>
    ///<remarks>
    ///Функция обрабатывает строковое представление информации, для дальнейшего хранения
    ///</remarks>
    ///<param name="str">
    ///Строка для обработки
    ///</param>
    public static double ParseDimention(string str)
    {
       return Parser.ParseDouble(str);
    }

    ///<summary>
    ///Функция для получения данных для поля Coin.Material с интернет страницы объекта
    ///</summary>
    ///<remarks>
    ///Функция ищет на странице нужные данные и преобразует в строку для их дальнейшей обработки
    ///</remarks>
    ///<param name="doc">
    ///HtmlDocument страницы с данными
    ///</param>
    public static string GetMaterial(HtmlDocument doc)
    {
        if (doc != null)
        {
            HtmlNodeCollection res = doc.DocumentNode.SelectNodes("//div[contains(@class, 'characteristic_denomenation')]");
            if (res != null)
            {
                foreach (HtmlNode node in res)
                {
                    if (node.InnerText.Contains("Сплав") || node.InnerText.Contains("Материал") || node.InnerText.Contains("Металл"))
                    {
                        return parseMaterial(node.SelectSingleNode("..//div[contains(@class, 'characteristic_value')]").InnerText);
                    }
                }
                return "0";
            }
        }
        return "-1";
    }

    ///<summary>
    ///Функция обработки данных для поля Coin.Material
    ///</summary>
    ///<remarks>
    ///Функция обрабатывает строковое представление информации, для дальнейшего хранения
    ///</remarks>
    ///<param name="str">
    ///Строка для обработки
    ///</param>
    public static string parseMaterial(string str)
    {
        return Parser.ParseString(str);
    }

    ///<summary>
    ///Функция для получения данных для поля Coin.Circulation с интернет страницы объекта
    ///</summary>
    ///<remarks>
    ///Функция ищет на странице нужные данные и преобразует в строку для их дальнейшей обработки
    ///</remarks>
    ///<param name="doc">
    ///HtmlDocument страницы с данными
    ///</param>
    public static long GetCirculation(HtmlDocument doc)
    {
        if (doc != null)
        {
            HtmlNodeCollection res = doc.DocumentNode.SelectNodes("//div[contains(@class, 'characteristic_denomenation')]");
            if (res != null)
            {
                foreach (HtmlNode node in res)
                {
                    if (node.InnerText.Contains("Тираж"))
                    {
                        return ParseCirculation(node.SelectSingleNode("..//div[contains(@class, 'characteristic_value')]").InnerText);
                    }
                }
            }
            return 0;
        }
        return -1;
    }

    ///<summary>
    ///Функция обработки данных для поля Coin.Circulation
    ///</summary>
    ///<remarks>
    ///Функция обрабатывает строковое представление информации, для дальнейшего хранения
    ///</remarks>
    ///<param name="str">
    ///Строка для обработки
    ///</param>
    public static long ParseCirculation(string str)
    {
        str = str.Replace(" ", "");
        return Parser.ParseLong(str);
    }

    ///<summary>
    ///Функция для получения данных для поля Coin.Obverse с интернет страницы объекта
    ///</summary>
    ///<remarks>
    ///Функция ищет на странице нужные данные и преобразует в строку для их дальнейшей обработки
    ///</remarks>
    ///<param name="doc">
    ///HtmlDocument страницы с данными
    ///</param>
    public static string GetObverse(HtmlDocument doc)
    {
        if (doc != null)
        {
            HtmlNodeCollection res = doc.DocumentNode.SelectNodes(".//div[contains(@class, 'commemor-coin_images')]");
            if (res != null)
            {
                return Cbr.linkBase + res[0].SelectNodes(".//img")[0].GetAttributeValue("src", "");
            }
            return "0";
        }
        return "-1";
    }

    ///<summary>
    ///Функция для получения данных для поля Coin.Reverse с интернет страницы объекта
    ///</summary>
    ///<remarks>
    ///Функция ищет на странице нужные данные и преобразует в строку для их дальнейшей обработки
    ///</remarks>
    ///<param name="doc">
    ///HtmlDocument страницы с данными
    ///</param>
    public static string GetReverse(HtmlDocument doc)
    {
        if (doc != null)
        {
            HtmlNodeCollection res = doc.DocumentNode.SelectNodes(".//div[contains(@class, 'commemor-coin_images')]");
            if (res != null)
            {
                return Cbr.linkBase + res[0].SelectNodes(".//img")[1].GetAttributeValue("src", "");
            }
            return "0";
        }
        return "-1";
    }

    ///<summary>
    ///Функция для получения данных для поля Coin.Country с интернет страницы объекта
    ///</summary>
    ///<remarks>
    ///Функция ищет на странице нужные данные и преобразует в строку для их дальнейшей обработки
    ///</remarks>
    ///<param name="doc">
    ///HtmlDocument страницы с данными
    ///</param>
    public static string GetCountry(HtmlDocument doc)
    {
        return ParseCountry("Российская федерация");
    }

    ///<summary>
    ///Функция обработки данных для поля Coin.Country
    ///</summary>
    ///<remarks>
    ///Функция обрабатывает строковое представление информации, для дальнейшего хранения
    ///</remarks>
    ///<param name="str">
    ///Строка для обработки
    ///</param>
    public static string ParseCountry(string str)
    {
        return Parser.ParseString(str);
    }
}