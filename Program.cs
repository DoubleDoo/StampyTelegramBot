using HtmlAgilityPack;
using Npgsql;
using System.Timers;



await Telegram.process();
await PostgreSQLSingle.connectToDb();

//List<string> d =await Cbr.getFreshPageLinks();
//Console.WriteLine(d.Count);
Timer.Process();

Console.ReadLine();
