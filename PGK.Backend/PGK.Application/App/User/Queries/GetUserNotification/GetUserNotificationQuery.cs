using MediatR;

namespace PGK.Application.App.User.Queries.GetUserNotification
{
    public class GetUserNotificationQuery : IRequest<NotificationListVm>
    {
        public string? Search { get; set; }

        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 20;
    
        public int UserId { get; set; }
    }
}
