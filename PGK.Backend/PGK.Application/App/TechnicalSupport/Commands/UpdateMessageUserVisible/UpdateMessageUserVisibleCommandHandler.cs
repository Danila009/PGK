using AutoMapper;
using MediatR;
using PGK.Application.App.TechnicalSupport.Queries.GetMessageList;
using PGK.Application.Common.Exceptions;
using PGK.Application.Interfaces;
using PGK.Domain.TechnicalSupport;
using PGK.Domain.User;

namespace PGK.Application.App.TechnicalSupport.Commands.UpdateMessageUserVisible
{
    internal class UpdateMessageUserVisibleCommandHandler
        : IRequestHandler<UpdateMessageUserVisibleCommand, MessageDto>
    {
        private readonly IPGKDbContext _dbContext;
        private readonly IMapper _mapper;

        public UpdateMessageUserVisibleCommandHandler(IPGKDbContext dbContext,
            IMapper mapper) => (_dbContext, _mapper) = (dbContext, mapper);

        public async Task<MessageDto> Handle(UpdateMessageUserVisibleCommand request,
            CancellationToken cancellationToken)
        {
            var message = await _dbContext.Messages.FindAsync(request.MessageId);

            if (message == null)
            {
                throw new NotFoundException(nameof(Message), request.MessageId);
            }

            if (request.Role != UserRole.ADMIN)
            {
                var user = await _dbContext.Users.FindAsync(request.UserId);

                if (user == null)
                {
                    throw new NotFoundException(nameof(Domain.User.User), request.UserId);
                }

                if (user.TechnicalSupportChat == null || user.TechnicalSupportChat.Id != message.Chat.Id)
                {
                    throw new UnauthorizedAccessException("У вас нет доступа к этому сообщению");
                }
            }

            message.UserVisible = true;
            await _dbContext.SaveChangesAsync(cancellationToken);

            return _mapper.Map<MessageDto>(message);
        }
    }
}
