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

//TelegramChannel ch3 = new TelegramChannel("LNR Stamps", "Stamps from LNR", "lnrstamps", "pochta-lnr.jpg");
//TelegramChannel ch4 = new TelegramChannel("UA Stamps", "Stamps from UA", "uastamps", "ukrposhta.jpg");


await Process.Init();

/*
List<string> r=await Postdonbass.ScrapPage();
foreach(string m in r)
{
    Console.WriteLine(m);
}*/
await Process.Start();



/*
string filename = "10mb+";
string newfilename = "new";
string path = @"" + Environment.CurrentDirectory + "/images/"+ filename+ ".jpg"; 
string newpath = @"" + @"" + Environment.CurrentDirectory + "/images/" + newfilename + ".jpg"; 

Console.WriteLine("Start");


double length = (double)new System.IO.FileInfo(path).Length / 1024 / 1024;
Console.WriteLine(length);

int qual = 100;
int step = 1;
var img = SixLabors.ImageSharp.Image.Load(path);

double width = img.Width;
double height = img.Height;

double max = new double[] { width, height }.Max();
double diff = max / (double)1280;

width = Math.Round(img.Width/diff);
height = Math.Round(img.Height / diff);

img.Mutate(x => x.Resize((int)width, (int)height, KnownResamplers.Lanczos3));


img.Save(newpath);

Console.WriteLine("End");
*/

/*
Stamp cn1 = await Postdonbass.Create("https://shop.postdonbass.com/filateliya/pochtovyj-blok-59-chess");
Console.WriteLine(cn1.ToString());
await cn1.Load();
await cn1.Post(Postdonbass.channel);

Stamp cn2= await Postdonbass.Create("https://shop.postdonbass.com/filateliya/listy-marok-1/list-marok-167-institut-ehkonomicheskih-issledovanij-50-let");
Console.WriteLine(cn2.ToString());
await cn2.Load();
await cn2.Post(Postdonbass.channel);*/

Console.ReadLine();



