using System;

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
		downloadImage(this.id);
	}

	public Image(Guid id,string link)
	{
		this.id = id;
		this.link = link;
	}

	private void downloadImage(Guid id)
    {

    }

    public override string ToString()
    {
        return base.ToString()+"Image "+id.ToString()+":\n"+link+"\n";
    }
}
