using System;

public struct channelDTO
{
    public string name;
    public string description;
    public string link;
    public string image;

    public channelDTO(string nm, string dsc, string lnk, string img) : this()
    {
        name = nm;
        description = dsc;
        link = lnk;
        image = img;
    }
}