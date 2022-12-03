using System.ComponentModel.DataAnnotations.Schema;

namespace PGK.Domain.User.EducationalSector
{
    [Table("EducationalSectorUsers")]
    public class EducationalSectorUser : User
    {
        public override string Role => UserRole.EDUCATIONAL_SECTOR.ToString();

    }
}
