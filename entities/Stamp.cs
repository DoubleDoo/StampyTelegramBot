using System;

public class Stamp : Collectable
{

	/*
id: string,
name:string,
date: string,
link: string
circulation:string,
nominal: string,




     set:string,
     format:string,
    protection: string,
    perforation: string,
     paper:string,
    printMetod: string,
     design:string,
    country: string,
     obverse:string,
*/

	public Stamp(string name, string link) : base(name, link)
	{

	}

	public Stamp(Guid id, string name, string link) : base(id, name, link)
	{

	}


	public override async Task post(channelDTO ch)
	{
		var text = $"<a href=\"{this.Link}\">{this.Name}</a>\n\n";
		await Telegram.sendmessage(ch, text);
	}

	public override async Task<Collectable> load()
	{
		Console.WriteLine("Loaded stamp"+this.Id);
		return new Stamp("","");
	}

	public override string ToString()
	{
		return "Coin: " + this.Id +
			"\nName:" + this.Name +
			"\nLink:" + this.Link +
			"\n";
	}
}
