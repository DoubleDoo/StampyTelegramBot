using System;

public class Stamp : Collectable, IImageLoader
{
	
	private string format;
	public string Format
	{
		get { return format; }
	}

	private string protection;
	public string Protection
	{
		get { return protection; }
	}

	private string perforation;
	public string Perforation
	{
		get { return perforation; }
	}

	private string printMetod;
	public string PrintMetod
	{
		get { return printMetod; }
	}

	private string design;
	public string Design
	{
		get { return design; }
	}

	private string country;
	public string Country
	{
		get { return country; }
	}

	private Image obverse;
	public Image Obverse
	{
		get { return obverse; }
	}

	private Guid obverseGuid;


	public Stamp(string name, DateOnly date, string catalogId, string series,decimal nominal,string format,string protection,long circulation,string perforation,string material, string printMetod,string design,string country, string obverse, string link) : base(name, date, series, catalogId, nominal, circulation, material, link)
	{
		this.format=format;
		this.protection=protection;
		this.perforation = perforation;
		this.printMetod=printMetod;
		this.design=design;
		this.country=country;
		this.obverse = new Image(obverse);
	}

	public Stamp(Guid id, string name, DateOnly date, string catalogId, string series, decimal nominal, string format, string protection, long circulation, string perforation, string material, string printMetod, string design, string country, Guid obverse, string link) : base(id, name, date, series, catalogId, nominal, circulation, material, link)
	{
		this.format = format;
		this.protection = protection;
		this.perforation = perforation;
		this.printMetod = printMetod;
		this.design = design;
		this.country = country;
		this.obverseGuid = obverse;
	}


	public async Task appendImages()
	{
		this.obverse = await loadImages(this.obverseGuid);
	}

	public async Task<Image> loadImages(Guid id)
	{
		return await ImageRequest.get(id);
	}


	public override async Task post(channelDTO ch)
	{
		var text = $"<a href=\"{this.Link}\">{this.Name}</a>\n\n" +
				   $"<b>Каталожный номер : </b> {this.CatalogId}\n" +
				   $"<b>Дата выпуска : </b> {this.Date}\n\n" +
				   $"<b>Номинал : </b> {this.Nominal} руб.\n" +
				   $"<b>Тираж : </b> {this.Circulation}\n";
		await Telegram.sendmessage(ch, text, new Image[] { this.Obverse });
	}

	public override async Task<Collectable> load()
	{
		await this.obverse.load();
		return (Collectable)await StampRequest.create(this);
	}

	public override string ToString()
	{
		return "Coin: " + this.Id +
			"\nName:" + this.Name +
			"\nDate:" + this.Date +
			"\nSeries:" + this.Series +
			"\nCatalogId:" + this.CatalogId +
			"\nNominal:" + this.Nominal +
			"\nCirculation:" + this.Circulation +
			"\nLink:" + this.Link +
			"\n";
	}
}
