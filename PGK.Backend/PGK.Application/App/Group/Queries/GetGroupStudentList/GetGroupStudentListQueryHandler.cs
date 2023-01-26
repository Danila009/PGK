using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PGK.Application.App.User.Student.Queries.GetStudentUserList;
using PGK.Application.Common.Paged;
using PGK.Application.Interfaces;

namespace PGK.Application.App.Group.Queries.GetGroupStudentList
{
    internal class GetGroupStudentListQueryHandler
        : IRequestHandler<GetGroupStudentListQuery, GroupStudentListVm>
    {
        private readonly IPGKDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetGroupStudentListQueryHandler(IPGKDbContext dbContext,
            IMapper mapper) => (_dbContext, _mapper) = (dbContext, mapper);

        public async Task<GroupStudentListVm> Handle(GetGroupStudentListQuery request,
            CancellationToken cancellationToken)
        {
            var query = _dbContext.StudentsUsers
                .Include(u => u.Group)
                .Where(u => u.Group.Id == request.GroupId);

            var students = query
                .OrderByDescending(u => u.Id)
                .ProjectTo<StudentDto>(_mapper.ConfigurationProvider);


            var studentsPaged = await PagedList<StudentDto>.ToPagedList(students, request.PageNumber,
                request.PageSize);

            return new GroupStudentListVm
            {
                Results = studentsPaged
            };
        }
    }
}
