using Microsoft.AspNetCore.Http;

namespace PGK.Application.Repository.FileRepository
{
    internal class FileRepository : IFileRepository
    {
        private string startupPath = Directory.GetCurrentDirectory();

        public byte[]? GetFile(string path)
        {
            if (File.Exists($"{startupPath}/{path}"))
                return File.ReadAllBytes($"{startupPath}/{path}");
            else
                return null;

        }

        public async Task<string> UploadFile(IFormFile file, string path, string? fileId = null)
        {
            if (!Directory.Exists($"{startupPath}/{path}"))
                Directory.CreateDirectory($"{startupPath}/{path}");

            var id = fileId ?? Guid.NewGuid().ToString();

            var extension = Path.GetExtension(file.FileName);

            using var fileStream = new FileStream($"{startupPath}/{path}{id}{extension}", FileMode.Create);

            await file.CopyToAsync(fileStream);

            return id + extension;
        }
    }
}
