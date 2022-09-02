using System.Text.RegularExpressions;
using HtmlAgilityPack;


///<summary>
///Статический клас для парсинга данных с сайта https://shop.postdonbass.com
///</summary>
public static class PostdonbassParser
{
    ///<summary>
    ///Статический Конструктор для создания нового объекта класса
    ///</summary>
    ///<returns>
    ///Объект класса
    ///</returns>
    static PostdonbassParser() { }

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
        HtmlNodeCollection res = doc.DocumentNode.SelectNodes("//h1[contains(@class, 'stamp-header')]");
        if (res != null)
        return ParseName(res[0].InnerText);

        return "";
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
        try
        {
            return str.Trim();
        }
        catch (Exception e)
        {
            return "";
        }
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
        HtmlNodeCollection res = doc.DocumentNode.SelectNodes("//p");
        foreach (HtmlNode node in res)
        {
            if (node.InnerText.Contains("Дата ввода в обращение"))
            {
                return ParseDate(node.InnerText);
            }
        }
        return ParseDate("");
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
        try
        {
            str = str.Split(":")[1];
            str = str.Trim();
            string[] spt = str.Split(".");
            return new DateOnly(int.Parse(spt[2]), int.Parse(spt[1]), int.Parse(spt[0]));
        }
        catch (Exception e)
        {
            return new DateOnly(2222, 02, 22);
        }
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
        return ParseSeries("");
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
        return str;
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
        HtmlNodeCollection res = doc.DocumentNode.SelectNodes("//p");
        foreach (HtmlNode node in res)
        {
            if (node.InnerText.Contains("Номер по каталогу"))
            {
                return ParseCatalogId(node.InnerText);
            }
        }
        return "";
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
        try
        {
            str = str.Split(":")[1];
            return str.Trim();
        }
        catch (Exception e)
        {
            return "missed";
        }
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
        HtmlNodeCollection res = doc.DocumentNode.SelectNodes("//p");
        foreach (HtmlNode node in res)
        {
            if (node.InnerText.Contains("Номинал"))
            {
                return ParseNominal(node.InnerText);
            }
        }
        return ParseNominal("");
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
        try
        {
            str = str.Split(":")[1];
            str = str.Replace(" ", "");
            str = str.Replace("руб.", "");
            return decimal.Parse(str);
        }
        catch (Exception e)
        {
            return -999;
        }
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
        HtmlNodeCollection res = doc.DocumentNode.SelectNodes("//p");
        foreach (HtmlNode node in res)
        {
            if (node.InnerText.Contains("Бумага"))
            {
                return parseMaterial(node.InnerText);
            }
        }
        return "";
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
        try
        {
            str = str.Split(":")[1];
            return str.Trim();
        }
        catch (Exception e)
        {
            return "missed";
        }
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
        HtmlNodeCollection res = doc.DocumentNode.SelectNodes("//p");
        foreach (HtmlNode node in res)
        {
            if (node.InnerText.Contains("Тираж"))
            {
                return ParseCirculation(node.InnerText);
            }
        }
        return ParseCirculation("");
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
        try
        {
            str = str.Split(":")[1];
            str = str.Replace(" ", "");
            str = str.Replace("экз.", "");
            return long.Parse(str);
        }
        catch(Exception e)
        {
            return -999;
        }
        //Regex regex = new Regex(@"\d(\d|\s)+");
        //MatchCollection matches = regex.Matches(str);
        //str = matches[0].Value.Replace(" ", "");
        //return long.Parse(str);
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
        HtmlNodeCollection res2 = doc.DocumentNode.SelectNodes(".//ul[contains(@class, 'thumbnails-stamp')]/li/a");
        if (res2 != null)
        {
            return res2[0].SelectNodes(".//img")[0].GetAttributeValue("src", "");
        }
        return "";
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
        HtmlNodeCollection res = doc.DocumentNode.SelectNodes("//p");
        foreach (HtmlNode node in res)
        {
            if (node.InnerText.Contains("Формат"))
            {
                return ParseFormat(node.InnerText);
            }
        }
        return "";
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
        try
        {
            str = str.Split(":")[1];
            return str;
        }
        catch (Exception e)
        {
            return "missed";
        }
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
        HtmlNodeCollection res = doc.DocumentNode.SelectNodes("//p");
        foreach (HtmlNode node in res)
        {
            if (node.InnerText.Contains("Защита"))
            {
                return ParseProtection(node.InnerText);
            }
        }
        return "";
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
        try
        {
            str = str.Split(":")[1];
            return str.Trim();
        }
        catch (Exception e)
        {
            return "missed";
        }
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
        HtmlNodeCollection res = doc.DocumentNode.SelectNodes("//p");
        foreach (HtmlNode node in res)
        {
            if (node.InnerText.Contains("Перфорация"))
            {
                return ParsePerforation(node.InnerText);
            }
        }
        return "";
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
        try
        {
            str = str.Split(":")[1] + " " + str.Split(":")[2];
            return str.Trim();
        }
        catch (Exception e)
        {
            return "missed";
        }
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
        HtmlNodeCollection res = doc.DocumentNode.SelectNodes("//p");
        foreach (HtmlNode node in res)
        {
            if (node.InnerText.Contains("Способ печати"))
 
            {
                return ParsePrintMetod(node.InnerText);
            }
        }
        return "";
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
        try
        {
            str = str.Split(":")[1];
            return str.Trim();
        }
        catch (Exception e)
        {
            return "missed";
        }
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
        HtmlNodeCollection res = doc.DocumentNode.SelectNodes("//p");
        foreach (HtmlNode node in res)
        {
            if (node.InnerText.Contains("Дизайнер"))
            {
                return ParseDesign(node.InnerText);
            }
        }
        return "";
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
        try
        {
            str = str.Split(":")[1];
            return str.Trim();
        }
        catch (Exception e)
        {
            return "missed";
        }
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
        HtmlNodeCollection res = doc.DocumentNode.SelectNodes("//p");
        foreach (HtmlNode node in res)
        {
            if (node.InnerText.Contains("Страна выпуска"))
            {
                return ParseDesign(node.InnerText);
            }
        }
        return "";
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
        try
        {
            str = str.Split(":")[1];
            return str.Trim();
        }
        catch (Exception e)
        {
            return "missed";
        }
    }
}