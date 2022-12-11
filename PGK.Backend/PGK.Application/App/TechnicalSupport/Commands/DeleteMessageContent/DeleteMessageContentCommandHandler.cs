using MediatR;
using PGK.Application.Interfaces;
using PGK.Application.Common.Exceptions;
using PGK.Domain.TechnicalSupport;

namespace PGK.Application.App.TechnicalSupport.Commands.DeleteMessageContent
{
    internal class DeleteMessageContentCommandHandler
        : IRequestHandler<DeleteMessageContentCommand>
    {
        private readonly IPGKDbContext _dbContext;

        public DeleteMessageContentCommandHandler(IPGKDbContext dbContext) =>
            _dbContext = dbContext;

        public async Task<Unit> Handle(DeleteMessageContentCommand request,
            CancellationToken cancellationToken)
        {
            var content = await _dbContext.MessageContents.FindAsync(request.MessageContentId);

            if(content == null)
            {
                throw new NotFoundException(nameof(MessageContent), request.MessageContentId);
            }

            var user = await _dbContext.Users.FindAsync(request.UserId);

            if (user == null)
            {
                throw new NotFoundException(nameof(Domain.User.User), request.UserId);
            }

            if (!user.TechnicalSupportMessages.Any(u => u.Id == content.Message.Id))
            {
                throw new UnauthorizedAccessException("У вас нет доступа к этому сообщению");
            }

            _dbContext.MessageContents.Remove(content);
            await _dbContext.SaveChangesAsync(cancellationToken);
        
            return Unit.Value;
        }
    }
}
