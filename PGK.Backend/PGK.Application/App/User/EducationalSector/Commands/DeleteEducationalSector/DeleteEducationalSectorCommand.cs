using MediatR;

namespace PGK.Application.App.User.EducationalSector.Commands.DeleteEducationalSector
{
    public class DeleteEducationalSectorCommand : IRequest
    {
        public int Id { get; set; }
    }
}
