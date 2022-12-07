using MediatR;
using PGK.Application.App.Speciality.Queries.GetSpecialityList;

namespace PGK.Application.App.Speciality.Commands.UpdateSpeciality
{
    public class UpdateSpecialityCommand : IRequest<SpecialityDto>
    {
        public int Id { get; set; }

        public string Number { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string NameAbbreviation { get; set; } = string.Empty;
        public string Qualification { get; set; } = string.Empty;

        public int DepartmentId { get; set; }
    }
}
