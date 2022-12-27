using AutoMapper;
using MediatR;
using PGK.Application.App.User.Queries.GetUserSettings;
using PGK.Application.Common.Exceptions;
using PGK.Application.Interfaces;

namespace PGK.Application.App.User.Commands.UpdateThemeCorners
{
    internal class UpdateThemeCornersCommandHandler
        : IRequestHandler<UpdateThemeCornersCommand, UserSettingsDto>
    {
        private readonly IPGKDbContext _dbContext;
        private readonly IMapper _mapper;

        public UpdateThemeCornersCommandHandler(IPGKDbContext dbContext, IMapper mapper) =>
            (_dbContext, _mapper) = (dbContext, mapper);

        public async Task<UserSettingsDto> Handle(UpdateThemeCornersCommand request,
            CancellationToken cancellationToken)
        {
            var user = await _dbContext.Users.FindAsync(request.UserId);

            if (user == null)
            {
                throw new NotFoundException(nameof(Domain.User.User), request.UserId);
            }

            user.ThemeCorners = request.ThemeCorners;
            await _dbContext.SaveChangesAsync(cancellationToken);

            return _mapper.Map<UserSettingsDto>(user);
        }
    }
}
