using System;

public class Coin
{
	private Guid id;
	public Guid Id
	{
		get { return id; }
	}

	private string name;

	public string Name
	{
		get { return name; }
	}
	
	private DateOnly date;

	public DateOnly Date
	{
		get { return date; }
	}

	private string series;

	public string Series
	{
		get { return series; }
	}
	private string catalogId;

	public string CatalogId
	{
		get { return catalogId; }
	}
	private decimal nominal;

	public decimal Nominal
	{
		get { return nominal; }
	}
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
	private string metal;

	public string Metal
	{
		get { return metal; }
	}
	private long circulation;

	public long Circulation
	{
		get { return circulation; }
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


	private string link;

	public string Link
	{
		get { return link; }
	}

	public Coin(string name, DateOnly date, string series, string catalogid, decimal nominal, double firstdimention, double seconddimention, string metal, long circulation, string obverseLink, string reverseLink, string link)
	{
		this.id = Guid.NewGuid();
		this.name = name;
		this.date = date;
		this.series = series;
		this.catalogId = catalogid;
		this.nominal = nominal;
		this.firstDimension = firstdimention;
		this.secondDimension = seconddimention;
		this.metal = metal;
		this.circulation = circulation;
		this.reverse = new Image(reverseLink);
		this.obverse = new Image(obverseLink);
		this.link = link;
	}

	public Coin(Guid id, string name, DateOnly date, string series, string catalogid, decimal nominal, double firstdimention, double seconddimention, string metal, long circulation, Guid obverse, Guid reverse, string link)
	{
		this.id = id;
		this.name = name;
		this.date = date;
		this.series = series;
		this.catalogId = catalogid;
		this.nominal = nominal;
		this.firstDimension = firstdimention;
		this.secondDimension = seconddimention;
		this.metal = metal;
		this.circulation = circulation;
		this.reverseGuid = reverse;
		this.obverseGuid = obverse;
		this.link = link;
	}


	public async Task appendImages()
	{
		this.reverse = await loadImages(this.reverseGuid);
		this.obverse = await loadImages(this.obverseGuid);
	}

	private async Task<Image> loadImages(Guid id)
	{
		return await ImageRequest.getImage(id);
	}


	public async Task post(channelDTO ch)
	{
		var text = $"<a href=\"{this.Link}\">{this.Name}</a>\n\n" +
				   $"<b>Каталожный номер : </b> {this.CatalogId}\n" +
				   $"<b>Дата выпуска : </b> {this.Date}\n\n" +
				   $"<b>Номинал : </b> {this.Nominal} руб.\n" +
				   $"<b>Материал : </b> {this.Metal}\n" +
				   $"<b>Тираж : </b> {this.Circulation}\n";
		await Telegram.sendmessage(ch, text, new Image[] { this.Obverse, this.Reverse });
	}

	public async Task<Coin> load()
	{
		await this.reverse.load();
		await this.obverse.load();
		return await CoinRequest.createCoin(this);
	}

	public override string ToString()
	{
		return "Coin: " + this.id +
			"\nName:" + this.name +
			"\nDate:" + this.date +
			"\nSeries:" + this.series +
			"\nCatalogId:" + this.catalogId +
			"\nNominal:" + this.nominal +
			"\nDiameter:" + this.firstDimension +
			"\nDiameter2:" + this.secondDimension +
			"\nMetal:" + this.metal +
			"\nCirculation:" + this.circulation +
			"\nLink:" + this.link +
			"\n";
	}
}


