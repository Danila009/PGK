using System.ComponentModel.DataAnnotations;

namespace PGK.WebApi.Models.Subject
{
    public class UpdateSubjectModel
    {
        [Required] public string SubjectTitle { get; set; } = string.Empty;
    }
}
