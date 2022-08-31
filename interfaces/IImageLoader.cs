using System;
using Npgsql;

public interface IImageLoader
{
	public  Task appendImages();
	public Task<Image> loadImages(Guid id);
}


