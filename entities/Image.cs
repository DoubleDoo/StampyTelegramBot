using System;
using System.Net;

public class Image
{
	private Guid id;
	public Guid Id   
	{
		get { return id; }
	}

	private string link;

	public string Link  
	{
		get { return link; }
	}
	public Image(string link)
	{
		this.id = Guid.NewGuid();
		this.link = link;
	}

	public Image(Guid id,string link)
	{
		this.id = id;
		this.link = link;
	}

	private void downloadImage(Guid id)
    {
		using (WebClient client = new WebClient())
		{
			client.DownloadFile(this.link, Environment.CurrentDirectory+"/data/img/"  + id+".jpg");
		}
	}

	public async Task post(channelDTO ch)
	{
		var text = $"<a href=\"{this.id}\">{this.Link}</a>\n";
		await Telegram.sendmessage(ch, text, new Image[] { this });
	}

	public async Task<Image> load()
	{
		downloadImage(this.id);
		return await ImageRequest.createImage(this);
	}

	public override string ToString()
    {
        return base.ToString()+"Image "+id.ToString()+":\n"+link+"\n";
    }
}
