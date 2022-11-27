using MediatR;
using PGK.Application.Interfaces;
using PGK.Application.Common.Exceptions;
using PGK.Domain.User.Teacher;

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

            var group = new Domain.Group.Group
            {
                Number = request.Number,
                Speciality = request.Speciality,
                SpecialityAbbreviation = request.SpecialityAbbreviation,
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
