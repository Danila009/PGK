using AutoMapper;
using MediatR;
using PGK.Application.Common.Exceptions;
using PGK.Application.Interfaces;

namespace PGK.Application.App.User.Queries.GetUserById
{
    internal class GetUserByIdQueryHandler
        : IRequestHandler<GetUserByIdQuery, UserDetailsDto>
    {
        private readonly IPGKDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetUserByIdQueryHandler(IPGKDbContext dbContext, IMapper mapper) =>
            (_dbContext, _mapper) = (dbContext, mapper);

        public async Task<UserDetailsDto> Handle(GetUserByIdQuery request,
            CancellationToken cancellationToken)
        {
            var user = await _dbContext.Users.FindAsync(request.UserId);

            if (user == null)
            {
                throw new NotFoundException(nameof(Domain.User.User), request.UserId);
            }

            return _mapper.Map<UserDetailsDto>(user);
        }
    }
}
