using MediatR;
using PGK.Application.Common;
using PGK.Application.Interfaces;
using PGK.Application.Security;
using PGK.Domain.User.DeputyHeadma;

namespace PGK.Application.App.User.DeputyHeadma.Commands.Registration
{
    internal class RegistrationDeputyHeadmaCommandHandler
        : IRequestHandler<RegistrationDeputyHeadmaCommand, RegistrationDeputyHeadmaVm>
    {

        private readonly IPGKDbContext _dbContext;
        private readonly IAuth _auth;

        public RegistrationDeputyHeadmaCommandHandler(IPGKDbContext dbContext,
            IAuth auth) => (_dbContext, _auth) = (dbContext, auth);

        public async Task<RegistrationDeputyHeadmaVm> Handle(
            RegistrationDeputyHeadmaCommand request,
            CancellationToken cancellationToken)
        {
            var password = Guid.NewGuid().ToString();

            var refreshToken = _auth.CreateRefreshToken();

            var passwordHash = PasswordHash.CreateHash(password);

            var user = new DeputyHeadmaUser
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                MiddleName = request.MiddleName,
                Password = passwordHash,
                RefreshToken = refreshToken
            };

            await _dbContext.DeputyHeadmaUsers.AddAsync(user, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

            var accessToken = _auth.CreateToken(userId: user.Id, userRole: user.Role);

            return new RegistrationDeputyHeadmaVm
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
