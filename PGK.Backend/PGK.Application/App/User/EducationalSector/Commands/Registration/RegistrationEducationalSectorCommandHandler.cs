using MediatR;
using PGK.Application.Common;
using PGK.Application.Interfaces;
using PGK.Application.Security;
using PGK.Domain.User.EducationalSector;

namespace PGK.Application.App.User.EducationalSector.Commands.Registration
{
    public class RegistrationEducationalSectorCommandHandler
        : IRequestHandler<RegistrationEducationalSectorCommand, RegistrationEducationalSectorVm>
    {

        private readonly IPGKDbContext _dbContext;
        private readonly IAuth _auth;

        public RegistrationEducationalSectorCommandHandler(IPGKDbContext dbContext,
            IAuth auth) => (_dbContext, _auth) = (dbContext, auth);

        public async Task<RegistrationEducationalSectorVm> Handle(
            RegistrationEducationalSectorCommand request, CancellationToken cancellationToken)
        {
            var password = Guid.NewGuid().ToString();

            var refreshToken = _auth.CreateRefreshToken();

            var passwordHash = PasswordHash.CreateHash(password);

            var user = new EducationalSectorUser
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                MiddleName = request.MiddleName,
                Password = passwordHash,
                RefreshToken = refreshToken
            };

            await _dbContext.EducationalSectorUsers.AddAsync(user, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

            var accessToken = _auth.CreateToken(userId: user.Id, userRole: user.Role);

            return new RegistrationEducationalSectorVm
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
