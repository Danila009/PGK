using System.ComponentModel.DataAnnotations;

namespace PGK.Domain.Schedules
{
    public class ScheduleColumn
    {
        [Key] public int Id { get; set; }
        public string? Text { get; set; }
        [Required] public string Time { get; set; } = string.Empty;
        public Group.Group? Group { get; set; }

        [Required] public ScheduleDepartment ScheduleDepartment { get; set; }
        public virtual List<ScheduleRow> Rows { get; set; } = new List<ScheduleRow>();
    }
}
