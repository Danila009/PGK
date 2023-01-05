using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PGK.Domain.User.Director
{
    [Table("DirectorUsers")]
    public class DirectorUser : User
    {
        [Required] public bool Current { get; set; } = true;

        public string? Cabinet { get; set; }
        public string? Information { get; set; }

        public override string Role => UserRole.DIRECTOR.ToString();
    }
}
