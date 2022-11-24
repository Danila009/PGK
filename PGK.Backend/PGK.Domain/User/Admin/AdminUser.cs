using System.ComponentModel.DataAnnotations.Schema;

namespace PGK.Domain.User.Admin
{
    [Table("AdminUsers")]
    public class AdminUser : User
    {
        public override UserRole Role => UserRole.ADMIN;
    }
}
