using MediatR;
using PGK.Application.Common;
using PGK.Application.Interfaces;
using PGK.Application.Security;
using PGK.Domain.User.Admin;

namespace PGK.Application.App.User.Admin.Commands.Registration
{
    public class RegistrationAdminCommandHandler
        : IRequestHandler<RegistrationAdminCommand, RegistrationAdminVm>
    {
        private readonly IPGKDbContext _dbContext;
        private readonly IAuth _auth;

        public RegistrationAdminCommandHandler(IPGKDbContext dbContext,
            IAuth auth) => (_dbContext, _auth) = (dbContext, auth);

        public async Task<RegistrationAdminVm> Handle(RegistrationAdminCommand request,
            CancellationToken cancellationToken)
        {
            var password = Guid.NewGuid().ToString();

            var refreshToken = _auth.CreateToken();

            var passwordHash = PasswordHash.CreateHash(password);

            var user = new AdminUser
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                MiddleName = request.MiddleName,
                Password = passwordHash,
                RefreshToken = refreshToken
            };

            await _dbContext.AdminUsers.AddAsync(user, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

            var accessToken = _auth.CreateAccessToken(userId: user.Id, userRole: user.Role);

            return new RegistrationAdminVm
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
