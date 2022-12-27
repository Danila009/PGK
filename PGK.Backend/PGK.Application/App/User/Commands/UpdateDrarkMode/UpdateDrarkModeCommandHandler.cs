using AutoMapper;
using MediatR;
using PGK.Application.App.User.Queries.GetUserSettings;
using PGK.Application.Common.Exceptions;
using PGK.Application.Interfaces;

namespace PGK.Application.App.User.Commands.UpdateDrarkMode
{
    internal class UpdateDrarkModeCommandHandler
        : IRequestHandler<UpdateDrarkModeCommand, UserSettingsDto>
    {
        private readonly IPGKDbContext _dbContext;
        private readonly IMapper _mapper;

        public UpdateDrarkModeCommandHandler(IPGKDbContext dbContext, IMapper mapper) =>
            (_dbContext, _mapper) = (dbContext, mapper);

        public async Task<UserSettingsDto> Handle(UpdateDrarkModeCommand request,
            CancellationToken cancellationToken)
        {
            var user = await _dbContext.Users.FindAsync(request.UserId);

            if(user == null)
            {
                throw new NotFoundException(nameof(Domain.User.User), request.UserId);
            }

            user.DrarkMode = !user.DrarkMode;

            await _dbContext.SaveChangesAsync(cancellationToken);

            return _mapper.Map<UserSettingsDto>(user);
        }
    }
}
