using MediatR;
using PGK.Application.Common.Exceptions;
using PGK.Application.Common;
using PGK.Application.Repository.ImageRepository;

namespace PGK.Application.App.User.Queries.GetUserPhoto
{
    public class GetUserPhotoQueryHandler
        : IRequestHandler<GetUserPhotoQuery, byte[]>
    {
        private readonly IImageRepository _imageRepository;
     
        public GetUserPhotoQueryHandler(IImageRepository imageRepository) =>
            _imageRepository = imageRepository;

        public async Task<byte[]> Handle(GetUserPhotoQuery request, CancellationToken cancellationToken)
        {
            var imagePath = Constants.USER_PHOTO_PATH;

            var image = _imageRepository.Get($"{imagePath}{request.UserId}.jpg");

            if(image == null)
            {
                throw new Exception($"Not found image {request.UserId}");
            }

            return image;
        }
    }
}
