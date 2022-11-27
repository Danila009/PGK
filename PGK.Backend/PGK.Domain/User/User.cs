using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PGK.Domain.User
{
    [Table("Users")]
    public class User
    {
        [Key] public int Id { get; set; }
        [Required, MaxLength(128)] public string FirstName { get; set; } = string.Empty;
        [Required, MaxLength(128)] public string LastName { get; set; } = string.Empty;
        [MaxLength(128)] public string? MiddleName { get; set; }

        [Required, MaxLength(256)] public string Password { get; set; } = string.Empty;

        public string? RefreshToken { get; set; } = null;

        public virtual string Role => UserRole.STUDENT.ToString();
    }
}
