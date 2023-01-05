using MediatR;
using PGK.Domain.User;

namespace PGK.Application.App.User.Commands.UpdateCabinet
{
    public class UpdateUserCabinetCommand : IRequest
    {
        public string? Cabinet { get; set; }

        public int UserId { get; set; }
        public UserRole UserRole { get; set; }
    }
}
