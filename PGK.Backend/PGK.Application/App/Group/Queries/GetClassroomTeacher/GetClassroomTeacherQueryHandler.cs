using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PGK.Application.Common.Exceptions;
using PGK.Application.App.User.Teacher.Queries.GetTeacherUserDetails;
using PGK.Application.Interfaces;

namespace PGK.Application.App.Group.Queries.GetClassroomTeacher
{
    public class GetClassroomTeacherQueryHandler
        : IRequestHandler<GetClassroomTeacherQuery, TeacherUserDetails>
    {
        private readonly IPGKDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetClassroomTeacherQueryHandler(IPGKDbContext dbContext,
            IMapper mapper) => (_dbContext, _mapper) = (dbContext, mapper);

        public async Task<TeacherUserDetails> Handle(GetClassroomTeacherQuery request,
            CancellationToken cancellationToken)
        {
            var group = await _dbContext.Groups
                .Include(u => u.ClassroomTeacher)
                .FirstOrDefaultAsync(u => u.Id == request.GroupId);

            if(group == null || group.ClassroomTeacher == null)
            {
                throw new NotFoundException(nameof(Domain.Group.Group), request.GroupId);
            }

            return _mapper.Map<TeacherUserDetails>(group.ClassroomTeacher);
        }
    }
}
