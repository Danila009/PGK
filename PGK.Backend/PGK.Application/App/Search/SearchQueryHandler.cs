using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using PGK.Application.App.Department.Queries.GetDepartmentList;
using PGK.Application.App.Group.Queries.GetGroupDetails;
using PGK.Application.App.Search.Enums;
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
using PGK.Application.Interfaces;

namespace PGK.Application.App.Search
{
    internal class SearchQueryHandler : IRequestHandler<SearchQuery, SearchVm>
    {
        private readonly IPGKDbContext _dbContext;
        public readonly IMapper _mapper;

        public SearchQueryHandler(IPGKDbContext dbContext, IMapper mapper) =>
            (_dbContext, _mapper) = (dbContext, mapper);

        public async Task<SearchVm> Handle(SearchQuery request,
            CancellationToken cancellationToken)
        {
            var vm = new SearchVm();

            var search = request.Search.ToLower().Trim();

            if (request.Type == SearchType.ALL || request.Type == SearchType.STUDENT)
            {
                var students = _dbContext.StudentsUsers
                    .Where(u =>
                        u.FirstName.ToLower().Contains(search) ||
                        u.LastName.ToLower().Contains(search) ||
                        (u.MiddleName ?? "").ToLower().Contains(search))
                    .ProjectTo<StudentDto>(_mapper.ConfigurationProvider);

                var studentsPaged = await PagedList<StudentDto>.ToPagedList(students, request.PageNumber,
                    request.PageSize);

                vm.Students = studentsPaged;
            }

            if (request.Type == SearchType.ALL || request.Type == SearchType.HEADMAN)
            {
                var headman = _dbContext.HeadmanUsers
                    .Where(u =>
                        u.FirstName.ToLower().Contains(search) ||
                        u.LastName.ToLower().Contains(search) ||
                        (u.MiddleName ?? "").ToLower().Contains(search))
                    .ProjectTo<HeadmanDto>(_mapper.ConfigurationProvider);

                var headmanPaged = await PagedList<HeadmanDto>.ToPagedList(headman, request.PageNumber,
                    request.PageSize);

                vm.Headmens = headmanPaged;
            }

            if (request.Type == SearchType.ALL || request.Type == SearchType.DEPUTY_HEADMAN)
            {
                var deputyHeadman = _dbContext.DeputyHeadmaUsers
                    .Where(u =>
                        u.FirstName.ToLower().Contains(search) ||
                        u.LastName.ToLower().Contains(search) ||
                        (u.MiddleName ?? "").ToLower().Contains(search))
                    .ProjectTo<DeputyHeadmanDto>(_mapper.ConfigurationProvider);

                var deputyHeadmanPaged = await PagedList<DeputyHeadmanDto>.ToPagedList(deputyHeadman,
                    request.PageNumber, request.PageSize);

                vm.DeputyHeadman = deputyHeadmanPaged;
            }

            if (request.Type == SearchType.ALL || request.Type == SearchType.TEACHER)
            {
                var teachers = _dbContext.TeacherUsers
                    .Where(u =>
                        u.FirstName.ToLower().Contains(search) ||
                        u.LastName.ToLower().Contains(search) ||
                        (u.MiddleName ?? "").ToLower().Contains(search))
                    .ProjectTo<TeacherUserDetails>(_mapper.ConfigurationProvider);

                var teachersPaged = await PagedList<TeacherUserDetails>.ToPagedList(teachers,
                    request.PageNumber, request.PageSize);

                vm.Teachers = teachersPaged;
            }

            if(request.Type == SearchType.ALL || request.Type == SearchType.EDUCATIONAL_SECTOR)
            {
                var educationalSector = _dbContext.EducationalSectorUsers
                    .Where(u =>
                        u.FirstName.ToLower().Contains(search) ||
                        u.LastName.ToLower().Contains(search) ||
                        (u.MiddleName ?? "").ToLower().Contains(search))
                    .ProjectTo<EducationalSectorDto>(_mapper.ConfigurationProvider);

                var educationalSectorPaged = await PagedList<EducationalSectorDto>.ToPagedList(
                    educationalSector, request.PageNumber, request.PageSize);

                vm.EducationalSectors = educationalSectorPaged;
            }

            if(request.Type == SearchType.ALL || request.Type == SearchType.DEPARTMENT_HEAD)
            {
                var departmentHead = _dbContext.DepartmentHeadUsers
                    .Where(u =>
                        u.FirstName.ToLower().Contains(search) ||
                        u.LastName.ToLower().Contains(search) ||
                        (u.MiddleName ?? "").ToLower().Contains(search))
                    .ProjectTo<DepartmentHeadDto>(_mapper.ConfigurationProvider);

                var departmentHeadPaged = await PagedList<DepartmentHeadDto>.ToPagedList(
                    departmentHead, request.PageNumber, request.PageSize);

                vm.DepartmentHead = departmentHeadPaged;
            }

            if(request.Type == SearchType.ALL || request.Type == SearchType.ADMIN)
            {
                var admins = _dbContext.AdminUsers
                    .Where(u =>
                        u.FirstName.ToLower().Contains(search) ||
                        u.LastName.ToLower().Contains(search) ||
                        (u.MiddleName ?? "").ToLower().Contains(search))
                    .ProjectTo<AdminDto>(_mapper.ConfigurationProvider);

                var adminsPaged = await PagedList<AdminDto>.ToPagedList(admins, request.PageNumber,
                    request.PageSize);

                vm.Admins = adminsPaged;
            }

            if(request.Type == SearchType.ALL || request.Type == SearchType.DEPARTMENT)
            {
                var department = _dbContext.Departments
                    .Where(u => u.Name.ToLower().Contains(search))
                    .ProjectTo<DepartmentDto>(_mapper.ConfigurationProvider);

                var departmentPaged = await PagedList<DepartmentDto>.ToPagedList(department,
                    request.PageNumber, request.PageSize);

                vm.Departments = departmentPaged;
            }

            if(request.Type == SearchType.ALL || request.Type == SearchType.GROUP)
            {
                var groups = _dbContext.Groups
                    .Where(u => u.Number.ToString().Contains(search))
                    .ProjectTo<GroupDetails>(_mapper.ConfigurationProvider);

                var groupsPaged = await PagedList<GroupDetails>.ToPagedList(groups,
                    request.PageNumber, request.PageSize);

                vm.Groups = groupsPaged;
            }

            if (request.Type == SearchType.ALL || request.Type == SearchType.SPECIALITY)
            {
                var specialties = _dbContext.Specialties
                    .Where(u => u.Qualification.ToLower().Contains(search) ||
                        u.Number.ToLower().Contains(search) ||
                        u.Name.ToLower().Contains(search))
                    .ProjectTo<SpecialityDto>(_mapper.ConfigurationProvider);

                var specialtiesPaged = await PagedList<SpecialityDto>.ToPagedList(specialties,
                    request.PageNumber, request.PageSize);

                vm.Specialities = specialtiesPaged;
            }

            if (request.Type == SearchType.ALL || request.Type == SearchType.SUBJECT)
            {
                var subjects = _dbContext.Subjects
                    .Where(u => u.SubjectTitle.ToLower().Contains(search))
                    .ProjectTo<SubjectDto>(_mapper.ConfigurationProvider);

                var subjectsPaged = await PagedList<SubjectDto>.ToPagedList(subjects,
                    request.PageNumber, request.PageSize);

                vm.Subjects = subjectsPaged;
            }

            return vm;
        }
    }
}
