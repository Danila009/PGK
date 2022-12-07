using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using PGK.Application.Common.Paged;
using PGK.Application.Interfaces;
using PGK.Domain.User.Admin;

namespace PGK.Application.App.User.Admin.Queries.GetAdminList
{
    internal class GetAdminListQueryHandler
        : IRequestHandler<GetAdminListQuery, AdminListVm>
    {
        private readonly IPGKDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetAdminListQueryHandler(IPGKDbContext dbContext,
            IMapper mapper) => (_dbContext, _mapper) = (dbContext, mapper);

        public async Task<AdminListVm> Handle(GetAdminListQuery request,
            CancellationToken cancellationToken)
        {
            IQueryable<AdminUser> query = _dbContext.AdminUsers;

            if (!string.IsNullOrEmpty(request.Search))
            {
                var search = request.Search.ToLower().Trim();
                query = query.Where(u =>
                    u.FirstName.ToLower().Contains(search) ||
                    u.LastName.ToLower().Contains(search)
                );
            }

            var admins = query
                .ProjectTo<AdminDto>(_mapper.ConfigurationProvider);


            var adminsPaged = await PagedList<AdminDto>.ToPagedList(admins,
                request.PageNumber, request.PageSize);

            return new AdminListVm { Results = adminsPaged };
        }
    }
}
