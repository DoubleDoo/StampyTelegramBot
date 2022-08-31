using System;
using Npgsql;


public static class CoinRequest
{

	private static string tableName = "Coin";
	static CoinRequest() { }

	public static async Task<Coin> create(Coin obj)
	{
		NpgsqlDataReader rd = await PostgreSQLSingle.sendSQL($"INSERT INTO public.\"" + tableName + "\"(id, name, date, series, catalogid, nominal, firstDimention, secondDimention, metal, circulation, obverse, reverse, link)" +
		"VALUES(" +
			$"'{ obj.Id.ToString() }'," +
			$"'{ obj.Name }'," +
			$"'{ obj.Date.ToString() }'," +
			$"'{ obj.Series }'," +
			$"'{ obj.CatalogId }'," +
			$"'{ obj.Nominal.ToString() }'," +
			$"'{ obj.FirstDimension.ToString() }'," +
			$"'{ obj.SecondDimension.ToString() }'," +
			$"'{ obj.Metal }'," +
			$"'{ obj.Circulation.ToString() }'," +
			$"'{ obj.Obverse.Id.ToString() }'," +
			$"'{ obj.Reverse.Id.ToString() }'," +
			$"'{ obj.Link }'" +
			");");
		await rd.DisposeAsync();
		return obj;
	}


	public static async Task<Coin> get(Guid id)
	{
		NpgsqlDataReader rd = await PostgreSQLSingle.sendSQL("SELECT * FROM public.\"" + tableName + "\" where id='" + id + "'");
		Coin res = null;
		while (rd.Read())
		{
			res = new Coin(rd.GetGuid(rd.GetOrdinal("id")),
						   rd.GetString(rd.GetOrdinal("name")),
						   (DateOnly)rd.GetDate(rd.GetOrdinal("date")),
						   rd.GetString(rd.GetOrdinal("series")),
					 	   rd.GetString(rd.GetOrdinal("catalogid")),
					 	   rd.GetDecimal(rd.GetOrdinal("nominal")),
						   rd.GetDouble(rd.GetOrdinal("firstDimention")),
						   rd.GetDouble(rd.GetOrdinal("secondDimention")),
						   rd.GetString(rd.GetOrdinal("metal")),
						   rd.GetInt64(rd.GetOrdinal("circulation")),
						   rd.GetGuid(rd.GetOrdinal("obverse")),
						   rd.GetGuid(rd.GetOrdinal("reverse")),
						   rd.GetString(rd.GetOrdinal("link"))
						   );
		}
		await rd.DisposeAsync();
		await res.appendImages();
		return res;
	}

	public static async Task<List<Coin>> get()
	{
		NpgsqlDataReader rd = await PostgreSQLSingle.sendSQL("SELECT * FROM public.\"" + tableName + "\"");
		List<Coin> list = new List<Coin>();
		while (rd.Read())
		{
			list.Add(new Coin(rd.GetGuid(rd.GetOrdinal("id")),
						   rd.GetString(rd.GetOrdinal("name")),
						   (DateOnly)rd.GetDate(rd.GetOrdinal("date")),
						   rd.GetString(rd.GetOrdinal("series")),
						   rd.GetString(rd.GetOrdinal("catalogid")),
						   rd.GetDecimal(rd.GetOrdinal("nominal")),
							rd.GetDouble(rd.GetOrdinal("firstDimention")),
						   rd.GetDouble(rd.GetOrdinal("secondDimention")),
						   rd.GetString(rd.GetOrdinal("metal")),
						   rd.GetInt64(rd.GetOrdinal("circulation")),
						   rd.GetGuid(rd.GetOrdinal("obverse")),
						   rd.GetGuid(rd.GetOrdinal("reverse")),
						   rd.GetString(rd.GetOrdinal("link"))
						   ));
		}
		await rd.DisposeAsync();
		list.ForEach(x => x.appendImages());
		return list;
	}
}