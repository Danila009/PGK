using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using PGK.Application.Common.Paged;
using PGK.Application.Interfaces;

namespace PGK.Application.App.Speciality.Queries.GetSpecialityList
{
    internal class GetSpecialityListQueryHandler
        : IRequestHandler<GetSpecialityListQuery, SpecialityListVm>
    {
        private readonly IMapper _mapper;
        private readonly IPGKDbContext _dbContext;

        public GetSpecialityListQueryHandler(IMapper mapper, IPGKDbContext dbContext) =>
            (_dbContext, _mapper) = (dbContext, mapper);

        public async Task<SpecialityListVm> Handle(GetSpecialityListQuery request,
            CancellationToken cancellationToken)
        {
            IQueryable<Domain.Speciality.Speciality> query = _dbContext.Specialties;

            if (!string.IsNullOrEmpty(request.Search))
            {
                query = query.Where(u => u.Name.ToLower().Contains(request.Search.ToLower()) ||
                    u.NameAbbreviation.ToLower().Contains(request.Search.ToLower()));
            }

            var specialties = query
                .ProjectTo<SpecialityDto>(_mapper.ConfigurationProvider);

            var specialityPaged = await PagedList<SpecialityDto>.ToPagedList(specialties,
                request.PageNumber, request.PageSize);

            return new SpecialityListVm { Results = specialityPaged };
        }
    }
}
