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
        initTimer = new System.Timers.Timer(30000);
        initTimer.Elapsed += WaitFinishedEvent;
        initTimer.Enabled = true;
    }

    private static void SetPeriodTimer()
    {
        processTimer = new System.Timers.Timer(30000);
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
        //Queue<string> cbr = new Queue<string>();
        Queue<string> postdonbass = new Queue<string>(await Postdonbass.ScrapPageNew());

        List<Coin> coins = new List<Coin>();
        List<Stamp> stamps = new List<Stamp>();

        List<string> errorParse = new List<string>();
        List<string> errorPublic = new List<string>();

        int i = 0;
        int total = cbr.Count + postdonbass.Count;
        List<Task> tasks = new List<Task>();
        while (cbr.Count + postdonbass.Count > 0)
        {
            if (cbr.Count > 0)
            {
                i++;
                string ss = cbr.Dequeue();
                Console.WriteLine(ss);
                try
                {
                    tasks.Add(Cbr.Create(ss));
                }
                catch
                {
                    errorParse.Add(ss);
                }
            }
            if (postdonbass.Count > 0)
            {
                i++;
                string sss = postdonbass.Dequeue();
                Console.WriteLine(sss);
                try
                {
                    tasks.Add(Postdonbass.Create(sss));
                }
                catch
                {
                    errorParse.Add(sss);
                }
            }
            Console.WriteLine(i + ":" + total);
            await Task.WhenAll(tasks.ToArray());
            Console.WriteLine("____________________");
        }
        foreach (Task task in tasks)
        {
            Console.WriteLine("Осталось элементов : " + (tasks.Count - stamps.Count - coins.Count) + "/" + tasks.Count);
            if (task.GetType().ToString().IndexOf("Stamp") >= 0)
            {
                Stamp stamp = ((Task<Stamp>)task).Result;
                try
                {
                    await ProcessStamp(stamp, Postdonbass.channel);
                }
                catch
                {
                    errorPublic.Add(stamp.Link);
                }
                stamps.Add(stamp);
            }
            if (task.GetType().ToString().IndexOf("Coin") >= 0)
            {
                Coin coin = ((Task<Coin>)task).Result;
                try
                {
                    await ProcessCoin(coin, Cbr.channel);
                }
                catch
                {
                    errorPublic.Add(coin.Link);
                }
                coins.Add(coin);
            }
            Console.WriteLine("____________________");
        }
        Console.WriteLine("Ошибки публикации");
        foreach (string error in errorPublic)
        {
            Console.WriteLine(error);
        }
        Console.WriteLine("Ошибки парсинга");
        foreach (string error in errorPublic)
        {
            Console.WriteLine(error);
        }
    }

    public static async Task ProcessCoin(Coin coin, TelegramChannel ch)
    {
        Console.WriteLine(coin.ToString());
        await coin.Load();
        await coin.Post(ch);
        await Task.Delay(1500);
    }

    public static async Task ProcessStamp(Stamp stamp, TelegramChannel ch)
    {
        Console.WriteLine(stamp.ToString());
        await stamp.Load();
        await stamp.Post(ch);
        await Task.Delay(1500);
    }

}


