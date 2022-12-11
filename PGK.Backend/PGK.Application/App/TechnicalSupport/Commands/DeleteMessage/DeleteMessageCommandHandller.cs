using MediatR;
using PGK.Application.Interfaces;
using PGK.Application.Common.Exceptions;
using PGK.Domain.TechnicalSupport;
using PGK.Domain.User;

namespace PGK.Application.App.TechnicalSupport.Commands.DeleteMessage
{
    internal class DeleteMessageCommandHandller
        : IRequestHandler<DeleteMessageCommand>
    {
        private readonly IPGKDbContext _dbContext;

        public DeleteMessageCommandHandller(IPGKDbContext dbContext) =>
            _dbContext = dbContext;

        public async Task<Unit> Handle(DeleteMessageCommand request,
            CancellationToken cancellationToken)
        {
            var message = await _dbContext.Messages.FindAsync(request.MessageId);

            if (message == null)
            {
                throw new NotFoundException(nameof(Message), request.MessageId);
            }

            if(request.Role != UserRole.ADMIN)
            {
                var user = await _dbContext.Users.FindAsync(request.UserId);

                if (user == null)
                {
                    throw new NotFoundException(nameof(Domain.User.User), request.UserId);
                }

                if(user.TechnicalSupportChat == null || user.TechnicalSupportChat.Id != message.Chat.Id)
                {
                    throw new UnauthorizedAccessException("У вас нет доступа к этому сообщению");
                }
            }

            _dbContext.Messages.Remove(message);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
