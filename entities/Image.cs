using System.Net;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using SixLabors.ImageSharp.Formats.Jpeg;


///<summary>
///Класс для хранения объектов изображений
///</summary>
public class Image : Base
{
    ///<summary>
    ///Конструктор для создания нового объекта класса
    ///</summary>
    ///<returns>
    ///Объект класса
    ///</returns>
    ///<param name="link">
    ///Ссылка на источник данных
    ///</param>
    public Image(string link) : base(link)
    {

    }

    ///<summary>
    ///Конструктор для создания нового объекта класса из данных базы данных
    ///</summary>
    ///<returns>
    ///Объект класса
    ///</returns>
    ///<param name="link">
    ///Ссылка на источник данных
    ///</param>
    ///<param name="id">
    ///GUID объекта
    ///</param>
    public Image(Guid id, string link) : base(id, link) { }

    public override async Task Load()
    {
        await DownloadImage(Id);
        await ImageRequest.Create(this);
    }

    public override string ToString()
    {
        return "Image " + this.Id.ToString() + ":\n" + Link + "\n";
    }

    ///<summary>
    ///Функция загузки изображения по <paramref name="id"/>
    ///</summary>
    ///<remarks>
    ///Функция загружает изображение по ссылке объекта класса, 
    ///и сохраняет его в формате .jpg с названием равным <paramref name="id"/>
    ///</remarks>
    ///<param name="id">
    ///GUID объекта
    ///</param>
    private async Task DownloadImage(Guid id)
    {
        string path = Environment.CurrentDirectory + "/data/img/" + id+ ".jpg";
        string buffer = Environment.CurrentDirectory + "/data/img/" +"buffer" + ".jpg";

        WebClient client = new WebClient();
        await client.DownloadFileTaskAsync(new Uri(Link), path);

        SixLabors.ImageSharp.Image img = SixLabors.ImageSharp.Image.Load(path);

        if(img.Width > 1280 || img.Height > 1280)
        {
            Console.WriteLine(id+" Resizing...");
            double diff = new double[] { img.Width, img.Height }.Max() / (double) 1280;
            img.Mutate(x => x.Resize((int)Math.Round(img.Width / diff), (int)Math.Round(img.Height / diff), KnownResamplers.Lanczos3));
            img.Save(path);
        }

        /*
        int qual = 100;
        int step = 1;
        double length = (double)new System.IO.FileInfo(path).Length / 1024 / 1024;
        Console.WriteLine(qual + ":" + length);
        if (length >= 10 && qual>= step)
        {
            var img = SixLabors.ImageSharp.Image.Load(path);
            while (length >= 3 && qual >= step)
            {
                qual = qual - step;
                FileStream outImage = File.Create(buffer);
                await img.SaveAsJpegAsync(outImage, new JpegEncoder { Quality = qual });
                length = (double)new System.IO.FileInfo(buffer).Length / 1024 / 1024;
                outImage.Close();
                Console.WriteLine(qual + ":" + length);
            }
            img = SixLabors.ImageSharp.Image.Load(buffer);
            FileStream outI = File.Create(path);
            await img.SaveAsJpegAsync(outI);
            outI.Close();
            File.Delete(buffer);
        }*/
    }
}
