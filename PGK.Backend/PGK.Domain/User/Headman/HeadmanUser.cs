using PGK.Domain.User.Student;
using System.ComponentModel.DataAnnotations.Schema;

namespace PGK.Domain.User.Headman
{
    [Table("HeadmanUsers")]
    public class HeadmanUser : StudentUser
    {
        public override string Role => UserRole.HEADMAN.ToString();

    }
}
