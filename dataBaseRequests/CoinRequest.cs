using Npgsql;

///<summary>
///Статичный клас для взаимодействия с объектами класса Coin и базой данных
///</summary>
public static class CoinRequest
{
    ///<summary>
    ///Поле для хранения наименования таблицы
    ///</summary>
    ///<value>
    ///Имя таблицы
    ///</value>
    private static string tableName = "Coin";

    ///<summary>
    ///Статичнский конструктор для создания нового объекта класса
    ///</summary>
    ///<returns>
    ///Объект класса
    ///</returns>
    static CoinRequest() { }

    ///<summary>
    ///Асинхронная функция создания объекта в базе данных 
    ///</summary>
    ///<returns>
    ///Асинхронная задача с результатом Coin
    ///</returns>
    ///<param name="obj">
    ///Обьект Coin для загрузки в базу данных
    ///</param>
    public static async Task<Coin> Create(Coin obj)
    {
        NpgsqlDataReader rd = await PostgreSQLSingle.SendSQL($"INSERT INTO public.\"" + tableName + "\"(id, name, date, series, catalogid, nominal, firstDimention, secondDimention, material, circulation, obverse, reverse, link)" +
        "VALUES(" +
            $"'{ obj.Id.ToString() }'," +
            $"'{ obj.Name }'," +
            $"'{ obj.Date.ToString() }'," +
            $"'{ obj.Series }'," +
            $"'{ obj.CatalogId }'," +
            $"'{ obj.Nominal.ToString() }'," +
            $"'{ obj.FirstDimension.ToString() }'," +
            $"'{ obj.SecondDimension.ToString() }'," +
            $"'{ obj.Material }'," +
            $"'{ obj.Circulation.ToString() }'," +
            $"'{ obj.Obverse.Id.ToString() }'," +
            $"'{ obj.Reverse.Id.ToString() }'," +
            $"'{ obj.Link }'" +
            ");");
        await rd.DisposeAsync();
        return obj;
    }

    ///<summary>
    ///Асинхронная функция получения объекта из базы данных
    ///</summary>
    ///<returns>
    ///Асинхронная задача с результатом Coin
    ///</returns>
    ///<param name="id">
    ///GUID Coin для поиска в базе данных
    ///</param>
    public static async Task<Coin> Get(Guid id)
    {
        NpgsqlDataReader rd = await PostgreSQLSingle.SendSQL("SELECT * FROM public.\"" + tableName + "\" where id='" + id + "'");
        Coin res = null;
        while (rd.Read())
        {
            res = new Coin(rd.GetGuid(rd.GetOrdinal("id")),
                           rd.GetString(rd.GetOrdinal("link")),
                           rd.GetString(rd.GetOrdinal("catalogid")),
                           rd.GetString(rd.GetOrdinal("name")),
                           rd.GetString(rd.GetOrdinal("series")),
                           rd.GetString(rd.GetOrdinal("material")),
                           rd.GetDecimal(rd.GetOrdinal("nominal")),
                           rd.GetInt64(rd.GetOrdinal("circulation")),
                           (DateOnly)rd.GetDate(rd.GetOrdinal("date")),
                           rd.GetDouble(rd.GetOrdinal("firstDimention")),
                           rd.GetDouble(rd.GetOrdinal("secondDimention")),
                           rd.GetGuid(rd.GetOrdinal("obverse")),
                           rd.GetGuid(rd.GetOrdinal("reverse"))
                           );
        }
        await rd.DisposeAsync();
        await res.AppendImages();
        return res;
    }

    ///<summary>
    ///Асинхронная функция получения всех объектов из базы данных
    ///</summary>
    ///<returns>
    ///Асинхронная задача с результатом  список Coin
    ///</returns>
    public static async Task<List<Coin>> Get()
    {
        NpgsqlDataReader rd = await PostgreSQLSingle.SendSQL("SELECT * FROM public.\"" + tableName + "\"");
        List<Coin> list = new List<Coin>();
        while (rd.Read())
        {
            list.Add(new Coin(rd.GetGuid(rd.GetOrdinal("id")),
                           rd.GetString(rd.GetOrdinal("link")),
                           rd.GetString(rd.GetOrdinal("catalogid")),
                           rd.GetString(rd.GetOrdinal("name")),
                           rd.GetString(rd.GetOrdinal("series")),
                           rd.GetString(rd.GetOrdinal("material")),
                           rd.GetDecimal(rd.GetOrdinal("nominal")),
                           rd.GetInt64(rd.GetOrdinal("circulation")),
                           (DateOnly)rd.GetDate(rd.GetOrdinal("date")),
                           rd.GetDouble(rd.GetOrdinal("firstDimention")),
                           rd.GetDouble(rd.GetOrdinal("secondDimention")),
                           rd.GetGuid(rd.GetOrdinal("obverse")),
                           rd.GetGuid(rd.GetOrdinal("reverse"))
                           ));
        }
        await rd.DisposeAsync();
        list.ForEach(x => x.AppendImages());
        return list;
    }
}