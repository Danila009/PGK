using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using PGK.Application.App.User.Teacher.Queries.GetTeacherUserDetails;
using PGK.Application.Common.Paged;
using PGK.Application.Interfaces;
using PGK.Domain.User.Teacher;

namespace PGK.Application.App.User.Teacher.Queries.GetTeacherUserList
{
    public class GetTeacherUserListQueryHandler
        : IRequestHandler<GetTeacherUserListQuery, TeacherUserListVm>
    {
        private readonly IPGKDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetTeacherUserListQueryHandler(IPGKDbContext dbContext,
            IMapper mapper) => (_dbContext, _mapper) = (dbContext, mapper);

        public async Task<TeacherUserListVm> Handle(GetTeacherUserListQuery request,
            CancellationToken cancellationToken)
        {
            IQueryable<TeacherUser> query = _dbContext.TeacherUsers;


            if (!string.IsNullOrEmpty(request.Search))
            {
                var search = request.Search.ToLower();
                query = query.Where(u =>
                    u.FirstName.ToLower().Contains(search) ||
                    u.LastName.ToLower().Contains(search)
                );
            }

            var teachers = query
                .ProjectTo<TeacherUserDetails>(_mapper.ConfigurationProvider);

            var teachersPaged = await PagedList<TeacherUserDetails>.ToPagedList(teachers,
                request.PageNumber, request.PageSize);

            return new TeacherUserListVm { Results = teachersPaged };
        }
    }
}
