using System.ComponentModel.DataAnnotations;

namespace PGK.Application.App.Group.Commands.CreateGroup
{
    public class CreateGroupVm
    {
        [Required] public int Id { get; set; }
    }
}
