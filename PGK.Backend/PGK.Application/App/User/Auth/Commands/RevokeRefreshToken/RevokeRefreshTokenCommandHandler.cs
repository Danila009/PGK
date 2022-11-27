using Microsoft.EntityFrameworkCore;
using MediatR;
using PGK.Application.Interfaces;
using PGK.Application.Common.Exceptions;

namespace PGK.Application.App.User.Auth.Commands.RevokeRefreshToken
{
    public class RevokeRefreshTokenCommandHandler
        : IRequestHandler<RevokeRefreshTokenCommand>
    {
        public readonly IPGKDbContext _dbContext;

        public RevokeRefreshTokenCommandHandler(IPGKDbContext dbContext) =>
            _dbContext = dbContext;

        public async Task<Unit> Handle(RevokeRefreshTokenCommand request,
            CancellationToken cancellationToken)
        {
            var user = await _dbContext.Users
                .FirstOrDefaultAsync(u => u.RefreshToken == request.RefreshToken);

            if (user == null)
            {
                throw new NotFoundException(nameof(User), request.RefreshToken);
            }

            user.RefreshToken = null;

            await _dbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
