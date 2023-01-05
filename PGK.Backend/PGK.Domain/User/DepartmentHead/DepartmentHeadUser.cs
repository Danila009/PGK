using System.ComponentModel.DataAnnotations.Schema;

namespace PGK.Domain.User.DepartmentHead
{
    [Table("DepartmentHeadUsers")]
    public class DepartmentHeadUser : User
    {
        public override string Role => UserRole.DEPARTMENT_HEAD.ToString();

        public string? Cabinet { get; set; }
        public string? Information { get; set; }
    }
}
