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
		dbConnection = connectToDb();
	}

	public static  NpgsqlConnection connectToDb()
    {
		string ConnectionString = $"Host={host};Port={port};Username={login};Password={password};Database={dataBase}";
		NpgsqlConnection res = new NpgsqlConnection(ConnectionString);
		res.Open();
		return res;
	}

	public static NpgsqlDataReader sendSQL(string comand)
	{
		NpgsqlCommand cmd = new NpgsqlCommand(comand, dbConnection);
		NpgsqlDataReader reader = cmd.ExecuteReader();
		return reader;
	}

	/*
	public static void closeConnection()
	{
		dbConnection.Close();
	}
	*/
}
