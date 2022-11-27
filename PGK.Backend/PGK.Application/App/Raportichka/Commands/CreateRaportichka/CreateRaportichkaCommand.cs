using MediatR;
using PGK.Domain.User;
using System.ComponentModel.DataAnnotations;

namespace PGK.Application.App.Raportichka.Commands.CreateRaportichka
{
    public class CreateRaportichkaCommand : IRequest<CreateRaportichkaVm>
    {
        [Required] public UserRole Role { get; set; }
        [Required] public int UserId { get; set; }

        [Required] public int? GroupId { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;
    }
}
