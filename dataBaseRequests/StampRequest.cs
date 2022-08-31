using System;
using Npgsql;


public static class StampRequest
{

	private static string tableName = "Stamp";
	static StampRequest() { }

	public static async Task<Stamp> create(Stamp obj)
	{
		NpgsqlDataReader rd = await PostgreSQLSingle.sendSQL($"INSERT INTO public.\"" + tableName + "\"(id, name, date, catalogid, series, nominal, format, protection, circulation, perforation, material, printmetod, design, country, obverse, link)" +
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


	public static async Task<Stamp> get(Guid id)
	{
		NpgsqlDataReader rd = await PostgreSQLSingle.sendSQL("SELECT * FROM public.\"" + tableName + "\" where id='" + id + "'");
		Stamp res = null;
		while (rd.Read())
		{
			res = new Stamp(rd.GetGuid(rd.GetOrdinal("id")),
						   rd.GetString(rd.GetOrdinal("name")),
						   (DateOnly)rd.GetDate(rd.GetOrdinal("date")),
						   rd.GetString(rd.GetOrdinal("catalogid")),
						   rd.GetString(rd.GetOrdinal("series")),
						   rd.GetDecimal(rd.GetOrdinal("nominal")),
						   rd.GetString(rd.GetOrdinal("format")),
						   rd.GetString(rd.GetOrdinal("protection")),
						   rd.GetInt64(rd.GetOrdinal("circulation")),
						   rd.GetString(rd.GetOrdinal("perforation")),
						   rd.GetString(rd.GetOrdinal("material")),
						   rd.GetString(rd.GetOrdinal("printMetod")),
						   rd.GetString(rd.GetOrdinal("design")),
						   rd.GetString(rd.GetOrdinal("country")),
						   rd.GetGuid(rd.GetOrdinal("obverse")),
						   rd.GetString(rd.GetOrdinal("link"))
						   );
		}
		await rd.DisposeAsync();
		await res.appendImages();
		return res;
	}

	public static async Task<List<Stamp>> get()
	{
		NpgsqlDataReader rd = await PostgreSQLSingle.sendSQL("SELECT * FROM public.\"" + tableName + "\"");
		List<Stamp> list = new List<Stamp>();
		while (rd.Read())
		{
			list.Add(new Stamp(rd.GetGuid(rd.GetOrdinal("id")),
						   rd.GetString(rd.GetOrdinal("name")),
						   (DateOnly)rd.GetDate(rd.GetOrdinal("date")),
						   rd.GetString(rd.GetOrdinal("catalogid")),
						   rd.GetString(rd.GetOrdinal("series")),
						   rd.GetDecimal(rd.GetOrdinal("nominal")),
						   rd.GetString(rd.GetOrdinal("format")),
						   rd.GetString(rd.GetOrdinal("protection")),
						   rd.GetInt64(rd.GetOrdinal("circulation")),
						   rd.GetString(rd.GetOrdinal("perforation")),
						   rd.GetString(rd.GetOrdinal("material")),
						   rd.GetString(rd.GetOrdinal("printMetod")),
						   rd.GetString(rd.GetOrdinal("design")),
						   rd.GetString(rd.GetOrdinal("country")),
						   rd.GetGuid(rd.GetOrdinal("obverse")),
						   rd.GetString(rd.GetOrdinal("link"))
						   ));
		}
		await rd.DisposeAsync();
		list.ForEach(x => x.appendImages());
		return list;
	}
}