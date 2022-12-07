using AutoMapper;
using MediatR;
using PGK.Application.App.User.EducationalSector.Queries.GetEducationalSectorList;
using PGK.Application.Common.Exceptions;
using PGK.Application.Interfaces;
using PGK.Domain.User.EducationalSector;

namespace PGK.Application.App.User.EducationalSector.Queries.GetEducationalSectorDetails
{
    internal class GetEducationalSectorDetailsQueryHandler
        : IRequestHandler<GetEducationalSectorDetailsQuery, EducationalSectorDto>
    {
        private readonly IPGKDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetEducationalSectorDetailsQueryHandler(IPGKDbContext dbContext,
            IMapper mapper) => (_dbContext, _mapper) = (dbContext, mapper);

        public async Task<EducationalSectorDto> Handle(GetEducationalSectorDetailsQuery request,
            CancellationToken cancellationToken)
        {
            var educationalSector = await _dbContext.EducationalSectorUsers.FindAsync(request.Id);

            if (educationalSector == null)
            {
                throw new NotFoundException(nameof(EducationalSectorUser), request.Id);
            }

            return _mapper.Map<EducationalSectorDto>(educationalSector);
        }
    }
}
