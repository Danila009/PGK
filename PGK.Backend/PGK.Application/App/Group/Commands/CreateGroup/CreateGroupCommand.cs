using MediatR;
using System.ComponentModel.DataAnnotations;

namespace PGK.Application.App.Group.Commands.CreateGroup
{
    public class CreateGroupCommand : IRequest<CreateGroupVm>
    {
        [Required] public int Number { get; set; }
        [Required] public string Speciality { get; set; } = string.Empty;
        [Required] public string SpecialityAbbreviation { get; set; } = string.Empty;
        [Required] public int ClassroomTeacherId { get; set; }
    }
}
