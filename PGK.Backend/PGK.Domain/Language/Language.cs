using System.ComponentModel.DataAnnotations;

namespace PGK.Domain.Language
{
    public class Language
    {
        [Key] public int Id { get; set; }
        [Required] public string Name { get; set; } = string.Empty;
        [Required] public string NameEn { get; set; } = string.Empty;
        [Required] public string Code { get; set; } = string.Empty;
    }
}
