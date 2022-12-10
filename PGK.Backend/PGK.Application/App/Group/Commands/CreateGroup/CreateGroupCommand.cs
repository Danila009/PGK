using MediatR;
using System.ComponentModel.DataAnnotations;

namespace PGK.Application.App.Group.Commands.CreateGroup
{
    public class CreateGroupCommand : IRequest<CreateGroupVm>
    {
        [Required] public int Course { get; set; }
        [Required] public int Number { get; set; }
        [Required] public int SpecialityId { get; set; }
        [Required] public int DepartmentId { get; set; }
        [Required] public int ClassroomTeacherId { get; set; }
    }
}
