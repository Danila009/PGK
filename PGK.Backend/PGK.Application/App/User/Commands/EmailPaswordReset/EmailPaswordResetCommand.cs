using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace PGK.Application.App.User.Commands.EmailPaswordReset
{
    public class EmailPaswordResetCommand : IRequest<ContentResult>
    {
        public int UserId { get; set; }
        public string Token { get; set; } = string.Empty;
    }
}
