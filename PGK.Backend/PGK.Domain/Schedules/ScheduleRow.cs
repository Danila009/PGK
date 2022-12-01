using PGK.Domain.User.Teacher;
using System.ComponentModel.DataAnnotations;

namespace PGK.Domain.Schedules
{
    public class ScheduleRow
    {
        [Key] public int Id { get; set; }
        public string? Text { get; set; }
        
        public TeacherUser? Teacher { get; set; }

        [Required] public ScheduleColumn Column { get; set; }
    }
}
