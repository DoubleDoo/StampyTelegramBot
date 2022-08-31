using System;
using WTelegram;
using TL;




public static  class Telegram
{
    private static WTelegram.Client clnt;
    public static TL.User usr;
    public static channelDTO[] channelsDTO = new channelDTO[] { new ("CBR Coins", "Coins from cbr.ru","cbrcoins","cbr.jpg"), 
                                                                new ("DNR Stamps", "Stamps from DNR", "dnrstamps", "dnr.jpg"), 
                                                                new ("LNR Stamps", "Stamps from LNR", "lnrstamps", "lnr.jpg"), 
                                                                new ("UA Stamps", "Stamps from UA", "uastamps", "ua.jpg") };
    private static Dictionary<channelDTO, Channel> channels = new Dictionary<channelDTO, Channel>();

    static string Config(string what)
    {
        switch (what)
        {
            case "api_id": return "12796764";
            case "api_hash": return "be2da9bf1a62b5073463a12a80e1274b";
            case "phone_number": return "+79299912242";
            case "verification_code": Console.Write("Code: "); return Console.ReadLine();
            case "session_pathname": return Environment.CurrentDirectory + "/data/WTelegram.session";
            default: return null;
        }
    }

    static Telegram()
    {
        clnt = new WTelegram.Client(Config);
    }


    public static channelDTO getChannelDto(int i)
    {
        return channelsDTO[i];
    }

    public static async Task process()
    {
        await login();
        await init();
    }

    public static async Task init()
    {
        Console.WriteLine("Init");
        List <channelDTO> missedChanels = await checkChannelsExist(channelsDTO);
        Console.WriteLine(missedChanels.Count);
        if (missedChanels.Count!=0)
        {
            await createChannels(missedChanels.ToArray<channelDTO>());
        }
        await checkChannelsExist(channelsDTO);
    }

    public static async Task login()
    {
        Console.WriteLine("Login");
        usr = await clnt.LoginUserIfNeeded();
        Console.WriteLine($"We are logged-in as {usr.username ?? usr.first_name + " " + usr.last_name} (id {usr.id})");
    }

    public static async Task<List<Channel>> getChannels()
    {
        var chats = await clnt.Messages_GetAllDialogs();
        List<TL.Channel> channelsBuf = new List<TL.Channel>();
        foreach (var (id, chat) in chats.chats)
            switch (chat)
            {
                case TL.Channel channel:
                    {
                        channelsBuf.Add(channel);
                    }
                Console.WriteLine($"{id}: Channel {channel.username}: {channel.title}");
                break;
            }
        return channelsBuf;
    }

    public static async Task<TL.Channel> getChannel(channelDTO chnl)
    {
        var chats = await clnt.Messages_GetAllDialogs();
        List<TL.Channel> channelsBuf = new List<TL.Channel>();
        foreach (var (id, chat) in chats.chats)
            switch (chat)
            {
                case TL.Channel channel:
                    {
                        if(channel.title== chnl.name)
                        {
                            return channel;
                        }
                    }
                    Console.WriteLine($"{id}: Channel {channel.username}: {channel.title}");
                    break;
            }
        return null;
    }

    public static async Task<List<channelDTO>> checkChannelsExist(channelDTO[] chnls)
    {
        List<channelDTO> missedChanels = new List<channelDTO>();
        channels.Clear();
        List<TL.Channel> channelsBuf = await getChannels();

        if (channelsBuf.Count==0)
        {
            return chnls.ToList();
        }

        bool check=false;
        foreach (channelDTO chnlDto in chnls)
        {
            foreach (Channel chnl in channelsBuf)
            {
                if (chnl.title == chnlDto.name)
                {
                    check = true;
                    Console.WriteLine("Exist:" + chnlDto.name);
                    channels.Add(chnlDto, chnl);
                }
            }
            if(!check)
            {
                Console.WriteLine("Missed:" + chnlDto.name);
                missedChanels.Add(chnlDto);
            }
            check = false;
        }
        Console.WriteLine(missedChanels.Count);
        return missedChanels;
    }

    public static async Task createChannel(channelDTO chnl)
    {
        Console.WriteLine("Create:"+ chnl.name);
        await clnt.Channels_CreateChannel(chnl.name, chnl.description);
        Channel ch = await getChannel(chnl);
        await clnt.Channels_UpdateUsername(ch, chnl.link);
        await Task.Delay(10000);

        Console.WriteLine(@"" + Environment.CurrentDirectory + "/channelsSettings/"+chnl.image);
        var fp = await clnt.UploadFileAsync(@""+Environment.CurrentDirectory + "/channelsSettings/"+ chnl.image, null);
        var photo = new InputChatUploadedPhoto() { file = fp };
        photo.flags = InputChatUploadedPhoto.Flags.has_file;
        await clnt.Channels_EditPhoto(ch, photo);
        await Task.Delay(10000);
    }


    public static async Task createChannels(channelDTO[] chnls)
    {
        var chats = await clnt.Messages_GetAllDialogs();
        foreach (channelDTO ch in chnls)
        {
            if(!channels.ContainsKey(ch))
            {
                await Task.Delay(10000);
                await createChannel(ch);
            }
        }
    }

    public static async Task sendmessage(channelDTO ch, string msg)
    {
        var entities = clnt.HtmlToEntities(ref msg);
        await clnt.SendMessageAsync((InputPeer)channels[ch], msg, entities: entities);
        Console.WriteLine("--Posted--");
    }

    public static async Task sendmessage(channelDTO ch,string msg,Image[] imgs)
    {
        List<InputMedia> media=new List<InputMedia>();
        foreach(Image im in imgs)
        {
            var file=await clnt.UploadFileAsync(@"" + Environment.CurrentDirectory + "/data/img/" + im.Id + ".jpg", null);
            media.Add(new InputMediaUploadedPhoto { file = file });
        }
        var entities = clnt.HtmlToEntities(ref msg);
        await clnt.SendAlbumAsync((InputPeer)channels[ch], media.ToArray(),msg,entities:entities);
        Console.WriteLine("--Posted p--");
    }
}
