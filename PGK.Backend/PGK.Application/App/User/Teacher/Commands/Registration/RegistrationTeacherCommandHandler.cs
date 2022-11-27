using MediatR;
using PGK.Application.Common;
using PGK.Application.Interfaces;
using PGK.Application.Security;
using PGK.Domain.User.Teacher;

namespace PGK.Application.App.User.Teacher.Commands.Registration
{
    public class RegistrationTeacherCommandHandler
        : IRequestHandler<RegistrationTeacherCommand, RegistrationTeacherVm>
    {
        private readonly IPGKDbContext _dbContext;
        private readonly IAuth _auth;

        public RegistrationTeacherCommandHandler(IPGKDbContext dbContext,
            IAuth auth) => (_dbContext, _auth) = (dbContext, auth);

        public async Task<RegistrationTeacherVm> Handle(RegistrationTeacherCommand request,
            CancellationToken cancellationToken)
        {

            var password = Guid.NewGuid().ToString();

            var refreshToken = _auth.CreateRefreshToken();

            var passwordHash = PasswordHash.CreateHash(password);

            var user = new TeacherUser
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                MiddleName = request.MiddleName,
                Password = passwordHash,
                RefreshToken = refreshToken
            };

            await _dbContext.TeacherUsers.AddAsync(user, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

            var accessToken = _auth.CreateToken(userId: user.Id, userRole: user.Role);

            return new RegistrationTeacherVm
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken,
                UserRole = user.Role,
                UserId = user.Id,
                Passowrd = password
            };
        }
    }
}
