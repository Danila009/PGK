using System.ComponentModel.DataAnnotations;

namespace PGK.Domain.TechnicalSupport
{
    public class Message
    {
        [Key] public int Id { get; set; }

        public string? Text { get; set; }

        [Required] public bool UserVisible { get; set; } = false;
        [Required] public bool Pin { get; set; } = false;
        [Required] public bool Edited { get; set; } = false;
        public DateTime? EditedDate { get; set; }
        [Required] public DateTime Date { get; set; } = DateTime.Now;
        [Required] public User.User User { get; set; }
        [Required] public Chat Chat { get; set; }

        public virtual List<MessageContent> Contents { get; set; } = new List<MessageContent>();
    }
}
