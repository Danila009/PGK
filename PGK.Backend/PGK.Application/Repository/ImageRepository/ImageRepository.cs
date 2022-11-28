using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;

namespace PGK.Application.Repository.ImageRepository
{
    internal class ImageRepository : IImageRepository
    {
        public void Delete(string path)
        {
            if (File.Exists(path))
                File.Delete(path);
        }

        public byte[]? Get(string path)
        {
            if (File.Exists(path))
                return File.ReadAllBytes(path);
            else
                return null;
        }

        public string Save(byte[] imgBytes, string path, string? imageId)
        {
            var image = Image.Load(imgBytes);
            var id = imageId ?? Guid.NewGuid().ToString();

            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            image.Mutate(m =>
                m.Resize(
                    new ResizeOptions
                    {
                        Mode = ResizeMode.Max,
                        Size = new Size(512)
                    }
                 )
            );

            image.Save($"{path}{id}.jpg");

            return $"{path}{id}.jpg";
        }
    }
}
