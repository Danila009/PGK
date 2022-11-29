using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PGK.Application.Common.Paged;
using PGK.Application.Interfaces;
using PGK.Domain.User.Student;

namespace PGK.Application.App.User.Student.Queries.GetStudentUserList
{
    public class GetStudentUserListQueryHandler
        : IRequestHandler<GetStudentUserListQuery, StudentUserListVm>
    {
        private readonly IPGKDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetStudentUserListQueryHandler(IPGKDbContext dbContext,
            IMapper mapper) => (_dbContext, _mapper) = (dbContext, mapper);

        public async Task<StudentUserListVm> Handle(GetStudentUserListQuery request,
            CancellationToken cancellationToken)
        {
            IQueryable<StudentUser> queries = _dbContext.StudentsUsers
                .Include(u => u.Group)
                    .ThenInclude(u => u.ClassroomTeacher)
                .Include(u => u.Group)
                    .ThenInclude(u => u.Department)
                .Include(u => u.Group)
                    .ThenInclude(u => u.Speciality);

            if (!string.IsNullOrEmpty(request.Search))
            {
                var search = request.Search.ToLower();
                queries = queries.Where(u => 
                    u.FirstName.ToLower().Contains(search) ||
                    u.LastName.ToLower().Contains(search)
                );
            }

            if(request.UserRoles != null && request.UserRoles.Count > 0)
            {
                queries = queries.Where(u => request.UserRoles.Contains(u.Role));
            }

            if(request.GroupIds != null && request.GroupIds.Count > 0)
            {
                queries = queries.Where(u => request.GroupIds.Contains(u.Group.Id));
            }

            var students = queries
                .ProjectTo<StudentDto>(_mapper.ConfigurationProvider);


            var studentPaged = await PagedList<StudentDto>.ToPagedList(students,
                request.PageNumber, request.PageSize);

            return new StudentUserListVm { Results = studentPaged };
        }
    }
}
