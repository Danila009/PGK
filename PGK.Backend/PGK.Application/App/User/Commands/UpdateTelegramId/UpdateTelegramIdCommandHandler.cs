using MediatR;
using PGK.Application.Interfaces;
using PGK.Application.Common.Exceptions;
using Microsoft.EntityFrameworkCore;
using PGK.Application.Security;

namespace PGK.Application.App.User.Commands.UpdateTelegramId
{
    internal class UpdateTelegramIdCommandHandler
        : IRequestHandler<UpdateTelegramIdCommand>
    {
        private readonly IPGKDbContext _dbContext;
        private readonly IAuth _auth;

        public UpdateTelegramIdCommandHandler(IPGKDbContext dbContext, IAuth auth) =>
            (_dbContext, _auth) = (dbContext, auth);

        public async Task<Unit> Handle(UpdateTelegramIdCommand request,
            CancellationToken cancellationToken)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(u => 
                u.TelegramToken == request.TelegramToken);

            if(user == null)
            {
                throw new NotFoundException(nameof(Domain.User.User), request.TelegramToken);
            }

            if(!_auth.TokenValidation(request.TelegramToken, TokenType.TELEGRAM_TOKEN))
            {
                throw new UnauthorizedAccessException("Telegram token истек");
            }

            user.TelegramToken = null;
            user.TelegramId = request.TelegramId;

            await _dbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
