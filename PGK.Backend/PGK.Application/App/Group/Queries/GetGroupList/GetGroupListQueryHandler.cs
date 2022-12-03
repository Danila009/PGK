using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PGK.Application.App.Group.Queries.GetGroupDetails;
using PGK.Application.Common.Paged;
using PGK.Application.Interfaces;

namespace PGK.Application.App.Group.Queries.GetGroupList
{
    public class GetGroupListQueryHandler
        : IRequestHandler<GetGroupListQuery, GroupListVm>
    {
        private readonly IPGKDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetGroupListQueryHandler(IPGKDbContext dbContext,
            IMapper mapper) => (_dbContext, _mapper) = (dbContext, mapper);

        public async Task<GroupListVm> Handle(GetGroupListQuery request,
            CancellationToken cancellationToken)
        {
            IQueryable<Domain.Group.Group> query = _dbContext.Groups
                .Include(u => u.ClassroomTeacher)
                .Include(u => u.Speciality)
                .Include(u => u.Department)
                .Include(u => u.DeputyHeadma)
                .Include(u => u.Headman);

            if (!string.IsNullOrEmpty(request.Search))
            {
                var search = request.Search.ToLower();

                query = query.Where(u => u.Number.ToString().ToLower().Contains(search) ||
                    u.Speciality.Name.ToLower().Contains(search));
            }

            var groups = query
                .ProjectTo<GroupDetails>(_mapper.ConfigurationProvider);


            var groupsPaged = await PagedList<GroupDetails>.ToPagedList(groups, request.PageNumber, 
                request.PageSize);

            return new GroupListVm { Results =  groupsPaged };
        }
    }
}
