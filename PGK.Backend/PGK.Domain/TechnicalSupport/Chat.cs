using System.ComponentModel.DataAnnotations;

namespace PGK.Domain.TechnicalSupport
{
    public class Chat
    {
        [Key] public int Id { get; set; }
        [Required] public DateTime DateCreation { get; set; } = DateTime.Now;

        public virtual List<Message> Messages { get; set; } = new List<Message>();
    }
}
