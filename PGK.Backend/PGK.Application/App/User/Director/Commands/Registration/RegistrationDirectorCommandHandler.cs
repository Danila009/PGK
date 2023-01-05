using MediatR;
using Microsoft.EntityFrameworkCore;
using PGK.Application.Common;
using PGK.Application.Interfaces;
using PGK.Application.Security;
using PGK.Domain.User.Director;

namespace PGK.Application.App.User.Director.Commands.Registration
{
    internal class RegistrationDirectorCommandHandler
        : IRequestHandler<RegistrationDirectorCommand, RegistrationDirectorVm>
    {
        private readonly IPGKDbContext _dbContext;
        private readonly IAuth _auth;

        public RegistrationDirectorCommandHandler(IPGKDbContext dbContext,
            IAuth auth) => (_dbContext, _auth) = (dbContext, auth);

        public async Task<RegistrationDirectorVm> Handle(RegistrationDirectorCommand request,
            CancellationToken cancellationToken)
        {
            var password = Guid.NewGuid().ToString();

            var refreshToken = _auth.CreateToken();

            var passwordHash = PasswordHash.CreateHash(password);

            var user = new DirectorUser
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                MiddleName = request.MiddleName,
                Password = passwordHash,
                RefreshToken = refreshToken
            };

            var directorCurrent = await _dbContext.DirectorUsers
                .FirstOrDefaultAsync(u => u.Current == true, cancellationToken);

            if(directorCurrent != null)
            {
                directorCurrent.Current = false;
            }

            await _dbContext.DirectorUsers.AddAsync(user, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

            var accessToken = _auth.CreateAccessToken(userId: user.Id, userRole: user.Role);

            return new RegistrationDirectorVm
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
