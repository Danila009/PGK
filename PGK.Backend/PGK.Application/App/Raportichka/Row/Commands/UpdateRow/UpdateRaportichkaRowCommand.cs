using MediatR;
using System.ComponentModel.DataAnnotations;

namespace PGK.Application.App.Raportichka.Row.Commands.UpdateRow
{
    public class UpdateRaportichkaRowCommand : IRequest
    {
        [Required] public int RowId { get; set; }

        public int NumberLesson { get; set; }
        public bool Confirmation { get; set; }
        public int Hours { get; set; }
        public int SubjectId { get; set; }
        public int TeacherId { get; set; }
        public int StudentId { get; set; }
        public int RaportichkaId { get; set; }
    }
}
