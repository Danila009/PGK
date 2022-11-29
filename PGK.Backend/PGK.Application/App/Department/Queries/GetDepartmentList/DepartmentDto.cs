using AutoMapper;
using PGK.Application.Common.Mappings;
using System.ComponentModel.DataAnnotations;

namespace PGK.Application.App.Department.Queries.GetDepartmentList
{
    public class DepartmentDto : IMapWith<Domain.Department.Department>
    {
        [Key] public int Id { get; set; }

        [Required] public string Name { get; set; } = string.Empty;

        //[Required] public DepartmentHeadUser DepartmentHead { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Domain.Department.Department, DepartmentDto>();
        }
    }
}
