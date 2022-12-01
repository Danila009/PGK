using MediatR;
using PGK.Application.Interfaces;
using PGK.Application.Repository.ImageRepository;
using PGK.Application.Common.Exceptions;
using PGK.Application.Common;

namespace PGK.Application.App.User.Commands.AddPhoto
{
    public class UserAddPhotoCommandHandler
        : IRequestHandler<UserAddPhotoCommand, UserPhotoVm>
    {
        private readonly IPGKDbContext _dbContext;
        private readonly IImageRepository _imageRepository;

        public UserAddPhotoCommandHandler(IPGKDbContext dbContext,
            IImageRepository imageRepository) =>
            (_dbContext, _imageRepository) = (dbContext, imageRepository);

        public async Task<UserPhotoVm> Handle(UserAddPhotoCommand request,
            CancellationToken cancellationToken)
        {
            var user = await _dbContext.Users.FindAsync(request.UserId);

            if(user == null)
            {
                throw new NotFoundException(nameof(Domain.User.User), request.UserId);
            }

            MemoryStream memoryStream = new MemoryStream();
            await request.Photo.CopyToAsync(memoryStream);

            var imagePath = Constants.USER_PHOTO_PATH;

            _imageRepository.Save(memoryStream.ToArray(), imagePath, request.UserId.ToString());

            var url = $"{Constants.BASE_URL}/User/Photo/{request.UserId}.jpg";

            user.PhotoUrl = url;
            await _dbContext.SaveChangesAsync(cancellationToken);

            return new UserPhotoVm
            {
                Url = url
            };
        }
    }
}
