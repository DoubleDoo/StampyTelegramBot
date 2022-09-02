using System;
using Npgsql;

public interface IImageLoader
{

    ///<summary>
    ///Асинхронная функция получения изображений объекта из базы данных
    ///</summary>
    ///<returns>
    ///Асинхронная задача
    ///</returns>
    public Task AppendImages();

    ///<summary>
    ///Асинхронная функция получения изображения из базы данных по <paramref name="id"/>
    ///</summary>
    ///<returns>
    ///Асинхронный объект изображения
    ///</returns>
    ///<param name="id">
    ///GUID изображения хранящегося в базе данных
    ///</param>
    public Task<Image> LoadImage(Guid id);
}


