using System;

public abstract  class Collectable : Base
{
	public  string Name { get; }
	public DateOnly Date { get; }
	public string Series { get; }
	public string CatalogId { get; }
	public decimal Nominal { get; }
	public long Circulation { get; }
	public string Material { get; }



	public Collectable(string nm, DateOnly date, string series, string catalogid, decimal nominal, long circulation, string material,string lnk):base(lnk)
	{
		Name = nm;
		Date = date;
		Series = series;
		CatalogId = catalogid;
		Nominal = nominal;
		Circulation = circulation;
		Material = material;
	}

	public Collectable(Guid id,string nm, DateOnly date, string series, string catalogid, decimal nominal, long circulation, string material, string lnk) : base(id,lnk)
	{
		Name = nm;
		Date = date;
		Series = series;
		CatalogId = catalogid;
		Nominal = nominal;
		Circulation = circulation;
		Material = material;
	}

	public abstract Task post(channelDTO ch);

	public override abstract Task<Collectable> load();
}


