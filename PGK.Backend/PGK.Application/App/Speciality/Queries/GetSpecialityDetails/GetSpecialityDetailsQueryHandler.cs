using AutoMapper;
using MediatR;
using PGK.Application.App.Speciality.Queries.GetSpecialityList;
using PGK.Application.Common.Exceptions;
using PGK.Application.Interfaces;

namespace PGK.Application.App.Speciality.Queries.GetSpecialityDetails
{
    internal class GetSpecialityDetailsQueryHandler
        : IRequestHandler<GetSpecialityDetailsQuery, SpecialityDto>
    {
        private readonly IPGKDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetSpecialityDetailsQueryHandler(IPGKDbContext dbContext,
            IMapper mapper) => (_dbContext, _mapper) = (dbContext, mapper);

        public async Task<SpecialityDto> Handle(GetSpecialityDetailsQuery request,
            CancellationToken cancellationToken)
        {
            var speciality = await _dbContext.AdminUsers.FindAsync(request.Id);

            if (speciality == null)
            {
                throw new NotFoundException(nameof(Domain.Speciality.Speciality), request.Id);
            }

            return _mapper.Map<SpecialityDto>(speciality);
        }
    }
}
