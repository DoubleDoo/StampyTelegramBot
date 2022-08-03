using HtmlAgilityPack;
using Npgsql;
using System.Timers;

string path = Environment.CurrentDirectory + "/data/img/";
Console.WriteLine(Environment.CurrentDirectory + "/data/img/");
DirectoryInfo dirInfo = new DirectoryInfo(path);
if (!dirInfo.Exists)
{
    Console.WriteLine("Createddirrectory");
    dirInfo.Create();
}
else
{
    Console.WriteLine("AllreafdyExist");
}
Timer.Process();