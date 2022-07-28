using HtmlAgilityPack;
using Npgsql;


Cbr.getPageLinks();

/*
Console.WriteLine(Request.proxyRequest("https://www.cbr.ru/cash_circulation/memorable_coins/coins_base/ShowCoins/?cat_num=5714-0084"));
Console.WriteLine(Request.nonProxyRequest("https://www.cbr.ru/cash_circulation/memorable_coins/coins_base/ShowCoins/?cat_num=5714-0084"));
Console.WriteLine(Request.balansedRequest("https://www.cbr.ru/cash_circulation/memorable_coins/coins_base/ShowCoins/?cat_num=5714-0084"));
*/


/*
Console.WriteLine("Coins:_______________________________");

string name = "г. Рыльск, Курская область";
DateOnly date = new DateOnly(2022,07,14);
string series = "Древние города России";
string catalogId = "5714-0084";
decimal nominal = 10;
double diameter = 27.0;
string metal = "сталь с латунным гальваническим покрытием/сталь с никелевым гальваническим покрытием";
int circulation = 1000000;
string obverse = "https://www.cbr.ru/legacy/PhotoStore/img/5714-0084.jpg";
string reverse = "https://www.cbr.ru/legacy/PhotoStore/img/5714-0084r.jpg";
string link = "https://www.cbr.ru/cash_circulation/memorable_coins/coins_base/ShowCoins/?cat_num=5714-0084";

Console.WriteLine("CREATE:");
Coin obj = CoinRequest.createCoin(name, date, series, catalogId, nominal, diameter, metal, circulation, obverse, reverse, link); ;
Console.WriteLine(obj.ToString());


Console.WriteLine("LIST:");
List<Coin> lst = CoinRequest.getCoins();
lst.ForEach(x => { Console.WriteLine(x.ToString()); });


Console.WriteLine("BYID:");
Coin res = CoinRequest.getCoin(obj.Id);
Console.WriteLine(res.ToString());
Console.WriteLine("Coins:END_______________________________");


Console.WriteLine("IMAGES:_______________________________");

Console.WriteLine("CREATE:");
 Image obj2 = ImageRequest.createImage("linklink");
 Console.WriteLine(obj2.ToString());

 Console.WriteLine("LIST:");
 List<Image> lst2=ImageRequest.getImages();
 lst2.ForEach(x => { Console.WriteLine(x.ToString()); });

 Console.WriteLine("BYID:");
 Image res2 = ImageRequest.getImage(obj2.Id);
 Console.WriteLine(res2.ToString());
 Console.ReadLine();
Console.WriteLine("IMAGES:END_______________________________");
*/