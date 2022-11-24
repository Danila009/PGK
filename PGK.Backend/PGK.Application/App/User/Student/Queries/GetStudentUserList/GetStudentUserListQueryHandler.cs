using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
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
            IQueryable<StudentUser> queries = _dbContext.StudentsUsers;

            if (!string.IsNullOrEmpty(request.Search))
            {
                var search = request.Search.ToLower();
                queries = queries.Where(u => 
                    u.FirstName.ToLower().Contains(search) ||
                    u.LastName.ToLower().Contains(search)
                );
            }

            var students = await queries
                .ProjectTo<StudentDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);


            return new StudentUserListVm {  };
        }
    }
}
