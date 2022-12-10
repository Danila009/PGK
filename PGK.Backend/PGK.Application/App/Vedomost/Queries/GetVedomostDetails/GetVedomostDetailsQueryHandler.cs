using AutoMapper;
using MediatR;
using PGK.Application.App.Vedomost.Queries.GetVedomostList;
using PGK.Application.Interfaces;
using PGK.Application.Common.Exceptions;

namespace PGK.Application.App.Vedomost.Queries.GetVedomostDetails
{
    internal class GetVedomostDetailsQueryHandler
        : IRequestHandler<GetVedomostDetailsQuery, VedomostDto>
    {
        private readonly IMapper _mapper;
        private readonly IPGKDbContext _dbContext;

        public GetVedomostDetailsQueryHandler(IMapper mapper, IPGKDbContext dbContext) =>
            (_dbContext, _mapper) = (dbContext, mapper);

        public async Task<VedomostDto> Handle(GetVedomostDetailsQuery request,
            CancellationToken cancellationToken)
        {
            var vedomost = await _dbContext.Vedomost.FindAsync(request.Id);

            if(vedomost == null)
            {
                throw new NotFoundException(nameof(Domain.Vedomost.Vedomost), request.Id);
            }

            return _mapper.Map<VedomostDto>(vedomost);
        }
    }
}
