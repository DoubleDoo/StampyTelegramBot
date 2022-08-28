﻿using HtmlAgilityPack;
using Npgsql;
using System.Timers;

public static class Timer
{
    static System.Timers.Timer processTimer;
    static System.Timers.Timer initTimer;
    static Telegram tg;
    static Timer()
    {
        tg = new Telegram();
        //await tg.process();
    }

    public static async void Process()
    {
        Console.WriteLine("Init");
        await tg.process();
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
        initTimer = new System.Timers.Timer(60000);
        initTimer.Elapsed += WaitFinishedEvent;
        initTimer.Enabled = true;
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
        foreach (Coin cn in Cbr.getNewLinks())
        {
            tg.sendmessage(tg.getChannelDto(0), cn.Name, new Image[] { cn.Obverse, cn.Reverse });
        }
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


