using MediatR;

namespace PGK.Application.App.Speciality.Commands.DeleteSpeciality
{
    public class DeleteSpecialityCommand : IRequest
    {
        public int Id { get; set; }
    }
}
