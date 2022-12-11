using MediatR;
using PGK.Application.Interfaces;
using PGK.Application.Common.Exceptions;

namespace PGK.Application.App.User.Commands.UpdateTelegramId
{
    internal class UpdateTelegramIdCommandHandler
        : IRequestHandler<UpdateTelegramIdCommand>
    {
        private readonly IPGKDbContext _dbContext;

        public UpdateTelegramIdCommandHandler(IPGKDbContext dbContext) =>
            _dbContext = dbContext;

        public async Task<Unit> Handle(UpdateTelegramIdCommand request,
            CancellationToken cancellationToken)
        {
            var user = await _dbContext.Users.FindAsync(request.UserId);

            if(user == null)
            {
                throw new NotFoundException(nameof(Domain.User.User), request.UserId);
            }

            user.TelegramId = request.TelegramId;

            await _dbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
