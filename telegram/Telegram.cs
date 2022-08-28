using System;
using WTelegram;
using TL;


public struct channelDTO
{
    public string name;
    public string description;
    public string link;
    public string image;

    public channelDTO(string nm,string dsc,string lnk,string img) : this()
    {
        name = nm;
        description = dsc;
        link = lnk;
        image = img;
    }
}

public class Telegram
{
    static WTelegram.Client clnt;
    static TL.User usr;
    static channelDTO[] channelsDTO = new channelDTO[] { new ("CBR Coins", "Coins from cbr.ru","cbrcoins","cbr.jpg"), new ("DNR Stamps", "Stamps from DNR", "dnrstamps", "dnr.jpg"), new ("LNR Stamps", "Stamps from LNR", "lnrstamps", "lnr.jpg"), new ("UA Stamps", "Stamps from UA", "uastamps", "ua.jpg") };
    static Dictionary<channelDTO, Channel> channels = new Dictionary<channelDTO, Channel>();

    string Config(string what)
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

    public Telegram()
    {
        clnt = new WTelegram.Client(Config);
    }

    public channelDTO getChannelDto(int i)
    {
        return channelsDTO[i];
    }

    public async Task process()
    {
        await this.login();
        await this.init();
        //await this.test();
    }

    public async Task init()
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

    public async Task login()
    {
        Console.WriteLine("Login");
        usr = await clnt.LoginUserIfNeeded();
        Console.WriteLine($"We are logged-in as {usr.username ?? usr.first_name + " " + usr.last_name} (id {usr.id})");
    }

    public async Task<List<Channel>> getChannels()
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

    public async Task<TL.Channel> getChannel(channelDTO chnl)
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

    public async Task<List<channelDTO>> checkChannelsExist(channelDTO[] chnls)
    {
        List<channelDTO> missedChanels = new List<channelDTO>();
        channels.Clear();
        List<TL.Channel> channelsBuf = await this.getChannels();

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

    public async Task createChannel(channelDTO chnl)
    {
        Console.WriteLine("Create:"+ chnl.name);
        await clnt.Channels_CreateChannel(chnl.name, chnl.description);
        Channel ch = await this.getChannel(chnl);
        await clnt.Channels_UpdateUsername(ch, chnl.link);

        Console.WriteLine(@"" + Environment.CurrentDirectory + "/channelsSettings/"+chnl.image);
        var fp = await clnt.UploadFileAsync(@""+Environment.CurrentDirectory + "/channelsSettings/"+ chnl.image, null);
        var photo = new InputChatUploadedPhoto() { file = fp };
        photo.flags = InputChatUploadedPhoto.Flags.has_file;
        await clnt.Channels_EditPhoto(ch, photo);
    }


    public async Task createChannels(channelDTO[] chnls)
    {
        var chats = await clnt.Messages_GetAllDialogs();
        foreach (channelDTO ch in chnls)
        {
            if(!channels.ContainsKey(ch))
            {
                await createChannel(ch);
            }
        }
    }

    public async void sendmessage(channelDTO ch, string msg)
    {
        var uploadedFile = await clnt.UploadFileAsync(@"" + Environment.CurrentDirectory + "/channelsSettings/" + ch.image, null);
        var uploadedFile2 = await clnt.UploadFileAsync(@"" + Environment.CurrentDirectory + "/channelsSettings/test.jpg", null);

        var inputMedias = new InputMedia[]
        {
            new InputMediaUploadedPhoto { file = uploadedFile },
             new InputMediaUploadedPhoto { file = uploadedFile2},
        };
        await clnt.SendAlbumAsync((InputPeer)channels[ch], inputMedias, msg);
        //await clnt.SendMessageAsync((InputPeer)channels[ch], msg, inputMedias[0]);
    }

    public async void sendmessage(channelDTO ch,string msg,Image[] imgs)
    {
        List<InputMedia> media=new List<InputMedia>();
        foreach(Image im in imgs)
        {
            var file=await clnt.UploadFileAsync(@"" + Environment.CurrentDirectory + "/data/img/" + im.Id + ".jpg", null);
            media.Add(new InputMediaUploadedPhoto { file = file });
        }
       
        await clnt.SendAlbumAsync((InputPeer)channels[ch], media.ToArray(), msg);
    }

    public async Task test()
    {
        Console.WriteLine("Test");
        foreach (channelDTO ch in channelsDTO)
        {
            sendmessage(ch, "Test msg to " + ch.name);
        }
    }
}
