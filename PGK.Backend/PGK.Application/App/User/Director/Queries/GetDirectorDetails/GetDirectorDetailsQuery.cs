using MediatR;
using PGK.Application.App.User.Director.Queries.GetDirectorList;

namespace PGK.Application.App.User.Director.Queries.GetDirectorDetails
{
    public class GetDirectorDetailsQuery : IRequest<DirectorDto>
    {
        public int DirectorId { get; set; }
    }
}
