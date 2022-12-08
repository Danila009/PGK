using System.ComponentModel.DataAnnotations;

namespace PGK.WebApi.Models.User
{
    public class UpdateUserModel
    {
        [Required, MaxLength(256)] public string FirstName { get; set; } = string.Empty;
        [Required, MaxLength(256)] public string LastName { get; set; } = string.Empty;
        [MaxLength(256)] public string? MiddleName { get; set; }
    }
}
