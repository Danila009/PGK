using AutoMapper;
using MediatR;
using PGK.Application.Interfaces;
using PGK.Domain.Notification;
using AutoMapper.QueryableExtensions;
using PGK.Application.Common.Paged;

namespace PGK.Application.App.User.Queries.GetUserNotification
{
    internal class GetUserNotificationQueryHandler
        : IRequestHandler<GetUserNotificationQuery, NotificationListVm>
    {
        private readonly IMapper _mapper;
        private readonly IPGKDbContext _dbContext;

        public GetUserNotificationQueryHandler(IMapper mapper, IPGKDbContext dbContext) =>
            (_dbContext, _mapper) = (dbContext, mapper);

        public async Task<NotificationListVm> Handle(GetUserNotificationQuery request,
            CancellationToken cancellationToken)
        {
            IQueryable<Notification> query = _dbContext.Notifications
                .Where(u => u.Users.Any(u => u.Id == request.UserId));

            if (!string.IsNullOrEmpty(request.Search))
            {
                var search = request.Search.ToLower().Trim();

                query = query.Where(u => 
                    u.Title.ToLower().Contains(search) || u.Message.ToLower().Contains(search));
            }

            var notifications = query
                .OrderByDescending(u => u.Date)
                .ProjectTo<NotificationDto>(_mapper.ConfigurationProvider);

            var notificationsPaged = await PagedList<NotificationDto>.ToPagedList(notifications,
                request.PageNumber, request.PageSize);

            return new NotificationListVm
            {
                Results = notificationsPaged
            };
        }
    }
}
