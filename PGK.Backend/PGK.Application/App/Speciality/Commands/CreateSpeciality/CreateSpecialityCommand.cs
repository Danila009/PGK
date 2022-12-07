using MediatR;
using PGK.Application.App.Speciality.Queries.GetSpecialityList;
using System.ComponentModel.DataAnnotations;

namespace PGK.Application.App.Speciality.Commands.CreateSpeciality
{
    public class CreateSpecialityCommand : IRequest<SpecialityDto>
    {
        [Required] public string Number { get; set; } = string.Empty;
        [Required] public string Name { get; set; } = string.Empty;
        [Required] public string NameAbbreviation { get; set; } = string.Empty;
        [Required] public string Qualification { get; set; } = string.Empty;

        [Required] public int DepartmentId { get; set; }
    }
}
