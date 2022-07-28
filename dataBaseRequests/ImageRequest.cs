using System;
using Npgsql;


public static class ImageRequest
{

	private static string tableName = "Image";
	static ImageRequest() {}

	public static Image createImage(string link)
	{
		Image res= new Image(link);
		NpgsqlDataReader rd = PostgreSQLSingle.sendSQL("INSERT INTO public.\"" + tableName + "\"(id, link)" +
		"VALUES(" +
			$"'{ res.Id.ToString() }'," +
			$"'{ res.Link }'" +
			");");
		rd.Dispose();
		return res;
	}

	public static Image getImage(Guid id)
	{
		NpgsqlDataReader rd = PostgreSQLSingle.sendSQL("SELECT * FROM public.\"" + tableName + "\" where id='"+id+"'");
		Image res=null;
		while (rd.Read())
		{
			res=new Image(rd.GetGuid(rd.GetOrdinal("id")), rd.GetString(rd.GetOrdinal("link")));
		}
		rd.Dispose();
		return res;
	}

	public static List<Image> getImages()
	{
		NpgsqlDataReader rd = PostgreSQLSingle.sendSQL("SELECT * FROM public.\"" + tableName + "\"");
		List<Image> list = new List<Image>();
		while (rd.Read())
		{
			list.Add(new Image(rd.GetGuid(rd.GetOrdinal("id")), rd.GetString(rd.GetOrdinal("link"))));
		}
		rd.Dispose();
		return list;
	}
}
