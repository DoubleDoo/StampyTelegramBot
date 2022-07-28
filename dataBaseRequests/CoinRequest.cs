using System;
using Npgsql;


public static class CoinRequest
{

	private static string tableName = "Coin";
	static CoinRequest() { }

	public static Coin createCoin(string name, DateOnly date, string series, string catalogid, decimal nominal, double diameter, string metal, long circulation, string obverseLink, string reverseLink, string link)
	{
		Coin res = new Coin(name, date, series, catalogid, nominal, diameter, metal, circulation, obverseLink, reverseLink, link);
		NpgsqlDataReader rd = PostgreSQLSingle.sendSQL($"INSERT INTO public.\"" + tableName + "\"(id, name, date, series, catalogid, nominal, diameter, metal, circulation, obverse, reverse, link)" +
		"VALUES(" +
			$"'{ res.Id.ToString() }'," +
			$"'{ res.Name }'," +
			$"'{ res.Date.ToString() }'," +
			$"'{ res.Series }'," +
			$"'{ res.CatalogId }'," +
			$"'{ res.Nominal.ToString() }'," +
			$"'{ res.Diameter.ToString() }'," +
			$"'{ res.Metal }'," +
			$"'{ res.Circulation.ToString() }'," +
			$"'{ res.Obverse.Id.ToString() }'," +
			$"'{ res.Reverse.Id.ToString() }'," +
			$"'{ res.Link }'" +
			");");
		rd.Dispose();
		return res;

	}

	public static Coin getCoin(Guid id)
	{
		NpgsqlDataReader rd = PostgreSQLSingle.sendSQL("SELECT * FROM public.\"" + tableName + "\" where id='" + id + "'");
		Coin res = null;
		while (rd.Read())
		{
			res = new Coin(rd.GetGuid(rd.GetOrdinal("id")),
						   rd.GetString(rd.GetOrdinal("name")),
						   (DateOnly)rd.GetDate(rd.GetOrdinal("date")),
						   rd.GetString(rd.GetOrdinal("series")),
					 	   rd.GetString(rd.GetOrdinal("catalogid")),
					 	   rd.GetDecimal(rd.GetOrdinal("nominal")),
						   rd.GetDouble(rd.GetOrdinal("diameter")),
						   rd.GetString(rd.GetOrdinal("metal")),
						   rd.GetInt64(rd.GetOrdinal("circulation")),
						   rd.GetGuid(rd.GetOrdinal("obverse")),
						   rd.GetGuid(rd.GetOrdinal("reverse")),
						   rd.GetString(rd.GetOrdinal("link"))
						   );
		}
		rd.Dispose();
		res.connectImages();
		return res;
	}

	public static List<Coin> getCoins()
	{
		NpgsqlDataReader rd = PostgreSQLSingle.sendSQL("SELECT * FROM public.\"" + tableName + "\"");
		List<Coin> list = new List<Coin>();
		while (rd.Read())
		{
			list.Add(new Coin(rd.GetGuid(rd.GetOrdinal("id")),
						   rd.GetString(rd.GetOrdinal("name")),
						   (DateOnly)rd.GetDate(rd.GetOrdinal("date")),
						   rd.GetString(rd.GetOrdinal("series")),
						   rd.GetString(rd.GetOrdinal("catalogid")),
						   rd.GetDecimal(rd.GetOrdinal("nominal")),
						   rd.GetDouble(rd.GetOrdinal("diameter")),
						   rd.GetString(rd.GetOrdinal("metal")),
						   rd.GetInt64(rd.GetOrdinal("circulation")),
						   rd.GetGuid(rd.GetOrdinal("obverse")),
						   rd.GetGuid(rd.GetOrdinal("reverse")),
						   rd.GetString(rd.GetOrdinal("link"))
						   ));
		}
		rd.Dispose();
		list.ForEach(x => x.connectImages());
		return list;
	}
}