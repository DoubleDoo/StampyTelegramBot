using System.Text.RegularExpressions;
using HtmlAgilityPack;


///<summary>
///Статический клас для парсинга данных с сайта https://shop.postdonbass.com
///</summary>
public static class PostdonbassParser
{
    ///<summary>
    ///Функция для получения данных для поля Stamp.Name с интернет страницы объекта
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
            HtmlNode res = doc.DocumentNode.SelectSingleNode("//h1[contains(@class, 'stamp-header')]");
            if (res != null)
            {
                return ParseName(res.InnerText);
            }
            return "0";
        }
        return "-1";
    }

    ///<summary>
    ///Функция обработки данных для поля Stamp.Name
    ///</summary>
    ///<remarks>
    ///Функция обрабатывает строковое представление информации, для дальнейшего хранения
    ///</remarks>
    ///<param name="str">
    ///Строка для обработки
    ///</param>
    public static string ParseName(string str)
    {
        Console.WriteLine(str);
        return Parser.ParseString(str);
    }

    ///<summary>
    ///Функция для получения данных для поля Stamp.Date с интернет страницы объекта
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
            HtmlNodeCollection res = doc.DocumentNode.SelectNodes("//p");
            if (res != null)
            {
                foreach (HtmlNode node in res)
                {
                    if (node.InnerText.Contains("Дата ввода в обращение:"))
                    {
                        return ParseDate(node.InnerText);
                    }
                }
            }
        }
        return new DateOnly(2222, 2, 22);
    }

    ///<summary>
    ///Функция обработки данных для поля Stamp.Date
    ///</summary>
    ///<remarks>
    ///Функция обрабатывает строковое представление информации, для дальнейшего хранения
    ///</remarks>
    ///<param name="str">
    ///Строка для обработки
    ///</param>
    public static DateOnly ParseDate(string str)
    {
        Console.WriteLine(str);
        str = str.Replace("Дата ввода в обращение:", "");
        str = str.Trim();
        return Parser.ParseDate(str);
    }

    ///<summary>
    ///Функция для получения данных для поля Stamp.Series с интернет страницы объекта
    ///</summary>
    ///<remarks>
    ///Функция ищет на странице нужные данные и преобразует в строку для их дальнейшей обработки
    ///</remarks>
    ///<param name="doc">
    ///HtmlDocument страницы с данными
    ///</param>
    public static string GetSeries(HtmlDocument doc)
    {
        return ParseSeries("-1");
    }

    ///<summary>
    ///Функция обработки данных для поля Stamp.Series
    ///</summary>
    ///<remarks>
    ///Функция обрабатывает строковое представление информации, для дальнейшего хранения
    ///</remarks>
    ///<param name="str">
    ///Строка для обработки
    ///</param>
    public static string ParseSeries(string str)
    {
        Console.WriteLine(str);
        return Parser.ParseString(str);
    }

    ///<summary>
    ///Функция для получения данных для поля Stamp.CatalogId с интернет страницы объекта
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
            HtmlNodeCollection res = doc.DocumentNode.SelectNodes("//p");
            if (res != null)
            {
                foreach (HtmlNode node in res)
                {
                    if (node.InnerText.Contains("Номер по каталогу:"))
                    {
                        return ParseCatalogId(node.InnerText);
                    }
                }
            }
            return "0";
        }
        return "-1";
    }

    ///<summary>
    ///Функция обработки данных для поля Stamp.CatalogId
    ///</summary>
    ///<remarks>
    ///Функция обрабатывает строковое представление информации, для дальнейшего хранения
    ///</remarks>
    ///<param name="str">
    ///Строка для обработки
    ///</param>
    public static string ParseCatalogId(string str)
    {
        Console.WriteLine(str);
        str = str.Replace("Номер по каталогу:", "");
        return Parser.ParseString(str);
    }

    ///<summary>
    ///Функция для получения данных для поля Stamp.Nominal с интернет страницы объекта
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
            HtmlNodeCollection res = doc.DocumentNode.SelectNodes("//p");
            if (res != null)
            {
                foreach (HtmlNode node in res)
                {
                    if (node.InnerText.Contains("Номинал:"))
                    {
                        return ParseNominal(node.InnerText);
                    }
                }
            }
            return 0;
        }
        return -1;
    }

    ///<summary>
    ///Функция обработки данных для поля Stamp.Nominal
    ///</summary>
    ///<remarks>
    ///Функция обрабатывает строковое представление информации, для дальнейшего хранения
    ///</remarks>
    ///<param name="str">
    ///Строка для обработки
    ///</param>
    public static decimal ParseNominal(string str)
    {
        Console.WriteLine(str);
        str = str.Replace("Номинал:", "");
        str = str.Replace(" ", "");
        str = str.Replace("руб.", "");
        return Parser.ParseDecimal(str);
    }

    ///<summary>
    ///Функция для получения данных для поля Stamp.Material с интернет страницы объекта
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
            HtmlNodeCollection res = doc.DocumentNode.SelectNodes("//p");
            if (res != null)
            {
                foreach (HtmlNode node in res)
                {
                    if (node.InnerText.Contains("Бумага:"))
                    {
                        return parseMaterial(node.InnerText);
                    }
                }
            }
            return "0";
        }
        return "-1";
    }

    ///<summary>
    ///Функция обработки данных для поля Stamp.Material
    ///</summary>
    ///<remarks>
    ///Функция обрабатывает строковое представление информации, для дальнейшего хранения
    ///</remarks>
    ///<param name="str">
    ///Строка для обработки
    ///</param>
    public static string parseMaterial(string str)
    {
        Console.WriteLine(str);
        str = str.Replace("Бумага:", "");
        return Parser.ParseString(str);
    }

    ///<summary>
    ///Функция для получения данных для поля Stamp.Circulation с интернет страницы объекта
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
            HtmlNodeCollection res = doc.DocumentNode.SelectNodes("//p");
            if (res != null)
            {
                foreach (HtmlNode node in res)
                {
                    if (node.InnerText.Contains("Тираж (шт.):"))
                    {
                        return ParseCirculation(node.InnerText);
                    }
                }
            }
            return 0;
        }
        return -1;
    }

    ///<summary>
    ///Функция обработки данных для поля Stamp.Circulation
    ///</summary>
    ///<remarks>
    ///Функция обрабатывает строковое представление информации, для дальнейшего хранения
    ///</remarks>
    ///<param name="str">
    ///Строка для обработки
    ///</param>
    public static long ParseCirculation(string str)
    {
        Console.WriteLine(str);
        str = str.Replace("Тираж (шт.):", "");
        str = str.Replace(" ", "");
        str = str.Replace("экз.", "");
        return Parser.ParseLong(str);
    }

    ///<summary>
    ///Функция для получения данных для поля Stamp.Obverse с интернет страницы объекта
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
            HtmlNode res = doc.DocumentNode.SelectSingleNode(".//ul[contains(@class, 'thumbnails-stamp')]/li/a");
            if (res != null)
            {
                return res.SelectSingleNode(".//img").GetAttributeValue("src", "");
            }
            return "0";
        }
        return "-1";
    }

    ///<summary>
    ///Функция для получения данных для поля Stamp.FirstDimention с интернет страницы объекта
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
            HtmlNodeCollection res = doc.DocumentNode.SelectNodes("//p");
            if (res != null)
            {
                foreach (HtmlNode node in res)
                {
                    if (node.InnerText.Contains("Формат (мм):"))
                    {
                        return ParseFirstDimention(node.InnerText);
                    }
                    else if (node.InnerText.Contains("Размер:"))
                    {
                        return ParseFirstDimention(node.InnerText);
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
    public static double ParseFirstDimention(string str)
    {
        Console.WriteLine(str);
        str = str.Split(":")[1];
        if (str.IndexOf("х") >= 0)
        {
            str = str.Split("х")[0];
        }
        else if (str.IndexOf("*") >= 0)
        {
            str = str.Split("*")[0];
        }
        else if (str.IndexOf("×") >= 0)
        {
            str = str.Split("×")[0];
        }
        else if (str.IndexOf("X") >= 0)
        {
            str = str.Split("X")[0];
        }
        Console.WriteLine(str);
        return double.Parse(Parser.ParseLong(str).ToString());
    }

    ///<summary>
    ///Функция для получения данных для поля Stamp.SecondDimention с интернет страницы объекта
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
            HtmlNodeCollection res = doc.DocumentNode.SelectNodes("//p");
            if (res != null)
            {
                foreach (HtmlNode node in res)
                {
                    if (node.InnerText.Contains("Формат (мм):"))
                    {
                        return ParseSecondDimention(node.InnerText);
                    }
                    else if (node.InnerText.Contains("Размер:"))
                    {
                        return ParseSecondDimention(node.InnerText);
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
    public static double ParseSecondDimention(string str)
    {
        Console.WriteLine(str);
        str = str.Split(":")[1];
        if (str.IndexOf("х") >= 0)
        {
            str = str.Split("х")[1];
        }
        else if (str.IndexOf("*") >= 0)
        {
            str = str.Split("*")[1];
        }
        else if (str.IndexOf("×") >= 0)
        {
            str = str.Split("×")[1];
        }
        else if (str.IndexOf("X") >= 0)
        {
            str = str.Split("X")[1];
        }
        Console.WriteLine(str);
        return double.Parse(Parser.ParseLong(str).ToString());
    }

    
    /*
    public static string GetFormat(HtmlDocument doc)
    {
        if (doc != null)
        {
            HtmlNodeCollection res = doc.DocumentNode.SelectNodes("//p");
            if (res != null)
            {
                foreach (HtmlNode node in res)
                {
                    if (node.InnerText.Contains("Формат (мм):"))
                    {
                        return ParseFormat(node.InnerText);
                    } else if (node.InnerText.Contains("Размер:"))
                    {
                        return ParseFormat(node.InnerText);
                    }
                }
            }
            return "0";
        }
        return "-1";
    }

  
    public static string ParseFormat(string str)
    {
        Console.WriteLine(str);
        str = str.Split(":")[1];
        return Parser.ParseString(str);

    }*/

    ///<summary>
    ///Функция для получения данных для поля Stamp.Protection с интернет страницы объекта
    ///</summary>
    ///<remarks>
    ///Функция ищет на странице нужные данные и преобразует в строку для их дальнейшей обработки
    ///</remarks>
    ///<param name="doc">
    ///HtmlDocument страницы с данными
    ///</param>
    public static string GetProtection(HtmlDocument doc)
    {
        if (doc != null)
        {
            HtmlNodeCollection res = doc.DocumentNode.SelectNodes("//p");
            if (res != null)
            {
                foreach (HtmlNode node in res)
                {
                    if (node.InnerText.Contains("Защита:"))
                    {
                        return ParseProtection(node.InnerText);
                    }
                }
            }
            return "0";
        }
        return "-1";
    }

    ///<summary>
    ///Функция обработки данных для поля Stamp.Protection
    ///</summary>
    ///<remarks>
    ///Функция обрабатывает строковое представление информации, для дальнейшего хранения
    ///</remarks>
    ///<param name="str">
    ///Строка для обработки
    ///</param>
    public static string ParseProtection(string str)
    {
        Console.WriteLine(str);
        str = str.Replace("Защита:", "");
        return Parser.ParseString(str);
    }

    ///<summary>
    ///Функция для получения данных для поля Stamp.Perforation с интернет страницы объекта
    ///</summary>
    ///<remarks>
    ///Функция ищет на странице нужные данные и преобразует в строку для их дальнейшей обработки
    ///</remarks>
    ///<param name="doc">
    ///HtmlDocument страницы с данными
    ///</param>
    public static string GetPerforation(HtmlDocument doc)
    {
        if (doc != null)
        {
            HtmlNodeCollection res = doc.DocumentNode.SelectNodes("//p");
            if (res != null)
            {
                foreach (HtmlNode node in res)
                {
                    if (node.InnerText.Contains("Перфорация:"))
                    {
                        return ParsePerforation(node.InnerText);
                    }
                }
            }
            return "0";
        }
        return "-1";
    }

    ///<summary>
    ///Функция обработки данных для поля Stamp.Perforation
    ///</summary>
    ///<remarks>
    ///Функция обрабатывает строковое представление информации, для дальнейшего хранения
    ///</remarks>
    ///<param name="str">
    ///Строка для обработки
    ///</param>
    public static string ParsePerforation(string str)
    {
        Console.WriteLine(str);
        str = str.Replace("Перфорация:", "");
        return Parser.ParseString(str);
    }

    ///<summary>
    ///Функция для получения данных для поля Stamp.PrintMetod с интернет страницы объекта
    ///</summary>
    ///<remarks>
    ///Функция ищет на странице нужные данные и преобразует в строку для их дальнейшей обработки
    ///</remarks>
    ///<param name="doc">
    ///HtmlDocument страницы с данными
    ///</param>
    public static string GetPrintMetod(HtmlDocument doc)
    {
        if (doc != null)
        {
            HtmlNodeCollection res = doc.DocumentNode.SelectNodes("//p");
            if (res != null)
            {
                foreach (HtmlNode node in res)
                {
                    if (node.InnerText.Contains("Способ печати:"))
                    {
                        return ParsePrintMetod(node.InnerText);
                    }
                }
            }
            return "0";
        }
        return "-1";
    }

    ///<summary>
    ///Функция обработки данных для поля Stamp.PrintMetod
    ///</summary>
    ///<remarks>
    ///Функция обрабатывает строковое представление информации, для дальнейшего хранения
    ///</remarks>
    ///<param name="str">
    ///Строка для обработки
    ///</param>
    public static string ParsePrintMetod(string str)
    {
        Console.WriteLine(str);
        str = str.Replace("Способ печати:", "");
        return Parser.ParseString(str);
    }

    ///<summary>
    ///Функция для получения данных для поля Stamp.Design с интернет страницы объекта
    ///</summary>
    ///<remarks>
    ///Функция ищет на странице нужные данные и преобразует в строку для их дальнейшей обработки
    ///</remarks>
    ///<param name="doc">
    ///HtmlDocument страницы с данными
    ///</param>
    public static string GetDesign(HtmlDocument doc)
    {
        if (doc != null)
        {
            HtmlNodeCollection res = doc.DocumentNode.SelectNodes("//p");
            if (res != null)
            {
                foreach (HtmlNode node in res)
                {
                    if (node.InnerText.Contains("Дизайнер:"))
                    {
                        return ParseDesign(node.InnerText);
                    }
                }
            }
            return "0";
        }
        return "-1";
    }

    ///<summary>
    ///Функция обработки данных для поля Stamp.Design
    ///</summary>
    ///<remarks>
    ///Функция обрабатывает строковое представление информации, для дальнейшего хранения
    ///</remarks>
    ///<param name="str">
    ///Строка для обработки
    ///</param>
    public static string ParseDesign(string str)
    {
        Console.WriteLine(str);
        str = str.Replace("Дизайнер:", "");
        return Parser.ParseString(str);
    }

    ///<summary>
    ///Функция для получения данных для поля Stamp.Country с интернет страницы объекта
    ///</summary>
    ///<remarks>
    ///Функция ищет на странице нужные данные и преобразует в строку для их дальнейшей обработки
    ///</remarks>
    ///<param name="doc">
    ///HtmlDocument страницы с данными
    ///</param>
    public static string GetCountry(HtmlDocument doc)
    {
        if (doc != null)
        {
            HtmlNodeCollection res = doc.DocumentNode.SelectNodes("//p");
            if (res != null)
            {
                foreach (HtmlNode node in res)
                {
                    if (node.InnerText.Contains("Страна выпуска:"))
                    {
                        return ParseCountry(node.InnerText);
                    }
                }
            }
            return "0";
        }
        return "-1";
    }

    ///<summary>
    ///Функция обработки данных для поля Stamp.Country
    ///</summary>
    ///<remarks>
    ///Функция обрабатывает строковое представление информации, для дальнейшего хранения
    ///</remarks>
    ///<param name="str">
    ///Строка для обработки
    ///</param>
    public static string ParseCountry(string str)
    {
        Console.WriteLine(str);
        str = str.Replace("Страна выпуска:", "");
        return Parser.ParseString(str);
    }
}