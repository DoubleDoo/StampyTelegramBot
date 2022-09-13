using System.Text.RegularExpressions;
using HtmlAgilityPack;


///<summary>
///Статический клас для парсинга данных с сайта https://pochta-lnr.ru/
///</summary>
public static class PochtaLnrParser
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
    public static string GetName(HtmlNode doc)
    {
        if (doc != null)
        {
            HtmlNode res = doc.SelectSingleNode(".//div[contains(@class, 'title')]");
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
        /*if(str.IndexOf("«")!=-1)
        {
            str = str.Split("«")[1];
            str = str.Replace("«", "");
            str = str.Replace("»", "");
        }*/
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
    public static DateOnly GetDate(HtmlNode doc)
    { 
        if (doc != null)
        {
            HtmlNodeCollection res = doc.SelectNodes(".//div[contains(@class, 'decl')]");
            if (res != null)
            {
                foreach (HtmlNode node in res)
                {
                    if (node.InnerText.Contains("Введение в оборот:"))
                    {
                        return ParseDate(node.SelectSingleNode("..//div[contains(@class, 'tdecl')]").InnerText);
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
    public static string GetSeries(HtmlNode doc)
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
    public static string GetCatalogId(HtmlNode doc)
    {

        if (doc != null)
        {
            HtmlNode res = doc.SelectSingleNode(".//div[contains(@class, 'sub-title')]");
            if (res != null)
            {
                return ParseCatalogId(res.InnerHtml);
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
        str = str.Replace("№", "");
        str = str.Replace("(", "");
        str = str.Replace(")", "");
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
    public static decimal GetNominal(HtmlNode doc)
    {
        
        if (doc != null)
        {
            HtmlNodeCollection res = doc.SelectNodes(".//div[contains(@class, 'decl')]");
            if (res != null)
            {
                foreach (HtmlNode node in res)
                {
                    if (node.InnerText.Contains("Номинальная цена марки:"))
                    {
                        return ParseNominal(node.SelectSingleNode("..//div[contains(@class, 'tdecl')]").InnerText);
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
    public static string GetMaterial(HtmlNode doc)
    {
        /*
        if (doc != null)
        {
            HtmlNodeCollection res = doc.SelectNodes("./p");
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
        }*/
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
    public static long GetCirculation(HtmlNode doc)
    {
        if (doc != null)
        {
            HtmlNodeCollection res = doc.SelectNodes(".//div[contains(@class, 'decl')]");
            if (res != null)
            {
                foreach (HtmlNode node in res)
                {
                    if (node.InnerText.Contains("Тираж:"))
                    {
                        return ParseCirculation(node.SelectSingleNode("..//div[contains(@class, 'tdecl')]").InnerText);
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
        str = str.Replace("шт.", "");
        str = str.Replace(" ", "");
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
    public static string GetObverse(HtmlNode doc)
    { 
        if (doc != null)
        {
            HtmlNode res = doc.SelectSingleNode(".//img");
            if (res != null)
            {
                return PochtaLnr.catalog + res.GetAttributeValue("src", "");
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
    public static double GetFirstDimention(HtmlNode doc)
    {
        if (doc != null)
        {
            HtmlNodeCollection res = doc.SelectNodes(".//div[contains(@class, 'decl')]");
            if (res != null)
            {
                foreach (HtmlNode node in res)
                {
                    if (node.InnerText.Contains("Формат марки:"))
                    {
                        return ParseFirstDimention(node.SelectSingleNode("..//div[contains(@class, 'tdecl')]").InnerText);
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
        else if (str.IndexOf("x") >= 0)
        {
            str = str.Split("x")[0];
        }
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
    public static double GetSecondDimention(HtmlNode doc)
    {
        if (doc != null)
        {
            HtmlNodeCollection res = doc.SelectNodes(".//div[contains(@class, 'decl')]");
            foreach (HtmlNode node in res)
            {
                if (node.InnerText.Contains("Формат марки:"))
                {
                    return ParseSecondDimention(node.SelectSingleNode("..//div[contains(@class, 'tdecl')]").InnerText);
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
        else if (str.IndexOf("x") >= 0)
        {
            str = str.Split("x")[1];
        }
        return double.Parse(Parser.ParseLong(str).ToString());
    }

    ///<summary>
    ///Функция для получения данных для поля Stamp.Protection с интернет страницы объекта
    ///</summary>
    ///<remarks>
    ///Функция ищет на странице нужные данные и преобразует в строку для их дальнейшей обработки
    ///</remarks>
    ///<param name="doc">
    ///HtmlDocument страницы с данными
    ///</param>
    public static string GetProtection(HtmlNode doc)
    {
        /*
        if (doc != null)
        {
            HtmlNodeCollection res = doc.SelectNodes("./p");
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
        }*/
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
    public static string GetPerforation(HtmlNode doc)
    {
        /*
        if (doc != null)
        {
            HtmlNodeCollection res = doc.SelectNodes("./p");
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
        }*/
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
    public static string GetPrintMetod(HtmlNode doc)
    {
        /*
        if (doc != null)
        {
            HtmlNodeCollection res = doc.SelectNodes("./p");
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
        }*/
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
    public static string GetDesign(HtmlNode doc)
    {
        /*
        if (doc != null)
        {
            HtmlNodeCollection res = doc.SelectNodes("./p");
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
        }*/
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
    public static string GetCountry(HtmlNode doc)
    {
        return "Луганская Народная Республика";
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
        return Parser.ParseString(str);
    }
}