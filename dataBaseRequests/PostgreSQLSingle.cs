using System;
using Npgsql;



public static class PostgreSQLSingle
{
	private static NpgsqlConnection dbConnection;

	private static string dataBase = "Collections";

	private static string host = "postgres";

	private static string port = "5432";

	private static string login = "bot";

	private static string password = "7140043";
	static PostgreSQLSingle()
	{

	}

	public static async Task connectToDb()
    {
		string ConnectionString = $"Host={host};Port={port};Username={login};Password={password};Database={dataBase}";
		dbConnection = new NpgsqlConnection(ConnectionString);
		await dbConnection.OpenAsync();
	}

	public static async Task<NpgsqlDataReader> sendSQL(string comand)
	{
		NpgsqlCommand cmd = new NpgsqlCommand(comand, dbConnection);
		return await cmd.ExecuteReaderAsync();
	}
}
