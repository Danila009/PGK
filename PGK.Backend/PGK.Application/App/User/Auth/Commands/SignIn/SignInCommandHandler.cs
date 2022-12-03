using MediatR;
using PGK.Application.Common;
using PGK.Application.Interfaces;
using PGK.Application.Security;
using Microsoft.EntityFrameworkCore;

namespace PGK.Application.App.User.Auth.Commands.SignIn
{
    public class SignInCommandHandler
        : IRequestHandler<SignInCommand, SignInVm>
    {
        private readonly IPGKDbContext _dbContext;
        private readonly IAuth _auth;

        public SignInCommandHandler(IPGKDbContext dbContext,
            IAuth auth) => (_dbContext, _auth) = (dbContext, auth);

        public async Task<SignInVm> Handle(SignInCommand request,
            CancellationToken cancellationToken)
        {
            var users = await _dbContext.Users.Where(u =>
                u.FirstName == request.FirstName &&
                u.LastName == request.LastName
                ).ToListAsync(cancellationToken);

            if (users == null || users.Count < 1)
            {
                throw new Exception("Пользователь не найден");
            }

            Domain.User.User? user = null;

            foreach(var i in users)
            {
                if(user != null)
                {
                    break;
                }

                if (PasswordHash.ValidatePassword(request.Password, i.Password))
                {
                    user = i;
                }
            };

            if(user == null)
            {
                throw new Exception("Не верный пароль");
            }

            var refreshToken = _auth.CreateRefreshToken();
            var accessToken = _auth.CreateToken(userId: user.Id, userRole: user.Role);

            user.RefreshToken = refreshToken;
            await _dbContext.SaveChangesAsync(cancellationToken);

            return new SignInVm
            {
                UserId = user.Id,
                UserRole = user.Role,
                RefreshToken = refreshToken,
                AccessToken = accessToken
            };
        }
    }
}
