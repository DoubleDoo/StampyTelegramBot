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

    public static void Process()
    {
        Console.WriteLine("Init");
        StartWaitTimer();
        Console.ReadLine();
    }


    public static void StartWaitTimer()
    {
        SetWaitTimer();
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
        processTimer = new System.Timers.Timer(5000);
        processTimer.Elapsed += WaitFinishedEvent;
        processTimer.Enabled = true;
    }

    private static void SetPeriodTimer()
    {
        processTimer = new System.Timers.Timer(10000);
        processTimer.Elapsed += OnTimedEvent;
        processTimer.AutoReset = true;
        processTimer.Enabled = true;
    }

    private static void OnTimedEvent(Object source, ElapsedEventArgs e)
    {
        Cbr.getNewLinks();
        Console.WriteLine("Tick");
    }

    private static void WaitFinishedEvent(Object source, ElapsedEventArgs e)
    {
        processTimer.Stop();
        processTimer.Dispose();
        Console.WriteLine("Finished");
        StartPeriodTimer();
    }
}


