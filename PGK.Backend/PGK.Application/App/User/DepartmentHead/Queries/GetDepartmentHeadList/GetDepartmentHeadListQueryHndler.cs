using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using PGK.Application.Common.Paged;
using PGK.Application.Interfaces;
using PGK.Domain.User.DepartmentHead;

namespace PGK.Application.App.User.DepartmentHead.Queries.GetDepartmentHeadList
{
    internal class GetDepartmentHeadListQueryHndler
        : IRequestHandler<GetDepartmentHeadListQuery, DepartmentHeadListVm>
    {
        private readonly IPGKDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetDepartmentHeadListQueryHndler(IPGKDbContext dbContext,
            IMapper mapper) => (_dbContext, _mapper) = (dbContext, mapper);

        public async Task<DepartmentHeadListVm> Handle(GetDepartmentHeadListQuery request,
            CancellationToken cancellationToken)
        {
            IQueryable<DepartmentHeadUser> query = _dbContext.DepartmentHeadUsers;

            if (!string.IsNullOrEmpty(request.Search))
            {
                var search = request.Search.ToLower().Trim();
                query = query.Where(u =>
                    u.FirstName.ToLower().Contains(search) ||
                    u.LastName.ToLower().Contains(search)
                );
            }

            var departmentHead = query
                .ProjectTo<DepartmentHeadDto>(_mapper.ConfigurationProvider);


            var departmentHeadPaged = await PagedList<DepartmentHeadDto>.ToPagedList(departmentHead,
                request.PageNumber, request.PageSize);

            return new DepartmentHeadListVm
            {
                Results = departmentHeadPaged
            };
        }
    }
}
