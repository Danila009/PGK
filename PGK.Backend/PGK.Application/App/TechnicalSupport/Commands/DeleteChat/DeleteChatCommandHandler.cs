using MediatR;
using Microsoft.EntityFrameworkCore;
using PGK.Application.Interfaces;
using PGK.Application.Common.Exceptions;

namespace PGK.Application.App.TechnicalSupport.Commands.DeleteChat
{
    internal class DeleteChatCommandHandler
        : IRequestHandler<DeleteChatCommand>
    {
        private readonly IPGKDbContext _dbContext;

        public DeleteChatCommandHandler(IPGKDbContext dbContext) =>
            _dbContext = dbContext;

        public async Task<Unit> Handle(DeleteChatCommand request,
            CancellationToken cancellationToken)
        {
            var user = await _dbContext.Users
                .Include(u => u.TechnicalSupportChat)
                .FirstOrDefaultAsync(u => u.Id == request.UserId);

            if (user == null)
            {
                throw new NotFoundException(nameof(Domain.User.User), request.UserId);
            }

            if(user.TechnicalSupportChat != null)
            {
                _dbContext.Chats.Remove(user.TechnicalSupportChat);
                user.TechnicalSupportChat = null;
                await _dbContext.SaveChangesAsync(cancellationToken);
            }

            return Unit.Value;
        }
    }
}
