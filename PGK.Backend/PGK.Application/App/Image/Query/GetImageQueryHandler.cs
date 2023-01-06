using MediatR;
using PGK.Application.Common;
using PGK.Application.Repository.ImageRepository;

namespace PGK.Application.App.Image.Query
{
    internal class GetImageQueryHandler : IRequestHandler<GetImageQuery, byte[]>
    {
        private readonly IImageRepository _imageRepository;

        public GetImageQueryHandler(IImageRepository imageRepository) =>
            _imageRepository = imageRepository;

        public async Task<byte[]> Handle(GetImageQuery request,
            CancellationToken cancellationToken)
        {
            var imagePath = Constants.IMAGES;

            var image = _imageRepository.Get($"{imagePath}{request.ImageName}.{request.Extension}");

            if (image == null)
            {
                throw new Exception($"Not found image {request.ImageName}");
            }

            return image;
        }
    }
}
