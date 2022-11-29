using PGK.Domain.User.EducationalSector;
using System.ComponentModel.DataAnnotations;

namespace PGK.Domain.Schedules
{
    public class Schedule
    {
        [Key] public int Id { get; set; }
        [Required] public DateTime Date { get; set; } 
        [Required] public EducationalSectorUser EducationalSector { get; set; }

        public virtual List<ScheduleDepartment> ScheduleDepartments { get; set; } = new List<ScheduleDepartment>();
    }
}
