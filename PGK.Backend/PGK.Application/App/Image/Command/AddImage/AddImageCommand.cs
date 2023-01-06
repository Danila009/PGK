using MediatR;
using Microsoft.AspNetCore.Http;

namespace PGK.Application.App.Image.Command.AddImage
{
    public class AddImageCommand : IRequest
    {
        public string ImageName = string.Empty;
        public string Extension = string.Empty;
        public IFormFile Image { get; set; }
    }
}
