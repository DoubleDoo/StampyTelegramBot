using System;
using Npgsql;


public static class ImageRequest
{

	private static string tableName = "Image";
	static ImageRequest() {}

	public static async Task<Image> createImage(Image obj)
	{
		NpgsqlDataReader rd = await PostgreSQLSingle.sendSQL("INSERT INTO public.\"" + tableName + "\"(id, link)" +
		"VALUES(" +
			$"'{ obj.Id.ToString() }'," +
			$"'{ obj.Link }'" +
			");");
		await rd.DisposeAsync();
		return obj;
	}

	public static async Task<Image> getImage(Guid id)
	{
		NpgsqlDataReader rd = await PostgreSQLSingle.sendSQL("SELECT * FROM public.\"" + tableName + "\" where id='"+id+"'");
		Image res=null;
		while (rd.Read())
		{
			res=new Image(rd.GetGuid(rd.GetOrdinal("id")), rd.GetString(rd.GetOrdinal("link")));
		}
		await rd.DisposeAsync();
		return res;
	}

	public static async Task<List<Image>> getImages()
	{
		NpgsqlDataReader rd = await PostgreSQLSingle.sendSQL("SELECT * FROM public.\"" + tableName + "\"");
		List<Image> list = new List<Image>();
		while (rd.Read())
		{
			list.Add(new Image(rd.GetGuid(rd.GetOrdinal("id")), rd.GetString(rd.GetOrdinal("link"))));
		}
		await rd.DisposeAsync();
		return list;
	}
}
