using MediatR;

namespace PGK.Application.App.Image.Query
{
    public class GetImageQuery : IRequest<byte[]>
    {
        public string ImageName = string.Empty;
        public string Extension = string.Empty;
    }
}
