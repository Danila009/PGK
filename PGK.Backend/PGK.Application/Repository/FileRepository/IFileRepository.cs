using Microsoft.AspNetCore.Http;

namespace PGK.Application.Repository.FileRepository
{
    internal interface IFileRepository
    {
        Task<string> UploadFile(IFormFile file, string path, string? fileId = null);

        byte[]? GetFile(string path);

    }
}
