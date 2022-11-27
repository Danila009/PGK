using MediatR;
using PGK.Domain.User;
using System.ComponentModel.DataAnnotations;

namespace PGK.Application.App.Raportichka.Commands.DeleteRaportichka
{
    public class DeleteRaportichkaCommand : IRequest
    {
        [Required] public int Id { get; set; }

        public int UserId { get; set; }
        public UserRole UserRole { get; set; }
    }
}
