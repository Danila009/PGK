using AutoMapper;
using MediatR;
using PGK.Application.App.Group.Queries.GetGroupDetails;
using PGK.Application.Common.Exceptions;
using PGK.Application.Interfaces;
using PGK.Domain.User.Teacher;

namespace PGK.Application.App.Group.Commands.UpdateGroup
{
    internal class UpdateGroupCommandHandler
        : IRequestHandler<UpdateGroupCommand, GroupDetails>
    {
        private readonly IPGKDbContext _dbContext;
        private readonly IMapper _mapper;

        public UpdateGroupCommandHandler(IPGKDbContext dbContext,
            IMapper mapper) => (_dbContext, _mapper) = (dbContext, mapper);

        public async Task<GroupDetails> Handle(UpdateGroupCommand request,
            CancellationToken cancellationToken)
        {
            var group = await _dbContext.Groups.FindAsync(request.Id);

            if(group == null)
            {
                throw new NotFoundException(nameof(Domain.Group.Group), request.Id);
            }

            var teacher = await _dbContext.TeacherUsers.FindAsync(request.ClassroomTeacherId);

            if (teacher == null)
            {
                throw new NotFoundException(nameof(TeacherUser), request.ClassroomTeacherId);
            }

            var speciality = await _dbContext.Specialties.FindAsync(request.SpecialityId);

            if (speciality == null)
            {
                throw new NotFoundException(nameof(Speciality), request.SpecialityId);
            }

            var department = await _dbContext.Departments.FindAsync(request.DepartmentId);

            if (department == null)
            {
                throw new NotFoundException(nameof(Department), request.DepartmentId);
            }

            var headman = await _dbContext.HeadmanUsers.FindAsync(request.HeadmanId);

            var deputyHeadma = await _dbContext.DeputyHeadmaUsers.FindAsync(request.DeputyHeadmaId);

            group.Number = request.Number;
            group.Speciality = speciality;
            group.Department = department;
            group.ClassroomTeacher = teacher;
            group.Headman = headman;
            group.DeputyHeadma = deputyHeadma;

            await _dbContext.SaveChangesAsync(cancellationToken);

            return _mapper.Map<GroupDetails>(group);
        }
    }
}
