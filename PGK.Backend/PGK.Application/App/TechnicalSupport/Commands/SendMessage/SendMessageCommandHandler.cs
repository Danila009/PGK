using MediatR;
using PGK.Application.Interfaces;
using PGK.Domain.TechnicalSupport;
using PGK.Application.Common.Exceptions;
using PGK.Domain.User;
using PGK.Application.App.TechnicalSupport.Queries.GetMessageList;
using AutoMapper;
using PGK.Application.Services.FCMService;
using Microsoft.EntityFrameworkCore;
using PGK.Domain.Notification;

namespace PGK.Application.App.TechnicalSupport.Commands.SendMessage
{
    public class SendMessageCommandHandler
        : IRequestHandler<SendMessageCommand, MessageDto>
    {
        private readonly IPGKDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IFCMService _fCMService;

        public SendMessageCommandHandler(IPGKDbContext dbContext, IMapper mapper, IFCMService fCMService) =>
            (_dbContext, _mapper, _fCMService) = (dbContext, mapper, fCMService);

        public async Task<MessageDto> Handle(SendMessageCommand request,
            CancellationToken cancellationToken)
        {
            var fromUser = await _dbContext.Users.FindAsync(request.UserId);

            if(fromUser == null)
            {
                throw new NotFoundException(nameof(Domain.User.User), request.UserId);
            }

            Chat chat;

            if (request.ChatId == null)
            {
                if (fromUser.TechnicalSupportChat == null)
                {
                    var newUserChat = await CreateNewChat(cancellationToken);

                    fromUser.TechnicalSupportChat = newUserChat;

                    chat = newUserChat;
                }
                else
                {
                    chat = fromUser.TechnicalSupportChat;
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
                User = fromUser,
                Text = request.Text,
                Chat = chat
            };

            await _dbContext.Messages.AddAsync(message, cancellationToken);

            Notification notification;

            if (request.Role == UserRole.ADMIN && request.ChatId != null)
            {
                var toUser = await GetUserByChatId(request.ChatId.Value);

                notification = new Notification
                {
                    Title = $"{fromUser.FirstName} {fromUser.LastName}",
                    Message = $"{message.Text ?? "контент"}",
                    Users = new List<Domain.User.User> { toUser }
                };

                if (toUser.IncludedTechnicalSupportNotifications)
                {
                    await _fCMService.SendMessage($"{fromUser.FirstName} {fromUser.LastName}",
                    $"{message.Text ?? "контент"}", $"user_{toUser.Id}");
                }
            }
            else
            {
                notification = new Notification
                {
                    Title = $"{fromUser.FirstName} {fromUser.LastName}",
                    Message = $"{message.Text ?? "контент"}",
                    Users = new List<Domain.User.User>()
                };

                await _fCMService.SendMessage($"{fromUser.FirstName} {fromUser.LastName}",
                    $"{message.Text ?? "контент"}", "admin");
            }

            await _dbContext.Notifications.AddAsync(notification, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return _mapper.Map<MessageDto>(message);
        }

        private async Task<Chat> CreateNewChat(CancellationToken cancellationToken)
        {
            var newUserChat = new Chat();

            await _dbContext.Chats.AddAsync(newUserChat, cancellationToken);

            return newUserChat;
        }

        private async Task<Domain.User.User> GetUserByChatId(int chatId)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(
                u => u.TechnicalSupportChat.Id == chatId);

            if(user == null)
            {
                throw new NotFoundException(nameof(Domain.User.User), chatId);
            }

            return user;
        }
    }
}
