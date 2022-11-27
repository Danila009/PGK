using MediatR;
using System.ComponentModel.DataAnnotations;

namespace PGK.Application.App.Raportichka.Commands.CreateRaportichka
{
    public class CreateRaportichkaCommand : IRequest<CreateRaportichkaVm>
    {
        [Required] public int GroupId { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;
    }
}
