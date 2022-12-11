using MediatR;
using PGK.Application.App.TechnicalSupport.Queries.GetMessageList;
using PGK.Domain.User;
using System.ComponentModel.DataAnnotations;

namespace PGK.Application.App.TechnicalSupport.Commands.SendMessage
{
    public class SendMessageCommand : IRequest<MessageDto>
    {
        public string? Text { get; set; }

        public int? ChatId { get; set; }
        [Required] public int UserId { get; set; }
        [Required] public UserRole Role { get; set; }
    }
}
