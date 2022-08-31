using System;
using System.Net;

public class Image:Base
{
	public Image(string link):base(link)
	{
		downloadImage(this.Id);
	}

	public Image(Guid id,string link) : base(id,link)
	{

	}
	public override async Task load()
	{
		await ImageRequest.create(this);
	}


	public override string ToString()
    {
        return "Image "+Id.ToString()+":\n"+Link+"\n";
    }

	private void downloadImage(Guid id)
	{
		using (WebClient client = new WebClient())
		{
			client.DownloadFile(this.Link, Environment.CurrentDirectory + "/data/img/" + id + ".jpg");
		}
	}
}
