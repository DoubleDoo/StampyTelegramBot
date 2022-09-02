///<summary>
///Класс для хранения объектов марок
///</summary>
public class Stamp : Collectable, IImageLoader
{

    ///<summary>
    ///Поле для хранения формата
    ///</summary>
    ///<value>
    ///Формат объекта
    ///</value>
    public string Format { get; set; }

    ///<summary>
    ///Поле для хранения защиты
    ///</summary>
    ///<value>
    ///Защита объекта
    ///</value>
    public string Protection { get; set; }

    ///<summary>
    ///Поле для хранения перфорации
    ///</summary>
    ///<value>
    ///Перфорация объекта
    ///</value>
    public string Perforation { get; set; }

    ///<summary>
    ///Поле для хранения метода печати
    ///</summary>
    ///<value>
    ///Метод печати объекта
    ///</value>
    public string PrintMetod { get; set; }

    ///<summary>
    ///Поле для хранения дизайна
    ///</summary>
    ///<value>
    ///Дизайн объекта
    ///</value>
    public string Design { get; set; }

    ///<summary>
    ///Поле для хранения страны
    ///</summary>
    ///<value>
    ///Страна объекта
    ///</value>
    public string Country { get; set; }

    ///<summary>
    ///Поле для хранения изображения оборотной стороны объекта
    ///</summary>
    ///<value>
    ///Изображение оборотной стороны объекта
    ///</value>
    public Image Obverse { get; set; }

    ///<summary>
    ///Поле для хранения GUID изображения оборотной стороны объекта
    ///</summary>
    ///<value>
    ///GUID изображения оборотной стороны объекта
    ///</value>
    ///<remarks>
    ///Нужен исключительно для последующего вызова функции AppendImages, 
    ///что-бы выгрузить из бд объекты изображений и привязать их к объекту
    ///</remarks>
    private Guid ObverseGuid { get; set; }

    ///<summary>
    ///Конструктор для создания нового объекта класса
    ///</summary>
    ///<returns>
    ///Объект класса
    ///</returns>
    ///<param name="name">
    ///Наименование объекта
    ///</param>
    ///<param name="date">
    ///Дата публикации объекта
    ///</param>
    ///<param name="series">
    ///Серия объекта
    ///</param>
    ///<param name="catalogid">
    ///Каталожный номер объекта
    ///</param>
    ///<param name="nominal">
    ///Номинальная стоимость объекта
    ///</param>
    ///<param name="circulation">
    ///Тираж объекта
    ///</param>
    ///<param name="material">
    ///Материал объекта
    ///</param>
    ///<param name="link">
    ///Ссылка на источник данных
    ///</param>
    ///<param name="format">
    ///Формат объекта
    ///</param>
    ///<param name="protection">
    ///Защита объекта
    ///</param>
    ///<param name="perforation">
    ///Перфорация объекта
    ///</param>
    ///<param name="printMetod">
    ///Метод печати объекта
    ///</param>
    ///<param name="design">
    ///Дизайн объекта
    ///</param>
    ///<param name="country">
    ///Страна объекта
    ///</param>
    ///<param name="obverse">
    ///Изображение лицевой стороны объекта
    ///</param>
    public Stamp(string link, string catalogid, string name, string series, string material, decimal nominal, long circulation, DateOnly date, string format, string protection, string perforation, string printMetod, string design, string country, string obverse) : base(link, catalogid, name, series, material, nominal, circulation, date)
    {
        Format = format;
        Protection = protection;
        Perforation = perforation;
        PrintMetod = printMetod;
        Design = design;
        Country = country;
        Obverse = new Image(obverse);
    }

    ///<summary>
    ///Конструктор для создания нового объекта класса из данных базы данных
    ///</summary>
    ///<returns>
    ///Объект класса
    ///</returns>
    ///<param name="id">
    ///GUID объекта
    ///</param>
    ///<param name="name">
    ///Наименование объекта
    ///</param>
    ///<param name="date">
    ///Дата публикации объекта
    ///</param>
    ///<param name="series">
    ///Серия объекта
    ///</param>
    ///<param name="catalogid">
    ///Каталожный номер объекта
    ///</param>
    ///<param name="nominal">
    ///Номинальная стоимость объекта
    ///</param>
    ///<param name="circulation">
    ///Тираж объекта
    ///</param>
    ///<param name="material">
    ///Материал объекта
    ///</param>
    ///<param name="link">
    ///Ссылка на источник данных
    ///</param>
    ///<param name="format">
    ///Формат объекта
    ///</param>
    ///<param name="protection">
    ///Защита объекта
    ///</param>
    ///<param name="perforation">
    ///Перфорация объекта
    ///</param>
    ///<param name="printMetod">
    ///Метод печати объекта
    ///</param>
    ///<param name="design">
    ///Дизайн объекта
    ///</param>
    ///<param name="country">
    ///Страна объекта
    ///</param>
    ///<param name="obverse">
    ///Изображение лицевой стороны объекта
    ///</param>
    public Stamp(Guid id, string link, string catalogid, string name, string series, string material, decimal nominal, long circulation, DateOnly date, string format, string protection, string perforation, string printMetod, string design, string country, Guid obverse) : base(link, catalogid, name, series, material, nominal, circulation, date)
    {
        Format = format;
        Protection = protection;
        Perforation = perforation;
        PrintMetod = printMetod;
        Design = design;
        Country = country;
        ObverseGuid = obverse;
    }


    public async Task AppendImages()
    {
        Obverse = await LoadImage(ObverseGuid);
    }

    public async Task<Image> LoadImage(Guid id)
    {
        return await ImageRequest.Get(id);
    }


    public override async Task Post(TelegramChannel ch)
    {
        var text = $"<a href=\"{this.Link}\">{this.Name}</a>\n\n" +
                   $"<b>Каталожный номер : </b> {this.CatalogId}\n" +
                   $"<b>Дата выпуска : </b> {this.Date}\n\n" +
                   $"<b>Номинал : </b> {this.Nominal} руб.\n" +
                   $"<b>Тираж : </b> {this.Circulation}\n";
        await Telegram.Sendmessage(ch.Channel, text, new Image[] { this.Obverse });
    }

    public override async Task<Collectable> Load()
    {
        await Obverse.Load();
        return (Collectable)await StampRequest.Create(this);
    }

    public override string ToString()
    {
        return "Coin: " + this.Id +
            "\nName:" + this.Name +
            "\nDate:" + this.Date +
            "\nSeries:" + this.Series +
            "\nCatalogId:" + this.CatalogId +
            "\nNominal:" + this.Nominal +
            "\nCirculation:" + this.Circulation +
            "\nLink:" + this.Link +
            "\n";
    }
}
