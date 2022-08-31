using HtmlAgilityPack;
using Npgsql;
using System.Timers;

public static class Timer
{
    static System.Timers.Timer processTimer;
    static System.Timers.Timer initTimer;

    static Timer()
    {
 
    }

    public static async void Process()
    {
        Console.WriteLine("Init");
        StartWaitTimer();
    }


    public static void StartWaitTimer()
    {
        SetWaitTimer();
        Console.ReadLine();
    }

    public static void StartPeriodTimer()
    {
        SetPeriodTimer();
        Console.ReadLine();
        processTimer.Stop();
        processTimer.Dispose();
    }

    private static void SetWaitTimer()
    {
        initTimer = new System.Timers.Timer(10000);
        initTimer.Elapsed += WaitFinishedEvent;
        initTimer.Enabled = true;
    }

    private static void SetPeriodTimer()
    {
        processTimer = new System.Timers.Timer(60000);
        processTimer.Elapsed += OnTimedEvent;
        processTimer.AutoReset = true;
        processTimer.Enabled = true;
    }

    private static async void OnTimedEvent(Object source, ElapsedEventArgs e)
    {
        await Cbr.getNewLinks();
        Console.WriteLine("Tick");
    }

    private static void WaitFinishedEvent(Object source, ElapsedEventArgs e)
    {
        var di = new DirectoryInfo(Environment.CurrentDirectory + @"/data/img");
        Console.WriteLine(Environment.CurrentDirectory + @"/data/img");
        if (!di.Exists)
        {
            Console.WriteLine("Created");
            di.Create();
        }
        initTimer.Stop();
        initTimer.Dispose();
        Console.WriteLine("Finished");
        StartPeriodTimer();
    }
}


