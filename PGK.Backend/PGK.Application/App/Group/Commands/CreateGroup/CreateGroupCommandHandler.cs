using MediatR;
using PGK.Application.Interfaces;
using PGK.Application.Common.Exceptions;
using PGK.Domain.User.Teacher;
using PGK.Domain.Speciality;
using PGK.Domain.Department;

namespace PGK.Application.App.Group.Commands.CreateGroup
{
    public class CreateGroupCommandHandler
        : IRequestHandler<CreateGroupCommand, CreateGroupVm>
    {
        private readonly IPGKDbContext _dbContext;

        public CreateGroupCommandHandler(IPGKDbContext dbContext) =>
            _dbContext = dbContext;

        public async Task<CreateGroupVm> Handle(CreateGroupCommand request,
            CancellationToken cancellationToken)
        {
            var teacher = await _dbContext.TeacherUsers.FindAsync(request.ClassroomTeacherId);

            if (teacher == null)
            {
                throw new NotFoundException(nameof(TeacherUser), request.ClassroomTeacherId);
            }

            var speciality = await _dbContext.Specialties.FindAsync(request.SpecialityId);

            if(speciality == null)
            {
                throw new NotFoundException(nameof(Domain.Speciality.Speciality), request.SpecialityId);
            }

            var department = await _dbContext.Departments.FindAsync(request.DepartmentId);

            if(department == null)
            {
                throw new NotFoundException(nameof(Domain.Department.Department), request.DepartmentId);
            }

            var group = new Domain.Group.Group
            {
                Course = request.Course,
                Number = request.Number,
                Speciality = speciality,
                Department = department,
                ClassroomTeacher = teacher
            };

            await _dbContext.Groups.AddAsync(group, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return new CreateGroupVm
            {
                Id = group.Id
            };
        }
    }
}
