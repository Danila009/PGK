using AutoMapper;
using MediatR;
using PGK.Application.App.User.Director.Queries.GetDirectorList;
using PGK.Application.Common.Exceptions;
using PGK.Application.Interfaces;
using PGK.Domain.User.Director;

namespace PGK.Application.App.User.Director.Queries.GetDirectorDetails
{
    internal class GetDirectorDetailsQueryHandler
        : IRequestHandler<GetDirectorDetailsQuery, DirectorDto>
    {
        private readonly IPGKDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetDirectorDetailsQueryHandler(IPGKDbContext dbContext,
            IMapper mapper) => (_dbContext, _mapper) = (dbContext, mapper);

        public async Task<DirectorDto> Handle(GetDirectorDetailsQuery request,
            CancellationToken cancellationToken)
        {
            var director = await _dbContext.DirectorUsers.FindAsync(request.DirectorId);

            if (director == null)
            {
                throw new NotFoundException(nameof(DirectorUser), request.DirectorId);
            }

            return _mapper.Map<DirectorDto>(director);
        }
    }
}
