using MediatR;
using System.ComponentModel.DataAnnotations;

namespace PGK.Application.App.User.DepartmentHead.Commands.Registration
{
    public class RegistrationDepartmentHeadCommand : IRequest<RegistrationDepartmentHeadVm>
    {
        [Required, MaxLength(128)] public string FirstName { get; set; } = string.Empty;
        [Required, MaxLength(128)] public string LastName { get; set; } = string.Empty;
        [MaxLength(128)] public string? MiddleName { get; set; }
    }
}
