using System;

public abstract  class Collectable
{
	public  Guid Id { get; }
	public  string Name { get; }
	public  string Link { get; }

	public Collectable(string nm, string lnk)
	{
		Id = Guid.NewGuid();
		Name = nm;
		Link = lnk;
	}

	public Collectable(Guid i,string nm, string lnk)
	{
		Id = i;
		Name = nm;
		Link = lnk;
	}

	public abstract Task post(channelDTO ch);

	public abstract Task<Collectable> load();

	public abstract string ToString();
}


