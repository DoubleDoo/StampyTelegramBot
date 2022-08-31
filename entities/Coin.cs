using System;

public class Coin: Collectable , IImageLoader
{
	private double firstDimension;
	public double FirstDimension
	{
		get { return firstDimension; }
	}

	private double secondDimension;
	public double SecondDimension
	{
		get { return secondDimension; }
	}

	private Image obverse;

	public Image Obverse
	{
		get { return obverse; }
	}
	private Image reverse;

	public Image Reverse
	{
		get { return reverse; }
	}


	private Guid obverseGuid;
	private Guid reverseGuid;


	public Coin(string name, DateOnly date, string series, string catalogid, decimal nominal, double firstdimention, double seconddimention, string material, long circulation, string obverseLink, string reverseLink, string link) : base(name,date,series,catalogid,nominal, circulation, material, link)
	{
		this.firstDimension = firstdimention;
		this.secondDimension = seconddimention;
		this.reverse = new Image(reverseLink);
		this.obverse = new Image(obverseLink);
	}

	public Coin(Guid id, string name, DateOnly date, string series, string catalogid, decimal nominal, double firstdimention, double seconddimention, string material, long circulation, Guid obverse, Guid reverse, string link): base(id,name, date, series, catalogid,nominal,circulation, material, link)
	{
		this.firstDimension = firstdimention;
		this.secondDimension = seconddimention;
		this.reverseGuid = reverse;
		this.obverseGuid = obverse;
	}
	public  async Task appendImages()
	{
		this.reverse = await loadImages(this.reverseGuid);
		this.obverse = await loadImages(this.obverseGuid);
	}

	public  async Task<Image> loadImages(Guid id)
	{
		return await ImageRequest.get(id);
	}

	public override async Task post(channelDTO ch)
	{
		var text = $"<a href=\"{this.Link}\">{this.Name}</a>\n\n" +
				   $"<b>Каталожный номер : </b> {this.CatalogId}\n" +
				   $"<b>Дата выпуска : </b> {this.Date}\n\n" +
				   $"<b>Номинал : </b> {this.Nominal} руб.\n" +
				   $"<b>Материал : </b> {this.Material}\n" +
				   $"<b>Тираж : </b> {this.Circulation}\n";
		await Telegram.sendmessage(ch, text, new Image[] { this.Obverse, this.Reverse });
	}

	public override async Task<Collectable> load()
	{
		await this.reverse.load();
		await this.obverse.load();
		return (Collectable) await CoinRequest.create(this);
	}

	public override string ToString()
	{
		return "Coin: " + this.Id +
			"\nName:" + this.Name +
			"\nDate:" + this.Date +
			"\nSeries:" + this.Series +
			"\nCatalogId:" + this.CatalogId +
			"\nNominal:" + this.Nominal +
			"\nDiameter:" + this.FirstDimension +
			"\nDiameter2:" + this.SecondDimension +
			"\nMetal:" + this.Material +
			"\nCirculation:" + this.Circulation +
			"\nLink:" + this.Link +
			"\n";
	}
}


