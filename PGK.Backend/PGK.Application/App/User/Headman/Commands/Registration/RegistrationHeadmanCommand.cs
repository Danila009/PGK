using MediatR;
using System.ComponentModel.DataAnnotations;

namespace PGK.Application.App.User.Headman.Commands.Registration
{
    public class RegistrationHeadmanCommand : IRequest<RegistrationHeadmanVm>
    {
        [Required] public int TeacherId { get; set; }
        [Required] public int StudentId { get; set; }
    }
}
