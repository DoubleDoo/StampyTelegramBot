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
        try
        {
            HtmlNode res = doc.DocumentNode.SelectSingleNode("//h1[contains(@class, 'stamp-header')]");
            return ParseName(res.InnerText);
        }
        catch (ArgumentNullException ex)
        {
            ExceptionMessage.ExceptionString(ex, "Argument str is null");
        }
        catch (NullReferenceException ex)
        {
            ExceptionMessage.ExceptionString(ex, "HtmlNode is null (data not found)");
        }
        catch (Exception ex)
        {
            ExceptionMessage.UnknownExceptionString(ex);
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
        string buffer = str;
        try
        {
            return Parser.ParseString(buffer);
        }
        catch (ArgumentNullException ex)
        {
            ExceptionMessage.ExceptionString(ex, "Argument str is null");
        }
        catch (Exception ex)
        {
            ExceptionMessage.UnknownExceptionString(ex, str);
        }
        return "-1";
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
        try
        {
            HtmlNodeCollection res = doc.DocumentNode.SelectNodes("//p");
            foreach (HtmlNode node in res)
            {
                if (node.InnerText.Contains("Дата ввода в обращение"))
                {
                    return ParseDate(node.InnerText);
                }
            }
        }
        catch (ArgumentNullException ex)
        {
            ExceptionMessage.ExceptionString(ex, "Argument str is null");
        }
        catch (NullReferenceException ex)
        {
            ExceptionMessage.ExceptionString(ex, "HtmlNodeCollection is null (data not found)");
        }
        catch (Exception ex)
        {
            ExceptionMessage.UnknownExceptionString(ex);
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
        string buffer = str;
        try
        {
            buffer = buffer.Split(":")[1];
            buffer = buffer.Trim();
            return Parser.ParseDate(buffer);
        }
        catch (ArgumentNullException ex)
        {
            ExceptionMessage.ExceptionString(ex, "Argument str is null");
        }
        catch (Exception ex)
        {
            ExceptionMessage.UnknownExceptionString(ex, str);
        }
        return new DateOnly(2222, 2, 22);
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
        try
        {
            return ParseSeries("");
        }
        catch (ArgumentNullException ex)
        {
            ExceptionMessage.ExceptionString(ex, "Argument str is null");
        }
        catch (NullReferenceException ex)
        {
            ExceptionMessage.ExceptionString(ex, "HtmlNodeCollection is null (data not found)");
        }
        catch (Exception ex)
        {
            ExceptionMessage.UnknownExceptionString(ex);
        }
        return "-1";
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
        string buffer = str;
        try
        {
            return Parser.ParseString(buffer);
        }
        catch (ArgumentNullException ex)
        {
            ExceptionMessage.ExceptionString(ex, "Argument str is null");
        }
        catch (Exception ex)
        {
            ExceptionMessage.UnknownExceptionString(ex, str);
        }
        return "-1";
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
        try
        {
            HtmlNodeCollection res = doc.DocumentNode.SelectNodes("//p");
            foreach (HtmlNode node in res)
            {
                if (node.InnerText.Contains("Номер по каталогу"))
                {
                    return ParseCatalogId(node.InnerText);
                }
            }
        }
        catch (ArgumentNullException ex)
        {
            ExceptionMessage.ExceptionString(ex, "Argument str is null");
        }
        catch (NullReferenceException ex)
        {
            ExceptionMessage.ExceptionString(ex, "HtmlNodeCollection is null (data not found)");
        }
        catch (Exception ex)
        {
            ExceptionMessage.UnknownExceptionString(ex);
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
        string buffer = str;
        try
        {
            buffer = buffer.Split(":")[1];
            return Parser.ParseString(buffer);
        }
        catch (ArgumentNullException ex)
        {
            ExceptionMessage.ExceptionString(ex, "Argument str is null");
        }
        catch (Exception ex)
        {
            ExceptionMessage.UnknownExceptionString(ex, str);
        }
        return "-1";
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
        try
        {
            HtmlNodeCollection res = doc.DocumentNode.SelectNodes("//p");
            foreach (HtmlNode node in res)
            {
                if (node.InnerText.Contains("Номинал"))
                {
                    return ParseNominal(node.InnerText);
                }
            }
        }
        catch (ArgumentNullException ex)
        {
            ExceptionMessage.ExceptionString(ex, "Argument str is null");
        }
        catch (NullReferenceException ex)
        {
            ExceptionMessage.ExceptionString(ex, "HtmlNodeCollection is null (data not found)");
        }
        catch (Exception ex)
        {
            ExceptionMessage.UnknownExceptionString(ex);
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
        string buffer = str;
        try
        {
            buffer = buffer.Split(":")[1];
            buffer = buffer.Replace(" ", "");
            buffer = buffer.Replace("руб.", "");
            return Parser.ParseDecimal(buffer);
        }
        catch (ArgumentNullException ex)
        {
            ExceptionMessage.ExceptionString(ex, "Argument str is null");
        }
        catch (Exception ex)
        {
            ExceptionMessage.UnknownExceptionString(ex, str);
        }
        return -1;
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
        try
        {
            HtmlNodeCollection res = doc.DocumentNode.SelectNodes("//p");
            foreach (HtmlNode node in res)
            {
                if (node.InnerText.Contains("Бумага"))
                {
                    return parseMaterial(node.InnerText);
                }
            }
        }
        catch (ArgumentNullException ex)
        {
            ExceptionMessage.ExceptionString(ex, "Argument str is null");
        }
        catch (NullReferenceException ex)
        {
            ExceptionMessage.ExceptionString(ex, "HtmlNodeCollection is null (data not found)");
        }
        catch (Exception ex)
        {
            ExceptionMessage.UnknownExceptionString(ex);
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
        string buffer = str;
        try
        {
            buffer = buffer.Split(":")[1];
            return Parser.ParseString(buffer);
        }
        catch (ArgumentNullException ex)
        {
            ExceptionMessage.ExceptionString(ex, "Argument str is null");
        }
        catch (Exception ex)
        {
            ExceptionMessage.UnknownExceptionString(ex, str);
        }
        return "-1";
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
        try
        {
            HtmlNodeCollection res = doc.DocumentNode.SelectNodes("//p");
            foreach (HtmlNode node in res)
            {
                if (node.InnerText.Contains("Тираж"))
                {
                    return ParseCirculation(node.InnerText);
                }
            }
        }
        catch (ArgumentNullException ex)
        {
            ExceptionMessage.ExceptionString(ex, "Argument str is null");
        }
        catch (NullReferenceException ex)
        {
            ExceptionMessage.ExceptionString(ex, "HtmlNodeCollection is null (data not found)");
        }
        catch (Exception ex)
        {
            ExceptionMessage.UnknownExceptionString(ex);
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
        string buffer = str;
        try
        {
            buffer = buffer.Split(":")[1];
            buffer = buffer.Replace(" ", "");
            buffer = buffer.Replace("экз.", "");
            return Parser.ParseLong(buffer);
        }
        catch (ArgumentNullException ex)
        {
            ExceptionMessage.ExceptionString(ex, "Argument str is null");
        }
        catch (Exception ex)
        {
            ExceptionMessage.UnknownExceptionString(ex, str);
        }
        return -1;
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
        try
        {
            HtmlNode res = doc.DocumentNode.SelectSingleNode(".//ul[contains(@class, 'thumbnails-stamp')]/li/a");
            return res.SelectSingleNode(".//img").GetAttributeValue("src", "");
        }
        catch (ArgumentNullException ex)
        {
            ExceptionMessage.ExceptionString(ex, "Argument str is null");
        }
        catch (NullReferenceException ex)
        {
            ExceptionMessage.ExceptionString(ex, "HtmlNodeCollection is null (data not found)");
        }
        catch (Exception ex)
        {
            ExceptionMessage.UnknownExceptionString(ex);
        }
        return "-1";
    }

    ///<summary>
    ///Функция для получения данных для поля Stamp.Format с интернет страницы объекта
    ///</summary>
    ///<remarks>
    ///Функция ищет на странице нужные данные и преобразует в строку для их дальнейшей обработки
    ///</remarks>
    ///<param name="doc">
    ///HtmlDocument страницы с данными
    ///</param>
    public static string GetFormat(HtmlDocument doc)
    {
        try
        {
            HtmlNodeCollection res = doc.DocumentNode.SelectNodes("//p");
            foreach (HtmlNode node in res)
            {
                if (node.InnerText.Contains("Формат"))
                {
                    return ParseFormat(node.InnerText);
                }
            }
        }
        catch (ArgumentNullException ex)
        {
            ExceptionMessage.ExceptionString(ex, "Argument str is null");
        }
        catch (NullReferenceException ex)
        {
            ExceptionMessage.ExceptionString(ex, "HtmlNode is null (data not found)");
        }
        catch (Exception ex)
        {
            ExceptionMessage.UnknownExceptionString(ex);
        }
        return "-1";
    }

    ///<summary>
    ///Функция обработки данных для поля Stamp.Format
    ///</summary>
    ///<remarks>
    ///Функция обрабатывает строковое представление информации, для дальнейшего хранения
    ///</remarks>
    ///<param name="str">
    ///Строка для обработки
    ///</param>
    public static string ParseFormat(string str)
    {
        string buffer = str;
        try
        {
            buffer = buffer.Split(":")[1];
            return Parser.ParseString(buffer);
        }
        catch (ArgumentNullException ex)
        {
            ExceptionMessage.ExceptionString(ex, "Argument str is null");
        }
        catch (Exception ex)
        {
            ExceptionMessage.UnknownExceptionString(ex, str);
        }
        return "-1";
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
    public static string GetProtection(HtmlDocument doc)
    {
        try
        {
            HtmlNodeCollection res = doc.DocumentNode.SelectNodes("//p");
            foreach (HtmlNode node in res)
            {
                if (node.InnerText.Contains("Защита"))
                {
                    return ParseProtection(node.InnerText);
                }
            }
        }
        catch (ArgumentNullException ex)
        {
            ExceptionMessage.ExceptionString(ex, "Argument str is null");
        }
        catch (NullReferenceException ex)
        {
            ExceptionMessage.ExceptionString(ex, "HtmlNode is null (data not found)");
        }
        catch (Exception ex)
        {
            ExceptionMessage.UnknownExceptionString(ex);
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
        string buffer = str;
        try
        {
            buffer = buffer.Split(":")[1];
            return Parser.ParseString(buffer);
        }
        catch (ArgumentNullException ex)
        {
            ExceptionMessage.ExceptionString(ex, "Argument str is null");
        }
        catch (Exception ex)
        {
            ExceptionMessage.UnknownExceptionString(ex, str);
        }
        return "-1";
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
        try
        {
            HtmlNodeCollection res = doc.DocumentNode.SelectNodes("//p");
            foreach (HtmlNode node in res)
            {
                if (node.InnerText.Contains("Перфорация"))
                {
                    return ParsePerforation(node.InnerText);
                }
            }
        }
        catch (ArgumentNullException ex)
        {
            ExceptionMessage.ExceptionString(ex, "Argument str is null");
        }
        catch (NullReferenceException ex)
        {
            ExceptionMessage.ExceptionString(ex, "HtmlNode is null (data not found)");
        }
        catch (Exception ex)
        {
            ExceptionMessage.UnknownExceptionString(ex);
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
        string buffer = str;
        try
        {
            buffer = buffer.Split(":")[1] + " " + buffer.Split(":")[2];
            return Parser.ParseString(buffer);
        }
        catch (ArgumentNullException ex)
        {
            ExceptionMessage.ExceptionString(ex, "Argument str is null");
        }
        catch (Exception ex)
        {
            ExceptionMessage.UnknownExceptionString(ex, str);
        }
        return "-1";
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
        try
        {
            HtmlNodeCollection res = doc.DocumentNode.SelectNodes("//p");
            foreach (HtmlNode node in res)
            {
                if (node.InnerText.Contains("Способ печати"))
                {
                    return ParsePrintMetod(node.InnerText);
                }
            }
        }
        catch (ArgumentNullException ex)
        {
            ExceptionMessage.ExceptionString(ex, "Argument str is null");
        }
        catch (NullReferenceException ex)
        {
            ExceptionMessage.ExceptionString(ex, "HtmlNode is null (data not found)");
        }
        catch (Exception ex)
        {
            ExceptionMessage.UnknownExceptionString(ex);
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
        string buffer = str;
        try
        {
            buffer = buffer.Split(":")[1];
            return Parser.ParseString(buffer);
        }
        catch (ArgumentNullException ex)
        {
            ExceptionMessage.ExceptionString(ex, "Argument str is null");
        }
        catch (Exception ex)
        {
            ExceptionMessage.UnknownExceptionString(ex, str);
        }
        return "-1";
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
        try
        {
            HtmlNodeCollection res = doc.DocumentNode.SelectNodes("//p");
            foreach (HtmlNode node in res)
            {
                if (node.InnerText.Contains("Дизайнер"))
                {
                    return ParseDesign(node.InnerText);
                }
            }
        }
        catch (ArgumentNullException ex)
        {
            ExceptionMessage.ExceptionString(ex, "Argument str is null");
        }
        catch (NullReferenceException ex)
        {
            ExceptionMessage.ExceptionString(ex, "HtmlNode is null (data not found)");
        }
        catch (Exception ex)
        {
            ExceptionMessage.UnknownExceptionString(ex);
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
        string buffer = str;
        try
        {
            buffer = buffer.Split(":")[1];
            return Parser.ParseString(buffer);
        }
        catch (ArgumentNullException ex)
        {
            ExceptionMessage.ExceptionString(ex, "Argument str is null");
        }
        catch (Exception ex)
        {
            ExceptionMessage.UnknownExceptionString(ex, str);
        }
        return "-1";
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
        try
        {
            HtmlNodeCollection res = doc.DocumentNode.SelectNodes("//p");
            foreach (HtmlNode node in res)
            {
                if (node.InnerText.Contains("Страна выпуска"))
                {
                    return ParseCountry(node.InnerText);
                }
            }
        }
        catch (ArgumentNullException ex)
        {
            ExceptionMessage.ExceptionString(ex, "Argument str is null");
        }
        catch (NullReferenceException ex)
        {
            ExceptionMessage.ExceptionString(ex, "HtmlNode is null (data not found)");
        }
        catch (Exception ex)
        {
            ExceptionMessage.UnknownExceptionString(ex);
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
        string buffer = str;
        try
        {
            buffer = buffer.Split(":")[1];
            return Parser.ParseString(buffer);
        }
        catch (ArgumentNullException ex)
        {
            ExceptionMessage.ExceptionString(ex, "Argument str is null");
        }
        catch (Exception ex)
        {
            ExceptionMessage.UnknownExceptionString(ex, str);
        }
        return "-1";
    }
}