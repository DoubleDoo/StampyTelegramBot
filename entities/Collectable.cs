///<summary>
///Абстрактный класс предоставлющий общие поля для коллекционных объектов
///</summary>
public abstract class Collectable : Base
{
    ///<summary>
    ///Поле для хранения наименования объекта
    ///</summary>
    ///<value>
    ///Имя объекта
    ///</value>
    public string Name { get; set; }

    ///<summary>
    ///Поле для хранения даты публикации объекта в его источнике
    ///</summary>
    ///<value>
    ///Дата публикации объекта
    ///</value>
    public DateOnly Date { get; set; }

    ///<summary>
    ///Поле для хранения наименования серии к котрой принадлежит объект
    ///</summary>
    ///<value>
    ///Наименование серии объекта
    ///</value>
    public string Series { get; set; }

    ///<summary>
    ///Поле для хранения каталожного номера объекта
    ///</summary>
    ///<value>
    ///Каталожный номер объекта
    ///</value>
    public string CatalogId { get; set; }

    ///<summary>
    ///Поле для хранения номинальнй стоимости объекта
    ///</summary>
    ///<value>
    ///Номинальная стоимость объекта
    ///</value>
    public decimal Nominal { get; set; }

    ///<summary>
    ///Поле для хранения тиража объекта
    ///</summary>
    ///<value>
    ///Тираж объекта
    ///</value>
    public long Circulation { get; set; }

    ///<summary>
    ///Поле для хранения материала объекта
    ///</summary>
    ///<value>
    ///Матерал объекта
    ///</value>
    public string Material { get; set; }

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
    public Collectable(string link, string catalogid, string name, string series, string material, decimal nominal, long circulation, DateOnly date) : base(link)
    {
        Name = name;
        Date = date;
        Series = series;
        CatalogId = catalogid;
        Nominal = nominal;
        Circulation = circulation;
        Material = material;
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
    public Collectable(Guid id, string link, string catalogid, string name, string series, string material, decimal nominal, long circulation, DateOnly date) : base(id, link)
    {
        Name = name;
        Date = date;
        Series = series;
        CatalogId = catalogid;
        Nominal = nominal;
        Circulation = circulation;
        Material = material;
    }

    ///<summary>
    ///Асинхронная функция публикации объекта в телеграм канале 
    ///</summary>
    ///<returns>
    ///Асинхронная задача
    ///</returns>
    ///<param name="ch">
    ///Канал в котором произведется публикация
    ///</param>
    public abstract Task Post(TelegramChannel ch);

    public override abstract Task<Collectable> Load();
}


