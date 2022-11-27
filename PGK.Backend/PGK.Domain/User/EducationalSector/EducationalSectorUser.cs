using System.ComponentModel.DataAnnotations.Schema;

namespace PGK.Domain.User.EducationalSector
{
    [Table("EducationalSectorUsers")]
    public class EducationalSectorUser : User
    {
        public override string Role => UserRole.EDUCATIONAL_SECTOR.ToString();

        public virtual List<Schedules.Schedules> Schedules { get; set; } = new List<Schedules.Schedules>();
    }
}
