using Npgsql;

///<summary>
///Статичный клас для взаимодействия с объектами класса Image и базой данных
///</summary>
public static class ImageRequest
{
    ///<summary>
    ///Поле для хранения наименования таблицы
    ///</summary>
    ///<value>
    ///Имя таблицы
    ///</value>
    private static string tableName = "Image";

    ///<summary>
    ///Статичнский конструктор для создания нового объекта класса
    ///</summary>
    ///<returns>
    ///Объект класса
    ///</returns>
    static ImageRequest() { }

    ///<summary>
    ///Асинхронная функция создания объекта в базе данных 
    ///</summary>
    ///<returns>
    ///Асинхронная задача с результатом Image
    ///</returns>
    ///<param name="obj">
    ///Обьект Coin для загрузки в базу данных
    ///</param>
    public static async Task<Image> Create(Image obj)
    {
        NpgsqlDataReader rd = await PostgreSQLSingle.SendSQL("INSERT INTO public.\"" + tableName + "\"(id, link)" +
        "VALUES(" +
            $"'{ obj.Id.ToString() }'," +
            $"'{ obj.Link }'" +
            ");");
        await rd.DisposeAsync();
        return obj;
    }

    ///<summary>
    ///Асинхронная функция получения объекта из базы данных
    ///</summary>
    ///<returns>
    ///Асинхронная задача с результатом Image
    ///</returns>
    ///<param name="id">
    ///GUID Image для поиска в базе данных
    ///</param>
    public static async Task<Image> Get(Guid id)
    {
        Console.WriteLine("Begin image db req");
        NpgsqlDataReader rd = await PostgreSQLSingle.SendSQL("SELECT * FROM public.\"" + tableName + "\" where id='" + id + "'");
        Image res = null;
        while (rd.Read())
        {
            res = new Image(rd.GetGuid(rd.GetOrdinal("id")), rd.GetString(rd.GetOrdinal("link")));
        }
        await rd.DisposeAsync();
        Console.WriteLine("End image db req");
        return res;
    }

    ///<summary>
    ///Асинхронная функция получения всех объектов из базы данных
    ///</summary>
    ///<returns>
    ///Асинхронная задача с результатом  список Image
    ///</returns>
    public static async Task<List<Image>> Get()
    {
        Console.WriteLine("Begin images db req");
        NpgsqlDataReader rd = await PostgreSQLSingle.SendSQL("SELECT * FROM public.\"" + tableName + "\"");
        List<Image> list = new List<Image>();
        while (rd.Read())
        {
            list.Add(new Image(rd.GetGuid(rd.GetOrdinal("id")), rd.GetString(rd.GetOrdinal("link"))));
        }
        await rd.DisposeAsync();
        Console.WriteLine("End images db req");
        return list;
    }
}
