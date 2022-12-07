using MediatR;
using PGK.Application.Common;
using PGK.Application.Common.Exceptions;
using PGK.Application.Interfaces;
using PGK.Application.Security;
using PGK.Domain.User.Student;

namespace PGK.Application.App.User.Student.Commands.Registration
{
    public class RegistrationStudentCommandHandler
        : IRequestHandler<RegistrationStudentCommand, RegistrationStudentVm>
    {
        private readonly IPGKDbContext _dbContext;
        private readonly IAuth _auth;

        public RegistrationStudentCommandHandler(IPGKDbContext dbContext,
            IAuth auth) => (_dbContext, _auth) = (dbContext, auth);

        public async Task<RegistrationStudentVm> Handle(RegistrationStudentCommand request,
            CancellationToken cancellationToken)
        {
            var group = await _dbContext.Groups.FindAsync(request.GroupId);

            if (group == null)
            {
                throw new NotFoundException(nameof(Group), request.GroupId);
            }

            var password = Guid.NewGuid().ToString();

            var refreshToken = _auth.CreateToken();

            var passwordHash = PasswordHash.CreateHash(password);

            var user = new StudentUser
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                MiddleName = request.MiddleName,
                Password = passwordHash,
                Group = group,
                RefreshToken = refreshToken
            };

            await _dbContext.StudentsUsers.AddAsync(user, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

            var accessToken = _auth.CreateAccessToken(userId: user.Id, userRole: user.Role);

            return new RegistrationStudentVm
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
