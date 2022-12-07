using Microsoft.EntityFrameworkCore;
using MediatR;
using PGK.Application.Interfaces;
using PGK.Application.Common.Exceptions;
using PGK.Application.Security;

namespace PGK.Application.App.User.Auth.Commands.RevokeRefreshToken
{
    public class RevokeRefreshTokenCommandHandler
        : IRequestHandler<RevokeRefreshTokenCommand>
    {
        public readonly IPGKDbContext _dbContext;
        private readonly IAuth _auth;

        public RevokeRefreshTokenCommandHandler(IPGKDbContext dbContext, IAuth auth) =>
            (_dbContext, _auth) = (dbContext, auth);

        public async Task<Unit> Handle(RevokeRefreshTokenCommand request,
            CancellationToken cancellationToken)
        {
            var user = await _dbContext.Users
                .FirstOrDefaultAsync(u => u.RefreshToken == request.RefreshToken);

            if (user == null)
            {
                throw new UnauthorizedAccessException($"Invalid token ({request.RefreshToken})");
            }

            if (!_auth.TokenValidation(token: request.RefreshToken, type: TokenType.REFRESH_TOKEN))
            {
                throw new UnauthorizedAccessException("The token has expired");
            }

            user.RefreshToken = null;

            await _dbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
