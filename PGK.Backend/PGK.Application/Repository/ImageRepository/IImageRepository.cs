namespace PGK.Application.Repository.ImageRepository
{
    public interface IImageRepository
    {
        string Save(byte[] imgBytes, string path, string? imageId = null, string extension = "jpg");

        byte[]? Get(string path);

        void Delete(string path);
    }
}
