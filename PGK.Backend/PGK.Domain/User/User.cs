using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PGK.Domain.User
{
    [Table("Users")]
    public class User
    {
        [Key] public int Id { get; set; }
        [Required, MaxLength(64)] public string FirstName { get; set; } = string.Empty;
        [Required, MaxLength(64)] public string LastName { get; set; } = string.Empty;
        [MaxLength(64)] public string? MiddleName { get; set; }

        [Required, MaxLength(64)] public string Password { get; set; } = string.Empty;

        public string RefreshToken { get; set; } = string.Empty;

        public virtual UserRole Role => UserRole.STUDENT;
    }
}
