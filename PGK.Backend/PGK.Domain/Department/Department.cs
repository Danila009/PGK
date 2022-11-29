using PGK.Domain.Schedules;
using PGK.Domain.User.DepartmentHead;
using PGK.Domain.User.Student;
using System.ComponentModel.DataAnnotations;

namespace PGK.Domain.Department
{
    public class Department
    {
        [Key] public int Id { get; set; }

        [Required] public string Name { get; set; } = string.Empty;

        [Required] public DepartmentHeadUser DepartmentHead { get; set; }

        public virtual List<ScheduleDepartment> ScheduleDepartments { get; set; } = new List<ScheduleDepartment>();
        public virtual List<Group.Group> Groups { get; set; } = new List<Group.Group>();
        public virtual List<StudentUser> StudentUsers { get; set; } = new List<StudentUser>();
        public virtual List<Speciality.Speciality> Specializations { get; set; } = new List<Speciality.Speciality>();
    }
}
