using HtmlAgilityPack;
using Npgsql;
using System.Timers;



await Telegram.process();
await PostgreSQLSingle.connectToDb();

Timer.Process();

Console.ReadLine();
