using System.ComponentModel.DataAnnotations;

namespace PGK.WebApi.Models.Group
{
    public class UpdateGroupModel
    {
        [Required] public int Number { get; set; }
        [Required] public int SpecialityId { get; set; }
        [Required] public int ClassroomTeacherId { get; set; }
        public int? HeadmanId { get; set; } = null;
        public int? DeputyHeadmaId { get; set; } = null;

        [Required] public int DepartmentId { get; set; }
    }
}
