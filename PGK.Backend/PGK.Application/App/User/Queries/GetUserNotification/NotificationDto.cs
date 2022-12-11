using AutoMapper;
using PGK.Application.Common.Mappings;
using PGK.Domain.Notification;
using System.ComponentModel.DataAnnotations;

namespace PGK.Application.App.User.Queries.GetUserNotification
{
    public class NotificationDto : IMapWith<Notification>
    {
        [Key] public int Id { get; set; }
        [Required] public string Title { get; set; } = string.Empty;
        [Required] public string Message { get; set; } = string.Empty;

        [Required] public DateTime Date { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Notification, NotificationDto>();
        }
    }
}
