using MediatR;
using PGK.Application.App.Department.Queries.GetDepartmentList;
using System.ComponentModel.DataAnnotations;

namespace PGK.Application.App.Department.Commands.CreateDepartment
{
    public class CreateDepartmentCommand : IRequest<DepartmentDto>
    {
        [Required] public string Name { get; set; } = string.Empty;
        [Required] public int DepartmentHeadId { get; set; }
    }
}
