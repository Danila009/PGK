using MediatR;
using PGK.Application.App.User.Headman.Commands.Registration;
using System.ComponentModel.DataAnnotations;

namespace PGK.Application.App.User.Headman.Commands.DeputyRegistration
{
    public class RegistrationDeputyHeadmanCommand : IRequest<RegistrationHeadmanVm>
    {
        [Required] public int TeacherId { get; set; }
        [Required] public int StudentId { get; set; }
    }
}
