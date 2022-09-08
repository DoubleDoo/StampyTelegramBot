using Npgsql;

///<summary>
///Статичный клас для взаимодействия базой данных
///</summary>
public static class PostgreSQLSingle
{
    ///<summary>
    ///Поле для хранения соединения с базой данных
    ///</summary>
    ///<value>
    ///Соединение с базой данных
    ///</value>
    private static NpgsqlConnection dbConnection;

    ///<summary>
    ///Поле для хранения наименования базы данных
    ///</summary>
    ///<value>
    ///Наименование базы данных
    ///</value>
    private static string dataBase = "Collections";

    ///<summary>
    ///Поле для хранения хоста
    ///</summary>
    ///<value>
    ///Хост
    ///</value>
    private static string host = "postgres";

    ///<summary>
    ///Поле для хранения порта
    ///</summary>
    ///<value>
    ///Порт
    ///</value>
    private static string port = "5432";

    ///<summary>
    ///Поле для хранения логина
    ///</summary>
    ///<value>
    ///Логин
    ///</value>
    private static string login = "bot";

    ///<summary>
    ///Поле для хранения пароля
    ///</summary>
    ///<value>
    ///Пароль
    ///</value>
    private static string password = "7140043";

    ///<summary>
    ///Статичнский конструктор для создания нового объекта класса
    ///</summary>
    ///<returns>
    ///Объект класса
    ///</returns>
    static PostgreSQLSingle() { }

    ///<summary>
    ///Асинхронная функция подключения к базе данных
    ///</summary>
    ///<returns>
    ///Асинхронная задача
    ///</returns>
    ///


    public static async Task ConnectToDb()
    {
        string ConnectionString = $"Host={host};Port={port};Username={login};Password={password};Database={dataBase}";
        dbConnection = new NpgsqlConnection(ConnectionString);
        await dbConnection.OpenAsync();
    }



    ///<summary>
    ///Асинхронная функция запроса к базе данных
    ///</summary>
    ///<returns>
    ///Асинхронная задача с результатом NpgsqlDataReader
    ///</returns>
    ///<param name="comand">
    ///SQL комнада
    ///</param>
    public static async Task<NpgsqlDataReader> SendSQL(string comand)
    {
        NpgsqlCommand cmd = new NpgsqlCommand(comand, dbConnection);
        return await cmd.ExecuteReaderAsync();
    }
}
