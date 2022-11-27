using MediatR;
using System.ComponentModel.DataAnnotations;

namespace PGK.Application.App.Group.Commands.DeleteGroup
{
    public class DeleteGroupCommand : IRequest
    {
        [Required] public int GroupId { get; set; }
    }
}
