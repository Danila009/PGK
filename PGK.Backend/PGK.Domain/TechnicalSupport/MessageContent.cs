using PGK.Domain.TechnicalSupport.Enums;
using System.ComponentModel.DataAnnotations;

namespace PGK.Domain.TechnicalSupport
{
    public class MessageContent
    {
        [Key] public int Id { get; set; }

        [Required] public string Url { get; set; } = string.Empty;
        [Required] public MessageContentType Type { get; set; }

        [Required] public Message Message { get; set; }
    }
}
