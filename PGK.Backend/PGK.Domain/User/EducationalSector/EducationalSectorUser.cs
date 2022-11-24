using System.ComponentModel.DataAnnotations.Schema;

namespace PGK.Domain.User.EducationalSector
{
    [Table("EducationalSectorUsers")]
    public class EducationalSectorUser : User
    {
        public override UserRole Role => UserRole.EDUCATIONAL_SECTOR;

        public virtual List<Schedules.Schedules> Schedules { get; set; } = new List<Schedules.Schedules>();
    }
}
