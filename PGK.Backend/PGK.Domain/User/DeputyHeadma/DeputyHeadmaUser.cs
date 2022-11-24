using PGK.Domain.User.Student;
using System.ComponentModel.DataAnnotations.Schema;

namespace PGK.Domain.User.DeputyHeadma
{
    [Table("DeputyHeadmaUsers")]
    public class DeputyHeadmaUser : StudentUser
    {
        public override UserRole Role => UserRole.DEPUTY_HEADMAN;

    }
}
