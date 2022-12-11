using AutoMapper;
using PGK.Application.App.TechnicalSupport.Queries.GetMessageList;
using PGK.Application.App.User.Queries.GetUserLits;
using PGK.Application.Common.Extentions;
using PGK.Application.Common.Mappings;
using PGK.Domain.TechnicalSupport;
using System.ComponentModel.DataAnnotations;

namespace PGK.Application.App.TechnicalSupport.Queries.GetChatList
{
    public class ChatDto : IMapWith<Chat>
    {
        [Key] public int Id { get; set; }
        [Required] public DateTime DateCreation { get; set; } = DateTime.Now;
        
        public MessageDto? LastMessage { get; set; }
        public int MessageCount { get; set; }
        public virtual List<UserDto> Users { get; set; } = new List<UserDto>();

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Chat, ChatDto>()
                .ForMember("LastMessage", u => u.MapFrom(u => u.Messages.Last()))
                .ForMember("MessageCount", u => u.MapFrom(u => u.Messages.Count))
                .ForMember("Users", u => u.MapFrom(u => u.Messages.Select(u => u.User).RemoveDuplicates()));
        }
    }
}
