using AutoMapper;
using MediatR;
using PGK.Application.App.User.Queries.GetUserLits;
using PGK.Application.Common.Exceptions;
using PGK.Application.Interfaces;

namespace PGK.Application.App.User.Queries.GetUserById
{
    internal class GetUserByIdQueryHandler
        : IRequestHandler<GetUserByIdQuery, UserDto>
    {
        private readonly IPGKDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetUserByIdQueryHandler(IPGKDbContext dbContext, IMapper mapper) =>
            (_dbContext, _mapper) = (dbContext, mapper);

        public async Task<UserDto> Handle(GetUserByIdQuery request,
            CancellationToken cancellationToken)
        {
            var user = await _dbContext.Users.FindAsync(request.UserId);

            if (user == null)
            {
                throw new NotFoundException(nameof(Domain.User.User), request.UserId);
            }

            return _mapper.Map<UserDto>(user);
        }
    }
}
