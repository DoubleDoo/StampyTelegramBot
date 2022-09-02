using System.Net;

///<summary>
///Класс для хранения объектов изображений
///</summary>
public class Image : Base
{
    ///<summary>
    ///Конструктор для создания нового объекта класса
    ///</summary>
    ///<returns>
    ///Объект класса
    ///</returns>
    ///<param name="link">
    ///Ссылка на источник данных
    ///</param>
    public Image(string link) : base(link)
    {
        DownloadImage(this.Id);
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
    public Image(Guid id, string link) : base(id, link) { }

    public override async Task Load()
    {
        await ImageRequest.Create(this);
    }

    public override string ToString()
    {
        return "Image " + this.Id.ToString() + ":\n" + Link + "\n";
    }

    ///<summary>
    ///Функция загузки изображения по <paramref name="id"/>
    ///</summary>
    ///<remarks>
    ///Функция загружает изображение по ссылке объекта класса, 
    ///и сохраняет его в формате .jpg с названием равным <paramref name="id"/>
    ///</remarks>
    ///<param name="id">
    ///GUID объекта
    ///</param>
    private void DownloadImage(Guid id)
    {
        using (WebClient client = new WebClient())
        {
            client.DownloadFile(this.Link, Environment.CurrentDirectory + "/data/img/" + id + ".jpg");
        }
    }
}
