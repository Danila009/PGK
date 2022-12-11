using MediatR;
using PGK.Application.Common;
using PGK.Application.Repository.FileRepository;
using PGK.Application.Repository.ImageRepository;
using PGK.Domain.TechnicalSupport.Enums;

namespace PGK.Application.App.TechnicalSupport.Queries.GetMessageContentFile
{
    internal class GetMessageContentFileQueryHandler
        : IRequestHandler<GetMessageContentFileQuery, byte[]>
    {
        private readonly IFileRepository _fileRepository;
        private readonly IImageRepository _imageRepository;

        public GetMessageContentFileQueryHandler(IFileRepository fileRepository,
            IImageRepository imageRepository) => (_fileRepository, _imageRepository) =
                (fileRepository, imageRepository);

        public async Task<byte[]> Handle(GetMessageContentFileQuery request,
            CancellationToken cancellationToken)
        {
            var path = Constants.TECHNICAL_SUPPORT_MESSAGE_CONTENT_PATH;

            if (request.Type == MessageContentType.IMAGE)
            {
                var image = _imageRepository.Get($"{path}{request.FileId}");

                if (image == null)
                {
                    throw new Exception($"Not found image {request.FileId}");
                }

                return image;
            }
            else
            {
                var file = _fileRepository.GetFile($"{path}{request.FileId}");

                if (file == null)
                {
                    throw new Exception($"Not found file {request.FileId}");
                }

                return file;
            }
        }
    }
}
