using MediatR;
using PGK.Application.Interfaces;
using PGK.Domain.TechnicalSupport;
using PGK.Application.Common.Exceptions;
using PGK.Domain.User;
using PGK.Application.App.TechnicalSupport.Queries.GetMessageList;
using AutoMapper;

namespace PGK.Application.App.TechnicalSupport.Commands.SendMessage
{
    internal class SendMessageCommandHandler
        : IRequestHandler<SendMessageCommand, MessageDto>
    {
        private readonly IPGKDbContext _dbContext;
        private readonly IMapper _mapper;

        public SendMessageCommandHandler(IPGKDbContext dbContext, IMapper mapper) =>
            (_dbContext, _mapper) = (dbContext, mapper);

        public async Task<MessageDto> Handle(SendMessageCommand request,
            CancellationToken cancellationToken)
        {
            var user = await _dbContext.Users.FindAsync(request.UserId);

            if(user == null)
            {
                throw new NotFoundException(nameof(Domain.User.User), request.UserId);
            }

            Chat chat;


            if (request.ChatId == null)
            {
                if (user.TechnicalSupportChat == null)
                {
                    var newUserChat = await CreateNewChat(cancellationToken);

                    user.TechnicalSupportChat = newUserChat;

                    chat = newUserChat;
                }
                else
                {
                    chat = user.TechnicalSupportChat;
                }
            }
            else
            {
                if(request.Role == UserRole.ADMIN)
                {
                    chat = await _dbContext.Chats.FindAsync(request.ChatId) ??
                        throw new NotFoundException(nameof(Chat), request.ChatId);
                }
                else
                {
                    throw new UnauthorizedAccessException("У вас нет доступа к этому чату");
                }
            }

            var message = new Message
            {
                User = user,
                Text = request.Text,
                Chat = chat
            };

            await _dbContext.Messages.AddAsync(message, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return _mapper.Map<MessageDto>(message);
        }

        private async Task<Chat> CreateNewChat(CancellationToken cancellationToken)
        {
            var newUserChat = new Chat();

            await _dbContext.Chats.AddAsync(newUserChat, cancellationToken);

            return newUserChat;
        }
    }
}
