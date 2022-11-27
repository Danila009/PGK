using MediatR;

namespace PGK.Application.App.Raportichka.Queries.GetRaportichkaList
{
    public class GetRaportichkaListQuery : IRequest<RaportichkaListVm>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }

        public bool? Confirmation { get; set; }
        public DateTime? OnlyDate { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        public List<int> GroupIds { get; set; } = new List<int>();
        public List<int> SubjectIds { get; set; } = new List<int>();
        public List<int> ClassroomTeacherIds { get; set; } = new List<int>();
        public List<int> NumberLessons { get; set; } = new List<int>();
        public List<int> TeacherIds { get; set; } = new List<int>();
        public List<int> StudentIds { get; set; } = new List<int>();
    }
}
