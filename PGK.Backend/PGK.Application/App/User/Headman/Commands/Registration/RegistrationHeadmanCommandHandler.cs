using MediatR;
using PGK.Application.Interfaces;
using PGK.Application.Common.Exceptions;
using PGK.Domain.User.Student;
using Microsoft.EntityFrameworkCore;
using PGK.Domain.User.Headman;
using PGK.Application.Security;

namespace PGK.Application.App.User.Headman.Commands.Registration
{
    public class RegistrationHeadmanCommandHandler
        : IRequestHandler<RegistrationHeadmanCommand, RegistrationHeadmanVm>
    {
        private readonly IPGKDbContext _dbContext;
        private readonly IAuth _auth;

        public RegistrationHeadmanCommandHandler(IPGKDbContext dbContext, IAuth auth) =>
            (_dbContext, _auth) = (dbContext, auth);

        public async Task<RegistrationHeadmanVm> Handle(RegistrationHeadmanCommand request,
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

            var refreshToken = _auth.CreateRefreshToken();

            var headman = new HeadmanUser
            {
                Id = student.Id,
                FirstName = student.FirstName,
                LastName = student.LastName,
                MiddleName = student.MiddleName,
                Password = student.Password,
                RefreshToken = refreshToken,
                Group = student.Group
            };

            var accessToken = _auth.CreateToken(userId: headman.Id, userRole: headman.Role);

            _dbContext.StudentsUsers.Remove(student);
            await _dbContext.HeadmanUsers.AddAsync(headman, cancellationToken);
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
