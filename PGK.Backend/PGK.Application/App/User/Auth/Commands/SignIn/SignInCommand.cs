using MediatR;
using System.ComponentModel.DataAnnotations;

namespace PGK.Application.App.User.Auth.Commands.SignIn
{
    public class SignInCommand : IRequest<SignInVm>
    {
        [Required, MaxLength(256)] public string FirstName { get; set; } = string.Empty;
        [Required, MaxLength(256)] public string LastName { get; set; } = string.Empty;
        [MaxLength(256)] public string? MiddleName { get; set; }

        [Required, MaxLength(256)] public string Password { get; set; } = string.Empty;
    }
}
