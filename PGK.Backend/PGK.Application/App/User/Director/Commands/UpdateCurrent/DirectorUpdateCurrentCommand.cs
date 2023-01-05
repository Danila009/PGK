using MediatR;

namespace PGK.Application.App.User.Director.Commands.UpdateCurrent
{
    public class DirectorUpdateCurrentCommand : IRequest
    {
        public int DirectorId { get; set; }
    }
}
