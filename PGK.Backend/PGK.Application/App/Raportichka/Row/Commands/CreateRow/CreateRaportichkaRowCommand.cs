using MediatR;
using System.ComponentModel.DataAnnotations;

namespace PGK.Application.App.Raportichka.Row.Commands.CreateRow
{
    public class CreateRaportichkaRowCommand : IRequest<CreateRaportichkaRowVm>
    {
        [Required] public int RaportichkaId { get; set; }
        [Required] public int NumberLesson { get; set; }
        [Required] public int Hours { get; set; }
        [Required] public int SubjectId { get; set; }
        [Required] public int StudentId { get; set; }
        [Required] public int TeacherId { get; set; }
    }
}
