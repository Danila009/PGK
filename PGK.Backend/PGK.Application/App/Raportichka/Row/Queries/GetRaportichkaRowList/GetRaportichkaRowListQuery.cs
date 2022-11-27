using MediatR;
using System.ComponentModel.DataAnnotations;

namespace PGK.Application.App.Raportichka.Row.Queries.GetRaportichkaRowList
{
    public class GetRaportichkaRowListQuery : IRequest<RaportichkaRowListVm>
    {
        [Required] public int RaportichkaId { get; set; }
        [Required] public int PageNumber { get; set; }
        [Required] public int PageSize { get; set; }

        public bool? Confirmation { get; set; }

        public List<int> SubjectIds { get; set; } = new List<int>();
        public List<int> NumberLessons { get; set; } = new List<int>();
        public List<int> TeacherIds { get; set; } = new List<int>();
        public List<int> StudentIds { get; set; } = new List<int>();
    }
}
