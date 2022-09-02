using System.Timers;

public static class Process
{
    static System.Timers.Timer processTimer;
    static System.Timers.Timer initTimer;
    static bool inWork = false;

    static Process() { }

    public static async Task Init()
    {
        Console.WriteLine("Initialization...");
        await Telegram.Login();
        await PostgreSQLSingle.ConnectToDb();
        await Cbr.channel.Update();
        await Postdonbass.channel.Update();
        await UpdateImageCatalog();
        Console.WriteLine("Initialization finished");
    }

    public static async Task Start()
    {
        Console.WriteLine("Start...");
        Console.WriteLine("Waiting database initialization...");
        SetWaitTimer();
    }

    private static async Task UpdateImageCatalog()
    {
        var di = new DirectoryInfo(Environment.CurrentDirectory + @"/data/img");
        if (!di.Exists)
        {
            di.Create();
            Console.WriteLine(Environment.CurrentDirectory + @"/data/img" + " Created");
        }
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
            Console.WriteLine("Refreshing...");
            await getTest();
            Console.WriteLine("Done");
            inWork = false;
        }
    }

    private static void WaitFinishedEvent(Object source, ElapsedEventArgs e)
    {
        initTimer.Stop();
        initTimer.Dispose();
        Console.WriteLine("Done");
        StartPeriodTimer();
    }


    public static async Task getTest()
    {
        Queue<string> cbr = new Queue<string>(await Cbr.ScrapPageNew());
        Queue<string> postdonbass = new Queue<string>(await Postdonbass.ScrapPageNew());

        List<Coin> coins = new List<Coin>();
        List<Stamp> stamps = new List<Stamp>();

        int i = 0;
        int total = cbr.Count+postdonbass.Count;
        while (cbr.Count + postdonbass.Count > 0)
        {
            List<Task> tasks = new List<Task>();
            if (cbr.Count > 0)
            {
                i++;
                tasks.Add(Cbr.Create(cbr.Dequeue()));
            }
            if (postdonbass.Count > 0)
            {
                i++;
                tasks.Add(Postdonbass.Create(postdonbass.Dequeue()));
            }
            Console.WriteLine(i + ":" + total);
            await Task.WhenAll(tasks.ToArray());

            foreach (Task task in tasks)
            {
                if (task.GetType().ToString().IndexOf("Stamp") >= 0)
                {
                    Stamp stamp = ((Task<Stamp>)task).Result;
                    Console.WriteLine(stamp.ToString());
                    stamps.Add(stamp);
                }
                if (task.GetType().ToString().IndexOf("Coin") >= 0)
                {
                    Coin coin = ((Task<Coin>)task).Result;
                    Console.WriteLine(coin.ToString());
                    coins.Add(coin);
                }
            }
            Console.WriteLine("____________________");
        }

        Console.WriteLine("Итого марок : "+stamps.Count);
        Console.WriteLine("Итого монет : " + coins.Count);

        i = 0;
        total = coins.Count;
        foreach (Coin coin in coins)
        {
            List<Task> tsk = new List<Task>();
            i++;
            tsk.Add(coin.Post(Cbr.channel));
            tsk.Add(coin.Load());
            Console.WriteLine(i + ":" + total);
            await Task.WhenAll(tsk.ToArray());
            await Task.Delay(1000);
        }
        i = 0;
        total = postdonbass.Count;
        foreach (Stamp stamp in stamps)
        {
            List<Task> tsk = new List<Task>();
            i++;
            tsk.Add(stamp.Post(Postdonbass.channel));
            tsk.Add(stamp.Load());
            Console.WriteLine(i + ":" + total);
            await Task.WhenAll(tsk.ToArray());
            await Task.Delay(1000);
        }
    }
}


