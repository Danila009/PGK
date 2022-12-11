using AutoMapper;
using PGK.Application.App.TechnicalSupport.Queries.GetMessageContentList;
using PGK.Application.App.User.Queries.GetUserLits;
using PGK.Application.Common.Mappings;
using PGK.Domain.TechnicalSupport;
using System.ComponentModel.DataAnnotations;

namespace PGK.Application.App.TechnicalSupport.Queries.GetMessageList
{
    public class MessageDto : IMapWith<Message>
    {
        [Key] public int Id { get; set; }

        public string? Text { get; set; }

        [Required] public bool UserVisible { get; set; } = false;
        [Required] public bool Pin { get; set; } = false;
        [Required] public bool Edited { get; set; } = false;
        public DateTime? EditedDate { get; set; }
        [Required] public DateTime Date { get; set; } = DateTime.Now;
        [Required] public UserDto User { get; set; }

        public virtual List<MessageContentDto> Contents { get; set; } = new List<MessageContentDto>();

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Message, MessageDto>();
        }
    }
}
