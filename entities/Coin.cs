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
		this.reverse = createImage(reverseLink);
		this.obverse = createImage(obverseLink);
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

	public void connectImages()
	{
		this.reverse = loadImages(this.reverseGuid);
		this.obverse = loadImages(this.obverseGuid);
	}

	private Image createImage(string imagelink)
	{
		return ImageRequest.createImage(imagelink);
	}

	private Image loadImages(Guid id)
	{
		return ImageRequest.getImage(id);
	}

	public override string ToString()
	{
		return  "Coin: "+ this.id +
			"\nName:" + this.name+
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
