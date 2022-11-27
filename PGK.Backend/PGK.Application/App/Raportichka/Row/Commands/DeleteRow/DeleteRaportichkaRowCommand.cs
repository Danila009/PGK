using MediatR;
using PGK.Domain.User;

namespace PGK.Application.App.Raportichka.Row.Commands.DeleteRow
{
    public class DeleteRaportichkaRowCommand : IRequest
    {
        public int Id { get; set; }

        public int UserId { get; set; }
        public UserRole UserRole { get; set; }
    }
}
