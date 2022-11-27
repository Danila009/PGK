using System.ComponentModel.DataAnnotations;

namespace PGK.Application.App.Subject.Commands.CreateSubject
{
    public class CreateSubjectVm
    {
        [Required] public int Id { get; set; }
    }
}
