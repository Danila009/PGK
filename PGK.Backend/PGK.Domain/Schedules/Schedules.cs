using PGK.Domain.User.EducationalSector;
using System.ComponentModel.DataAnnotations;

namespace PGK.Domain.Schedules
{
    public class Schedules
    {
        [Key] public string Id { get; set; }
        [Required] public DateTime Date { get; set; }
        [Required] public string UrlSpreadsheets { get; set; }

        [Required] public EducationalSectorUser EducationalSector { get; set; }
    }
}
