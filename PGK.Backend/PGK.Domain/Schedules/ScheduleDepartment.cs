using System.ComponentModel.DataAnnotations;

namespace PGK.Domain.Schedules
{
    public class ScheduleDepartment
    {
        [Key] public int Id { get; set; }
        [Required] public string? Text { get; set; }
        public Department.Department? Department { get; set; }

        [Required] public Schedule Schedule { get; set; }

        public virtual List<ScheduleColumn> Columns { get; set; } = new List<ScheduleColumn>();
    }
}
