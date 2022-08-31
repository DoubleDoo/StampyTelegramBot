using System;

public abstract class Base
{
	public Guid Id { get; }
	public string Link { get; }

	public Base(string lnk)
	{
		Id = Guid.NewGuid();
		Link = lnk;
	}

	public Base(Guid i, string lnk)
	{
		Id = i;
		Link = lnk;
	}

	public abstract Task load();
	public abstract string ToString();
}

