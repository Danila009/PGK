using MediatR;
using PGK.Domain.User;

namespace PGK.Application.App.User.Commands.UpdateInformation
{
    public class UpdateInformationCommand : IRequest
    {
        public string? Information { get; set; }

        public int UserId { get; set; }
        public UserRole UserRole { get; set; }
    }
}
