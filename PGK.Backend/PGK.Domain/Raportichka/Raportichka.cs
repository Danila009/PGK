using System.ComponentModel.DataAnnotations;

namespace PGK.Domain.Raportichka
{
    public class Raportichka
    {
        [Key] public int Id { get; set; }
        [Required] public Group.Group Group { get; set; }
        [Required] public DateTime Date { get; set; }

        [Required] public virtual List<RaportichkaRow> Rows { get; set; } = new List<RaportichkaRow>();
    }
}
