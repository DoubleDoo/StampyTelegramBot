using HtmlAgilityPack;

public static class Request
{
    private static string myProxyIP = "213.108.196.185";
    private static int myPort = 10267;
    private static string userId = "dodubina";
    private static string password = "7140043";
    private static HtmlWeb web;

    static Request()
    {
        web = new HtmlWeb();
    }

    public static HtmlDocument BalansedRequest(string link)
    {
        try
        {
            return NonProxyRequest(link);
        }
        catch (Exception ex)
        {
            try
            {
                return ProxyRequest(link);
            }
            catch (Exception ex2)
            {
                Console.WriteLine(ex2.Message);
            }
        }
        return null;

    }

    public static HtmlDocument NonProxyRequest(string link)
    {
        HtmlDocument doc = web.Load(link);
        return doc;
    }

    public static HtmlDocument ProxyRequest(string link)
    {
        HtmlDocument doc = web.Load(link, myProxyIP, myPort, userId, password);
        return doc;
    }

    public static HtmlDocument UaProxyRequest(string link)
    {
        HtmlDocument doc = web.Load(link, "145.239.27.90", 10149, userId, password);
        return doc;
    }
    public static HtmlDocument EuProxyRequest(string link)
    {
        HtmlDocument doc = web.Load(link, "162.19.130.108", 10169, userId, password);
        return doc;
    }
}




