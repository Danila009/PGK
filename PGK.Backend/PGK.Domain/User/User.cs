using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PGK.Domain.User
{
    [Table("Users")]
    public class User
    {
        [Key] public int Id { get; set; }
        [Required, MaxLength(256)] public string FirstName { get; set; } = string.Empty;
        [Required, MaxLength(256)] public string LastName { get; set; } = string.Empty;
        [MaxLength(256)] public string? MiddleName { get; set; }

        [MaxLength(256)] public string? Email { get; set; } = string.Empty;
        [Required] public bool EmailVerification { get; set; } = false;

        public string? SendEmailToken { get; set; } = null;

        [Required, MaxLength(256)] public string Password { get; set; } = string.Empty;

        [Required] public bool DrarkMode { get; set; } = false;
        [Required] public SecondaryBackground SecondaryBackground { get; set; } = SecondaryBackground.BLUE;

        public string? PhotoUrl { get; set; } = null;
        public string? RefreshToken { get; set; } = null;

        public virtual string Role => UserRole.STUDENT.ToString();
    }
}
