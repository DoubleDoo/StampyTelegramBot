using HtmlAgilityPack;
using Npgsql;
using System.Timers;

public static class Timer
{
    static System.Timers.Timer processTimer;
    static System.Timers.Timer initTimer;
    static bool inWork=false;

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
        processTimer = new System.Timers.Timer(10000);
        processTimer.Elapsed += OnTimedEvent;
        processTimer.AutoReset = true;
        processTimer.Enabled = true;
    }

    private static async void OnTimedEvent(Object source, ElapsedEventArgs e)
    {
        if (!inWork)
        {
            inWork = true;
            await getTest();
            Console.WriteLine("Tick");
            inWork = false;
        }
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


    public static async Task<Stamp> getstamp()
    {
        return new Stamp("abc", "swd");
    }

    public static async Task getTest()
    {
        Queue<string> a = new Queue<string>(await Cbr.getFreshPageLinks());

        List<Coin> cl = new List<Coin>();
        List<Stamp> st = new List<Stamp>();

        int i = 0;
        int total = a.Count;
        while (a.Count > 0)
        {
            List<Task> tsk = new List<Task>();
            if (a.Count > 0)
            {
                i++;
                tsk.Add(Cbr.getObjDataFromLink(a.Dequeue()));
            }
            tsk.Add(getstamp());

            Console.WriteLine(i + ":" + total);
            await Task.WhenAll(tsk.ToArray());

            foreach (Task t in tsk)
            {
                if (t.GetType().ToString().IndexOf("Stamp") >= 0)
                {
                    Stamp stmp = ((Task<Stamp>)t).Result;
                    Console.WriteLine(stmp.ToString());
                    st.Add(stmp);
                }
                if (t.GetType().ToString().IndexOf("Coin") >= 0)
                {
                    Coin cn = ((Task<Coin>)t).Result;
                    Console.WriteLine(cn.ToString());
                    cl.Add(cn);
                }
            }
            Console.WriteLine("____________________");
        }
        Console.WriteLine(st.Count);
        Console.WriteLine(cl.Count);


        i = 0;
        total = cl.Count;
        foreach (Coin s in cl)
        {
            List<Task> tsk = new List<Task>();
            i++;
            tsk.Add(s.post(Telegram.getChannelDto(0)));
            tsk.Add(s.load());
            Console.WriteLine(i + ":" + total);
            await Task.WhenAll(tsk.ToArray());
            await Task.Delay(1000);
        }


        i = 0;
        total = st.Count;
        foreach (Stamp s in st)
        {
            List<Task> tsk = new List<Task>();
            i++;
            tsk.Add(s.post(Telegram.getChannelDto(1)));
            tsk.Add(s.load());
            Console.WriteLine(i + ":" + total);
            await Task.WhenAll(tsk.ToArray());
            await Task.Delay(1000);
        }
    }
}


