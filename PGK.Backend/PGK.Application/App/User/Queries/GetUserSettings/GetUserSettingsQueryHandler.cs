using MediatR;
using PGK.Application.Interfaces;
using PGK.Application.Common.Exceptions;
using AutoMapper;

namespace PGK.Application.App.User.Queries.GetUserSettings
{
    public class GetUserSettingsQueryHandler
        : IRequestHandler<GetUserSettingsQuery, UserSettingsDto>
    {
        private readonly IPGKDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetUserSettingsQueryHandler(IPGKDbContext dbContext, IMapper mapper) =>
            (_dbContext, _mapper) = (dbContext, mapper);

        public async Task<UserSettingsDto> Handle(GetUserSettingsQuery request,
            CancellationToken cancellationToken)
        {
            var user = await _dbContext.Users.FindAsync(request.UserId);

            if(user == null)
            {
                throw new NotFoundException(nameof(Domain.User.User), request.UserId);
            }

            return _mapper.Map<UserSettingsDto>(user);
        }
    }
}
