using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PGK.Application.App.Group.Queries.GetGroupDetails;
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
            IQueryable<Domain.Group.Group> query = _dbContext.Groups;

            var groups = await query
                .ProjectTo<GroupDetails>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            return new GroupListVm { Groups = groups };
        }
    }
}
