using System.ComponentModel.DataAnnotations;

namespace PGK.Domain.Notification
{
    public class Notification
    {
        [Key] public int Id { get; set; }
        [Required] public string Title { get; set; } = string.Empty;
        [Required] public string Message { get; set; } = string.Empty;
        [Required] public DateTime Date { get; set; } = DateTime.Now;

        [Required] public List<User.User> Users { get; set; } = new List<User.User>();
    }
}
