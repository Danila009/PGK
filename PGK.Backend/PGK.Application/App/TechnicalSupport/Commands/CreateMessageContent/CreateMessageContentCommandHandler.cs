using AutoMapper;
using MediatR;
using PGK.Application.App.TechnicalSupport.Queries.GetMessageContentList;
using PGK.Application.Common;
using PGK.Application.Common.Exceptions;
using PGK.Application.Interfaces;
using PGK.Application.Repository.FileRepository;
using PGK.Application.Repository.ImageRepository;
using PGK.Domain.TechnicalSupport;
using PGK.Domain.TechnicalSupport.Enums;

namespace PGK.Application.App.TechnicalSupport.Commands.CreateMessageContent
{
    internal class CreateMessageContentCommandHandler
        : IRequestHandler<CreateMessageContentCommand, MessageContentDto>
    {
        private readonly IPGKDbContext _dbContext;
        private readonly IFileRepository _fileRepository;
        private readonly IImageRepository _imageRepository;
        private readonly IMapper _mapper;

        public CreateMessageContentCommandHandler(IPGKDbContext dbContext
            , IFileRepository fileRepository, IMapper mapper, IImageRepository imageRepository) =>
            (_dbContext, _fileRepository, _imageRepository, _mapper) = 
            (dbContext, fileRepository, imageRepository, mapper);


        public async Task<MessageContentDto> Handle(CreateMessageContentCommand request,
            CancellationToken cancellationToken)
        {
            var message = await _dbContext.Messages.FindAsync(request.MessageId);

            if (message == null)
            {
                throw new NotFoundException(nameof(Message), request.MessageId);
            }

            var user = await _dbContext.Users.FindAsync(request.UserId);

            if (user == null)
            {
                throw new NotFoundException(nameof(Domain.User.User), request.UserId);
            }

            if (!user.TechnicalSupportMessages.Any(u => u.Id == message.Id))
            {
                throw new UnauthorizedAccessException("У вас нет доступа к этому сообщению");
            }

            string fileId;

            if(request.Type == MessageContentType.IMAGE)
            {
                MemoryStream memoryStream = new MemoryStream();
                await request.File.CopyToAsync(memoryStream);

                fileId = _imageRepository.Save(
                    imgBytes: memoryStream.ToArray(),
                    path: Constants.TECHNICAL_SUPPORT_MESSAGE_CONTENT_PATH);
            }
            else
            {
                fileId = await _fileRepository.UploadFile(
                    file: request.File,
                    path: Constants.TECHNICAL_SUPPORT_MESSAGE_CONTENT_PATH);
            }

            var content = new MessageContent
            {
                Url = $"{Constants.BASE_URL}/TechnicalSupport/Chat/Message/Content/{fileId}?type={request.Type}",
                Type = request.Type,
                Message = message
            };

            await _dbContext.MessageContents.AddAsync(content, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return _mapper.Map<MessageContentDto>(content);
        }
    }
}
