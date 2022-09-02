///<summary>
///Базовый абстрактный класс предоставляющий базовый функционал для сущностей базы данных
///</summary>
public abstract class Base
{
    ///<summary>
    ///Поле для хранения GUID
    ///</summary>
    ///<value>
    ///GUID объекта
    ///</value>
    public Guid Id { get; set; }

    ///<summary>
    ///Поле для хранения ссылки
    ///</summary>
    ///<value>
    ///Ссылка на объект в сети
    ///</value>
    public string Link { get; set; }

    ///<summary>
    ///Конструктор для создания нового объекта класса
    ///</summary>
    ///<returns>
    ///Объект класса
    ///</returns>
    ///<param name="link">
    ///Ссылка на источник данных
    ///</param>
    public Base(string link)
    {
        Id = Guid.NewGuid();
        Link = link;
    }

    ///<summary>
    ///Конструктор для создания нового объекта класса из данных базы данных
    ///</summary>
    ///<returns>
    ///Объект класса
    ///</returns>
    ///<param name="link">
    ///Ссылка на источник данных
    ///</param>
    ///<param name="id">
    ///GUID объекта
    ///</param>
    public Base(Guid id, string link)
    {
        Id = id;
        Link = link;
    }

    ///<summary>
    ///Асинхронная функция выгрузки объекта в базу данных 
    ///</summary>
    ///<returns>
    ///Асинхронная задача
    ///</returns>
    public abstract Task Load();

    ///<summary>
    ///Преобразование объекта в строковое представление
    ///</summary>
    ///<returns>
    ///Строковое представление объекта
    ///</returns>
    public abstract string ToString();
}

