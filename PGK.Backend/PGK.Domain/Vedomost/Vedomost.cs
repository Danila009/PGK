using System.ComponentModel.DataAnnotations;

namespace PGK.Domain.Vedomost
{
    public class Vedomost
    {
        [Key] public int Id { get; set; }
        [Required] public string Url { get; set; } = string.Empty;

        [Required] public DateTime Date { get; set; }

        [Required] public Group.Group Group { get; set; }
    }
}
