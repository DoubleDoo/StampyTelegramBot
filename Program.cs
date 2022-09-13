using HtmlAgilityPack;
using Npgsql;
using System.Net;
using System.Timers;
using System.Drawing.Imaging;
using TL;
using WTelegram;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using SixLabors.ImageSharp.Formats.Jpeg;


//docker-compose run stampytelegrambot
HtmlDocument doc = Request.BalansedRequest("https://postmark.ukrposhta.ua/index.php?route=product/category&path=83");
Console.WriteLine(doc.DocumentNode.InnerHtml);
foreach (HtmlNode row in doc.DocumentNode.SelectNodes(@"//a"))
{
    Console.WriteLine(row.InnerHtml);
}

//await Process.Init();
//await Process.Start();


//List<string> lst= await Ukrposhta.ScrapPage();
//foreach(string node in lst)
//{
//Console.WriteLine(node);
//await node.Load();
//await node.Post(PochtaLnr.channel);
//await Task.Delay(1500);
//}


/*
Stamp cn1 = await Postdonbass.Create("https://shop.postdonbass.com/filateliya/pochtovyj-blok-59-chess");
Console.WriteLine(cn1.ToString());
await cn1.Load();
await cn1.Post(Postdonbass.channel);

Stamp cn2= await Postdonbass.Create("https://shop.postdonbass.com/filateliya/listy-marok-1/list-marok-167-institut-ehkonomicheskih-issledovanij-50-let");
Console.WriteLine(cn2.ToString());
await cn2.Load();
await cn2.Post(Postdonbass.channel);
*/
Console.ReadLine();



