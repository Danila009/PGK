using AutoMapper;
using PGK.Application.Common.Mappings;
using PGK.Domain.TechnicalSupport;
using PGK.Domain.TechnicalSupport.Enums;
using System.ComponentModel.DataAnnotations;

namespace PGK.Application.App.TechnicalSupport.Queries.GetMessageContentList
{
    public class MessageContentDto : IMapWith<MessageContent>
    {
        [Key] public int Id { get; set; }

        [Required] public string Url { get; set; } = string.Empty;
        [Required] public MessageContentType Type { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<MessageContent, MessageContentDto>();
        }
    }
}
