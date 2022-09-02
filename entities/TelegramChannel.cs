using TL;

///<summary>
///Класс для хранения объектов марок
///</summary>
public class TelegramChannel
{
    ///<summary>
    ///Поле для наименования канала
    ///</summary>
    ///<value>
    ///Наименование объекта
    ///</value>
    public string Name { get; set; }

    ///<summary>
    ///Поле для описания канала
    ///</summary>
    ///<value>
    ///Описание объекта
    ///</value>
    public string Description { get; set; }
    ///<summary>
    ///Поле для ссылки на канал
    ///</summary>
    ///<value>
    ///Ссылка объекта
    ///</value>
    public string Link { get; set; }
    ///<summary>
    ///Поле для хранения наименования картинки канала
    ///</summary>
    ///<value>
    ///Название картинки объекта
    ///</value>
    public string Image { get; set; }

    ///<summary>
    ///Поле для хранения сущности канала в рамках WTelegramClient
    ///</summary>
    ///<value>
    ///TL.Channel объекта
    ///</value>
    public Channel Channel { get; set; }

    ///<summary>
    ///Конструктор для создания нового объекта телеграм канала
    ///</summary>
    ///<returns>
    ///Объект класса
    ///</returns>
    ///<param name="name">
    ///Наименование канала
    ///</param>
    ///<param name="description">
    ///Описание канала
    ///</param>
    ///<param name="link">
    ///Ссылка на канал
    ///</param>
    ///<param name="image">
    ///Название заготовленной картинки для канала
    ///</param>
    public TelegramChannel(string name, string description, string link, string image)
    {
        Name = name;
        Description = description;
        Link = link;
        Image = image;
    }

    ///<summary>
    ///Асинхронная функция проверяющая существует ли канала в профиле телеграм
    ///</summary>
    ///<returns>
    ///Асинхронная задача с результатом Tl.Channel
    ///</returns>
    private async Task<Channel> Check()
    {
        return await Telegram.GetChannel(Name);
    }

    ///<summary>
    ///Асинхронная функция создающая Телеграм канал
    ///</summary>
    ///<returns>
    ///Асинхронная задача с результатом Tl.Channel
    ///</returns>
    private async Task<Channel> Create()
    {
        await Telegram.CreateChannel(Name, Description);
        await Task.Delay(5000);
        Channel channel = await Telegram.GetChannel(Name);
        await Task.Delay(5000);
        await Telegram.UpdateUsername(channel, Link);
        await Task.Delay(10000);
        await Telegram.EditPhoto(channel, Image);
        await Task.Delay(10000);
        return channel;
    }

    ///<summary>
    ///Асинхронная функция проверяющая создан ли канал, и если нет, создающий его. Так же заполняет поле Channel
    ///</summary>
    ///<returns>
    ///Асинхронная задача с результатом Tl.Channel
    ///</returns>
    public async Task<Channel> Update()
    {
        Channel channel = await Check();
        if (channel == null)
        {
            channel = await Create();
        }
        Channel = channel;
        return channel;
    }

}