using AutoMapper;
using MediatR;
using PGK.Application.App.User.Queries.GetUserSettings;
using PGK.Application.Common.Exceptions;
using PGK.Application.Interfaces;

namespace PGK.Application.App.User.Commands.UpdateNotificationsSettings
{
    internal class UpdateNotificationsSettingsCommandHandler
        : IRequestHandler<UpdateNotificationsSettingsCommand, UserSettingsDto>
    {
        private readonly IPGKDbContext _dbContext;
        private readonly IMapper _mapper;

        public UpdateNotificationsSettingsCommandHandler(IPGKDbContext dbContext, IMapper mapper) =>
            (_dbContext, _mapper) = (dbContext, mapper);

        public async Task<UserSettingsDto> Handle(UpdateNotificationsSettingsCommand request,
            CancellationToken cancellationToken)
        {
            var user = await _dbContext.Users.FindAsync(request.UserId);

            if (user == null)
            {
                throw new NotFoundException(nameof(Domain.User.User), request.UserId);
            }

            user.IncludedNotifications = request.IncludedNotifications;
            user.SoundNotifications = request.SoundNotifications;
            user.VibrationNotifications = request.VibrationNotifications;
            user.IncludedSchedulesNotifications = request.IncludedSchedulesNotifications;
            user.IncludedJournalNotifications = request.IncludedJournalNotifications;
            user.IncludedRaportichkaNotifications = request.IncludedRaportichkaNotifications;
            user.IncludedTechnicalSupportNotifications = request.IncludedTechnicalSupportNotifications;
            await _dbContext.SaveChangesAsync(cancellationToken);

            return _mapper.Map<UserSettingsDto>(user);
        }
    }
}
