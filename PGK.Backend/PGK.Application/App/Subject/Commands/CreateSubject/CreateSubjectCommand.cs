using MediatR;
using System.ComponentModel.DataAnnotations;

namespace PGK.Application.App.Subject.Commands.CreateSubject
{
    public class CreateSubjectCommand : IRequest<CreateSubjectVm>
    {
        [Required] public string SubjectTitle { get; set; } = string.Empty;
    }
}
