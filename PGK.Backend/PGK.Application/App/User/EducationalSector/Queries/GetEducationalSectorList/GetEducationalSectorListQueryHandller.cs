using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using PGK.Application.Common.Paged;
using PGK.Application.Interfaces;
using PGK.Domain.User.EducationalSector;

namespace PGK.Application.App.User.EducationalSector.Queries.GetEducationalSectorList
{
    internal class GetEducationalSectorListQueryHandller
        : IRequestHandler<GetEducationalSectorListQuery, EducationalSectorListVm>
    {
        private readonly IPGKDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetEducationalSectorListQueryHandller(IPGKDbContext dbContext,
            IMapper mapper) => (_dbContext, _mapper) = (dbContext, mapper);

        public async Task<EducationalSectorListVm> Handle(GetEducationalSectorListQuery request,
            CancellationToken cancellationToken)
        {
            IQueryable<EducationalSectorUser> query = _dbContext.EducationalSectorUsers;

            if (!string.IsNullOrEmpty(request.Search))
            {
                var search = request.Search.ToLower().Trim();
                query = query.Where(u =>
                    u.FirstName.ToLower().Contains(search) ||
                    u.LastName.ToLower().Contains(search)
                );
            }

            var educationalSector = query
                .ProjectTo<EducationalSectorDto>(_mapper.ConfigurationProvider);


            var educationalSectorPaged = await PagedList<EducationalSectorDto>.ToPagedList(educationalSector,
                request.PageNumber, request.PageSize);

            return new EducationalSectorListVm
            {
                Results = educationalSectorPaged
            };
        }
    }
}
