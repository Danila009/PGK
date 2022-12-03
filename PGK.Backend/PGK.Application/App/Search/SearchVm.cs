using PGK.Application.App.Department.Queries.GetDepartmentList;
using PGK.Application.App.Group.Queries.GetGroupDetails;
using PGK.Application.App.Speciality.Queries.GetSpecialityList;
using PGK.Application.App.Subject.Queries.GetSubjectList;
using PGK.Application.App.User.Admin.Queries.GetAdminList;
using PGK.Application.App.User.DepartmentHead.Queries.GetDepartmentHeadList;
using PGK.Application.App.User.EducationalSector.Queries.GetEducationalSectorList;
using PGK.Application.App.User.Headman.Queries.GetDeputyHeadmanList;
using PGK.Application.App.User.Headman.Queries.GetHeadmanList;
using PGK.Application.App.User.Student.Queries.GetStudentUserList;
using PGK.Application.App.User.Teacher.Queries.GetTeacherUserDetails;
using PGK.Application.Common.Paged;

namespace PGK.Application.App.Search
{
    public class SearchVm
    {
        public PagedList<StudentDto>? Students { get; set; }
        public PagedList<HeadmanDto>? Headmens { get; set; }
        public PagedList<DepartmentHeadDto>? DepartmentHead { get; set; }
        public PagedList<TeacherUserDetails>? Teachers { get; set; }
        public PagedList<EducationalSectorDto>? EducationalSectors { get; set; }
        public PagedList<DeputyHeadmanDto>? DeputyHeadman { get; set; }
        public PagedList<AdminDto>? Admins { get; set; }
        public PagedList<DepartmentDto>? Departments { get; set; }
        public PagedList<GroupDetails>? Groups { get; set; }
        public PagedList<SpecialityDto>? Specialities { get; set; }
        public PagedList<SubjectDto>? Subjects { get; set; }
    }
}
