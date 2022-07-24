// See https://aka.ms/new-console-template for more information
using HtmlAgilityPack;


string RefURL = "https://www.cbr.ru/cash_circulation/memorable_coins/coins_base/";
string myProxyIP = "193.23.50.233"; //check this is still available
int myPort = 10185;
string userId = "dodubina"; //leave it blank
string password = "7140043";

try
{
    for(int i = 0; i < 10; i++)
    {
        HtmlWeb web = new HtmlWeb();
        var doc = web.Load(RefURL.ToString(), myProxyIP, myPort, userId, password);
        Console.WriteLine(doc.DocumentNode.InnerHtml);
    }
}
catch (Exception ex)
{
    Console.WriteLine(ex.Message);
}
Console.WriteLine("Hello, World!");
Console.ReadLine();