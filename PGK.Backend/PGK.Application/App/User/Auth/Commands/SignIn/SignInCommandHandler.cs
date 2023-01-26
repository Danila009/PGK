using MediatR;
using PGK.Application.Common;
using PGK.Application.Interfaces;
using PGK.Application.Security;
using PGK.Application.Common.Exceptions;
using Microsoft.EntityFrameworkCore;
using PGK.Domain.User;

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
            var firstName = request.FirstName.ToLower().Trim();
            var lastName = request.LastName.ToLower().Trim();

            var users = await _dbContext.Users.Where(u =>
                u.FirstName.Trim().ToLower() == firstName &&
                u.LastName.Trim().ToLower() == lastName
                ).ToListAsync(cancellationToken);

            if (users == null || users.Count < 1)
            {
                return new SignInVm
                {
                    ErrorMessage = "Пользователь не найден"
                };
            }

            Domain.User.User? user = null;

            foreach(var i in users)
            {
                if(user != null)
                {
                    break;
                }

                if (PasswordHash.ValidatePassword(request.Password.Trim(), i.Password.Trim()))
                {
                    user = i;
                }
            };

            if(user == null)
            {
                return new SignInVm
                {
                    ErrorMessage = "Не верный пароль"
                };
            }

            var refreshToken = user.RefreshToken;

            if (refreshToken == null || !_auth.TokenValidation(refreshToken, TokenType.REFRESH_TOKEN))
            {
                refreshToken = _auth.CreateToken();
            }

            var accessToken = _auth.CreateAccessToken(userId: user.Id, userRole: user.Role);

            user.RefreshToken = refreshToken;
            await _dbContext.SaveChangesAsync(cancellationToken);

            int? groupId = null;

            if(user.Role == UserRole.STUDENT.ToString()
                || user.Role == UserRole.HEADMAN.ToString() || user.Role == UserRole.DEPUTY_HEADMAN.ToString())
            {
                var student = await _dbContext.StudentsUsers
                    .Include(u => u.Group)
                    .FirstOrDefaultAsync(u => u.Id == user.Id);

                if(student != null && student.Group != null)
                {
                    groupId = student.Group.Id;
                }
            }

            return new SignInVm
            {
                UserId = user.Id,
                UserRole = user.Role,
                GroupId = groupId,
                DrarkMode = user.DrarkMode,
                ThemeStyle = user.ThemeStyle,
                ThemeFontStyle = user.ThemeFontStyle,
                ThemeFontSize = user.ThemeFontSize,
                ThemeCorners = user.ThemeCorners,
                Language = user.Language,
                Email = user.Email,
                EmailVerification = user.EmailVerification,
                TelegramId = user.TelegramId,
                RefreshToken = refreshToken,
                AccessToken = accessToken
            };
        }
    }
}
