using AutoMapper;
using MediatR;
using PGK.Application.App.User.Queries.GetUserSettings;
using PGK.Application.Common.Exceptions;
using PGK.Application.Interfaces;

namespace PGK.Application.App.User.Commands.UpdateThemeFontSize
{
    public class UpdateThemeFontSizeCommandHandler
        : IRequestHandler<UpdateThemeFontSizeCommand, UserSettingsDto>
    {
        private readonly IPGKDbContext _dbContext;
        private readonly IMapper _mapper;

        public UpdateThemeFontSizeCommandHandler(IPGKDbContext dbContext, IMapper mapper) =>
            (_dbContext, _mapper) = (dbContext, mapper);

        public async Task<UserSettingsDto> Handle(UpdateThemeFontSizeCommand request,
            CancellationToken cancellationToken)
        {
            var user = await _dbContext.Users.FindAsync(request.UserId);

            if (user == null)
            {
                throw new NotFoundException(nameof(Domain.User.User), request.UserId);
            }

            user.ThemeFontSize = request.ThemeFontSize;
            await _dbContext.SaveChangesAsync(cancellationToken);

            return _mapper.Map<UserSettingsDto>(user);
        }
    }
}
