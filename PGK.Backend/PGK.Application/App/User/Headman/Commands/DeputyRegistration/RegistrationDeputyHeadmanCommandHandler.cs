using MediatR;
using Microsoft.EntityFrameworkCore;
using PGK.Application.App.User.Headman.Commands.Registration;
using PGK.Application.Common.Exceptions;
using PGK.Application.Interfaces;
using PGK.Application.Security;
using PGK.Domain.User.DeputyHeadma;
using PGK.Domain.User.Student;

namespace PGK.Application.App.User.Headman.Commands.DeputyRegistration
{
    internal class RegistrationDeputyHeadmanCommandHandler
        : IRequestHandler<RegistrationDeputyHeadmanCommand, RegistrationHeadmanVm>
    {
        private readonly IPGKDbContext _dbContext;
        private readonly IAuth _auth;

        public RegistrationDeputyHeadmanCommandHandler(IPGKDbContext dbContext, IAuth auth) =>
            (_dbContext, _auth) = (dbContext, auth);

        public async Task<RegistrationHeadmanVm> Handle(RegistrationDeputyHeadmanCommand request,
            CancellationToken cancellationToken)
        {
            var student = await _dbContext.StudentsUsers
                .Include(u => u.Group)
                    .ThenInclude(u => u.ClassroomTeacher)
                .FirstOrDefaultAsync(u => u.Id == request.StudentId);

            if (student == null)
            {
                throw new NotFoundException(nameof(StudentUser), request.StudentId);
            }

            if (student.Group.ClassroomTeacher.Id != request.TeacherId)
            {
                throw new Exception("Группа может быть изменена только классным руководителям");
            }

            var refreshToken = _auth.CreateToken();

            var headman = new DeputyHeadmaUser
            {
                Id = student.Id,
                FirstName = student.FirstName,
                LastName = student.LastName,
                MiddleName = student.MiddleName,
                Password = student.Password,
                Email = student.Email,
                RefreshToken = refreshToken,
                Group = student.Group
            };

            var accessToken = _auth.CreateAccessToken(userId: headman.Id, userRole: headman.Role);

            _dbContext.StudentsUsers.Remove(student);
            await _dbContext.DeputyHeadmaUsers.AddAsync(headman, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return new RegistrationHeadmanVm
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken,
                UserRole = headman.Role
            };
        }
    }
}
