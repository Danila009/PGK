using PGK.Application.Common.Paged;

namespace PGK.Application.App.User.Queries.GetUserNotification
{
    public class NotificationListVm : PagedResult<NotificationDto>
    {
        public override PagedList<NotificationDto> Results { get; set; }
    }
}
