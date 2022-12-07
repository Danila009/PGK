using MediatR;
using Microsoft.EntityFrameworkCore;
using PGK.Application.Interfaces;
using PGK.Application.Security;

namespace PGK.Application.App.User.Auth.Commands.RefreshToken
{ 
    internal class RefreshTokenCommandHandler
        : IRequestHandler<RefreshTokenCommand, RefreshTokenVm>
    {
        private readonly IPGKDbContext _dbContext;
        private readonly IAuth _auth;

        public RefreshTokenCommandHandler(IPGKDbContext dbContext,
            IAuth auth) => (_dbContext, _auth) = (dbContext, auth);

        public async Task<RefreshTokenVm> Handle(RefreshTokenCommand request,
            CancellationToken cancellationToken)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(
                u => u.RefreshToken == request.RefreshToken);

            if (user == null)
            {
                throw new UnauthorizedAccessException($"Invalid token ({request.RefreshToken})");
            }

            if(!_auth.TokenValidation(token: request.RefreshToken, type: TokenType.REFRESH_TOKEN))
            {
                throw new UnauthorizedAccessException("The token has expired");
            }

            var jwtToken = _auth.CreateAccessToken(userId: user.Id, userRole: user.Role);

            return new RefreshTokenVm { AccessToken = jwtToken };
        }
    }
}
