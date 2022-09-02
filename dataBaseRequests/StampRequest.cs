using Npgsql;

///<summary>
///Статичный клас для взаимодействия с объектами класса Stamp и базой данных
///</summary>
public static class StampRequest
{
    ///<summary>
    ///Поле для хранения наименования таблицы
    ///</summary>
    ///<value>
    ///Имя таблицы
    ///</value>
    private static string tableName = "Stamp";

    ///<summary>
    ///Статичнский конструктор для создания нового объекта класса
    ///</summary>
    ///<returns>
    ///Объект класса
    ///</returns>
    static StampRequest() { }

    ///<summary>
    ///Асинхронная функция создания объекта в базе данных 
    ///</summary>
    ///<returns>
    ///Асинхронная задача с результатом Stamp
    ///</returns>
    ///<param name="obj">
    ///Обьект Coin для загрузки в базу данных
    ///</param>
    public static async Task<Stamp> Create(Stamp obj)
    {
        NpgsqlDataReader rd = await PostgreSQLSingle.SendSQL($"INSERT INTO public.\"" + tableName + "\"(id, name, date, catalogid, series, nominal, format, protection, circulation, perforation, material, printmetod, design, country, obverse, link)" +
        "VALUES(" +
            $"'{ obj.Id.ToString() }'," +
            $"'{ obj.Name }'," +
            $"'{ obj.Date.ToString() }'," +
            $"'{ obj.CatalogId }'," +
            $"'{ obj.Series }'," +
            $"'{ obj.Nominal.ToString() }'," +
            $"'{ obj.Format }'," +
            $"'{ obj.Protection }'," +
            $"'{ obj.Circulation.ToString() }'," +
            $"'{ obj.Perforation }'," +
            $"'{ obj.Material }'," +
            $"'{ obj.PrintMetod }'," +
            $"'{ obj.Design }'," +
            $"'{ obj.Country }'," +
            $"'{ obj.Obverse.Id.ToString() }'," +
            $"'{ obj.Link }'" +
            ");");
        await rd.DisposeAsync();
        return obj;
    }

    ///<summary>
    ///Асинхронная функция получения объекта из базы данных
    ///</summary>
    ///<returns>
    ///Асинхронная задача с результатом Stamp
    ///</returns>
    ///<param name="id">
    ///GUID Stamp для поиска в базе данных
    ///</param>
    public static async Task<Stamp> Get(Guid id)
    {
        NpgsqlDataReader rd = await PostgreSQLSingle.SendSQL("SELECT * FROM public.\"" + tableName + "\" where id='" + id + "'");
        Stamp res = null;
        while (rd.Read())
        {
            res = new Stamp(rd.GetGuid(rd.GetOrdinal("id")),
                            rd.GetString(rd.GetOrdinal("link")),
                            rd.GetString(rd.GetOrdinal("catalogid")),
                            rd.GetString(rd.GetOrdinal("name")),
                            rd.GetString(rd.GetOrdinal("series")),
                            rd.GetString(rd.GetOrdinal("material")),
                            rd.GetDecimal(rd.GetOrdinal("nominal")),
                            rd.GetInt64(rd.GetOrdinal("circulation")),
                            (DateOnly)rd.GetDate(rd.GetOrdinal("date")),
                            rd.GetString(rd.GetOrdinal("format")),
                            rd.GetString(rd.GetOrdinal("protection")),
                            rd.GetString(rd.GetOrdinal("perforation")),
                            rd.GetString(rd.GetOrdinal("printMetod")),
                            rd.GetString(rd.GetOrdinal("design")),
                            rd.GetString(rd.GetOrdinal("country")),
                            rd.GetGuid(rd.GetOrdinal("obverse"))
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
    ///Асинхронная задача с результатом  список Stamp
    ///</returns>
    public static async Task<List<Stamp>> Get()
    {
        NpgsqlDataReader rd = await PostgreSQLSingle.SendSQL("SELECT * FROM public.\"" + tableName + "\"");
        List<Stamp> list = new List<Stamp>();
        while (rd.Read())
        {
            list.Add(new Stamp(rd.GetGuid(rd.GetOrdinal("id")),
                               rd.GetString(rd.GetOrdinal("link")),
                               rd.GetString(rd.GetOrdinal("catalogid")),
                               rd.GetString(rd.GetOrdinal("name")),
                               rd.GetString(rd.GetOrdinal("series")),
                               rd.GetString(rd.GetOrdinal("material")),
                               rd.GetDecimal(rd.GetOrdinal("nominal")),
                               rd.GetInt64(rd.GetOrdinal("circulation")),
                               (DateOnly)rd.GetDate(rd.GetOrdinal("date")),
                               rd.GetString(rd.GetOrdinal("format")),
                               rd.GetString(rd.GetOrdinal("protection")),
                               rd.GetString(rd.GetOrdinal("perforation")),
                               rd.GetString(rd.GetOrdinal("printMetod")),
                               rd.GetString(rd.GetOrdinal("design")),
                               rd.GetString(rd.GetOrdinal("country")),
                               rd.GetGuid(rd.GetOrdinal("obverse"))
                               ));
        }
        await rd.DisposeAsync();
        list.ForEach(x => x.AppendImages());
        return list;
    }
}