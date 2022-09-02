using System;

///<summary>
///Класс для хранения объектов монет
///</summary>
public class Coin : Collectable, IImageLoader
{
    ///<summary>
    ///Поле для хранения первого измерения
    ///</summary>
    ///<value>
    ///Первое измерение объекта
    ///</value>
    public double FirstDimension { get; set; }

    ///<summary>
    ///Поле для хранения второго измерения
    ///</summary>
    ///<value>
    ///Второе измерение объекта
    ///</value>
    public double SecondDimension { get; set; }

    ///<summary>
    ///Поле для хранения изображения лицевой стороны объекта
    ///</summary>
    ///<value>
    ///Изображение лицевой стороны объекта
    ///</value>
    public Image Obverse { get; set; }

    ///<summary>
    ///Поле для хранения изображения оборотной стороны объекта
    ///</summary>
    ///<value>
    ///Изображение оборотной стороны объекта
    ///</value>
    public Image Reverse { get; set; }

    ///<summary>
    ///Поле для хранения GUID изображения лицевой стороны объекта
    ///</summary>
    ///<value>
    ///GUID изображения лицевой стороны объекта
    ///</value>
    ///<remarks>
    ///Нужен исключительно для последующего вызова функции AppendImages, 
    ///что-бы выгрузить из бд объекты изображений и привязать их к объекту
    /// </remarks>
    public Guid ObverseGuid { get; set; }

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
    public Guid ReverseGuid { get; set; }

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
    ///<param name="firstdimention">
    ///Первое измерение объекта
    ///</param>
    ///<param name="seconddimention">
    ///Второе измерение объекта
    ///</param>
    ///<param name="obverseLink">
    ///Ссылка на изображение лицевой стороны объекта
    ///</param>
    ///<param name="reverseLink">
    ///Ссылка на изображение оборотной стороны объекта
    ///</param>
    public Coin(string link, string catalogid, string name, string series, string material, decimal nominal, long circulation, DateOnly date, double firstdimention, double seconddimention, string obverseLink, string reverseLink) : base(link, catalogid, name, series, material, nominal, circulation, date)
    {
        FirstDimension = firstdimention;
        SecondDimension = seconddimention;
        Reverse = new Image(reverseLink);
        Obverse = new Image(obverseLink);
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
    ///<param name="firstdimention">
    ///Первое измерение объекта
    ///</param>
    ///<param name="seconddimention">
    ///Второе измерение объекта
    ///</param>
    ///<param name="obverse">
    ///Изображение лицевой стороны объекта
    ///</param>
    ///<param name="reverse">
    ///Изображение оборотной стороны объекта
    ///</param>
    public Coin(Guid id, string link, string catalogid, string name, string series, string material, decimal nominal, long circulation, DateOnly date, double firstdimention, double seconddimention, Guid obverse, Guid reverse) : base(id, link, catalogid, name, series, material, nominal, circulation, date)
    {
        FirstDimension = firstdimention;
        SecondDimension = seconddimention;
        ReverseGuid = reverse;
        ObverseGuid = obverse;
    }

    public async Task AppendImages()
    {
        Reverse = await LoadImage(ReverseGuid);
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
                   $"<b>Материал : </b> {this.Material}\n" +
                   $"<b>Тираж : </b> {this.Circulation}\n";
        await Telegram.Sendmessage(ch.Channel, text, new Image[] { this.Obverse, this.Reverse });
    }

    public override async Task<Collectable> Load()
    {
        await Reverse.Load();
        await Obverse.Load();
        return (Collectable)await CoinRequest.Create(this);
    }

    public override string ToString()
    {
        return "Coin: " + Id +
            "\nName:" + Name +
            "\nDate:" + Date +
            "\nSeries:" + Series +
            "\nCatalogId:" + CatalogId +
            "\nNominal:" + Nominal +
            "\nDiameter:" + FirstDimension +
            "\nDiameter2:" + SecondDimension +
            "\nMetal:" + Material +
            "\nCirculation:" + Circulation +
            "\nLink:" + Link +
            "\n";
    }
}


