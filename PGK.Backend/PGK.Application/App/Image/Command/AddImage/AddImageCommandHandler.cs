using MediatR;
using PGK.Application.Common;
using PGK.Application.Repository.ImageRepository;

namespace PGK.Application.App.Image.Command.AddImage
{
    internal class AddImageCommandHandler : IRequestHandler<AddImageCommand>
    {
        private readonly IImageRepository _imageRepository;

        public AddImageCommandHandler(IImageRepository imageRepository) =>
            _imageRepository = imageRepository;

        public async Task<Unit> Handle(AddImageCommand request,
            CancellationToken cancellationToken)
        {
            MemoryStream memoryStream = new MemoryStream();
            await request.Image.CopyToAsync(memoryStream);

            var imagePath = Constants.IMAGES;

            _imageRepository.Save(
                memoryStream.ToArray(),
                imagePath,
                request.ImageName.ToString(),
                request.Extension);

            return Unit.Value;
        }
    }
}
