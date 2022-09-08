using System;
using WTelegram;
using TL;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Threading;
using System.Threading.Tasks;


public static class Telegram
{
    public static WTelegram.Client clnt;
    public static TL.User usr;
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

    public static async Task Login()
    {
        usr = await clnt.LoginUserIfNeeded();
        Console.WriteLine($"Logged-in as {usr.username ?? usr.first_name + " " + usr.last_name} (id {usr.id})");
    }

    public static async Task Sendmessage(Channel channel, string msg)
    {
        var entities = clnt.HtmlToEntities(ref msg);
        await clnt.SendMessageAsync((InputPeer)channel, msg, entities: entities);
    }

    public static async Task Sendmessage(Channel channel, string msg, Image[] imgs)
    {
        List<InputMedia> media = new List<InputMedia>();
        foreach (Image im in imgs)
        {

            var file = await clnt.UploadFileAsync(@"" + Environment.CurrentDirectory + "/data/img/" + im.Id + ".jpg", null);
            media.Add(new InputMediaUploadedPhoto { file = file});
        }
        var entities = clnt.HtmlToEntities(ref msg);
        await clnt.SendAlbumAsync((InputPeer)channel, media.ToArray(), msg, entities: entities);
    }

    public static async Task<List<ChatBase>> GetAllDialogs()
    {
        Messages_Dialogs chats = await clnt.Messages_GetAllDialogs();
        List<ChatBase> dialogs = new List<ChatBase>();
        foreach ((long id, ChatBase chat) in chats.chats)
        {
            dialogs.Add(chat);
        }
        return dialogs;
    }

    public static async Task<List<Channel>> GetAllChannels()
    {
        List<ChatBase> chats = await GetAllDialogs();
        List<Channel> channels = new List<Channel>();
        foreach (ChatBase chat in chats)
        {
            if (chat.GetType() == typeof(Channel))
            {
                channels.Add((Channel)chat);
            }
        }
        return channels;
    }

    public static async Task<Channel> GetChannel(long id)
    {
        List<Channel> channels = await GetAllChannels();
        foreach (Channel channel in channels)
        {
            if (channel.id == id)
            {
                return channel;
            }
        }
        return null;
    }

    public static async Task<Channel> GetChannel(string name)
    {
        List<Channel> channels = await GetAllChannels();
        foreach (Channel channel in channels)
        {
            if (channel.Title == name)
            {
                return channel;
            }
        }
        return null;
    }

    public static async Task<Channel> CreateChannel(string name, string description)
    {
        await clnt.Channels_CreateChannel(name, description);
        return await GetChannel(name);
    }

    public static async Task UpdateUsername(Channel channel, string link)
    {
        await clnt.Channels_UpdateUsername(channel, link);
    }

    public static async Task EditPhoto(Channel channel, string image)
    {
        InputFileBase fp = await clnt.UploadFileAsync(@"" + Environment.CurrentDirectory + "/images/" + image, null);
        InputChatUploadedPhoto photo = new InputChatUploadedPhoto() { file = fp };
        photo.flags = InputChatUploadedPhoto.Flags.has_file;
        await clnt.Channels_EditPhoto(channel, photo);
        // 400 PHOTO_SAVE_FILE_INVALID
    }
}
