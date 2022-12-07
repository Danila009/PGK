using MediatR;
using PGK.Application.App.Group.Queries.GetGroupDetails;
using System.ComponentModel.DataAnnotations;

namespace PGK.Application.App.Group.Commands.UpdateGroup
{
    public class UpdateGroupCommand : IRequest<GroupDetails>
    {
        [Required] public int Id { get; set; }

        [Required] public int Number { get; set; }
        [Required] public int SpecialityId { get; set; }
        [Required] public int ClassroomTeacherId { get; set; }
        public int? HeadmanId { get; set; } = null;
        public int? DeputyHeadmaId { get; set; } = null;

        [Required] public int DepartmentId { get; set; }
    }
}
