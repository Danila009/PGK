using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PGK.Application.App.Group.Queries.GetGroupDetails;
using PGK.Application.Common.Paged;
using PGK.Application.Interfaces;

namespace PGK.Application.App.Group.Queries.GetGroupList
{
    public class GetGroupListQueryHandler
        : IRequestHandler<GetGroupListQuery, GroupListVm>
    {
        private readonly IPGKDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetGroupListQueryHandler(IPGKDbContext dbContext,
            IMapper mapper) => (_dbContext, _mapper) = (dbContext, mapper);

        public async Task<GroupListVm> Handle(GetGroupListQuery request,
            CancellationToken cancellationToken)
        {
            IQueryable<Domain.Group.Group> query = _dbContext.Groups
                .Include(u => u.ClassroomTeacher)
                .Include(u => u.Speciality)
                .Include(u => u.Department)
                .Include(u => u.DeputyHeadma)
                .Include(u => u.Headman);

            if (!string.IsNullOrEmpty(request.Search))
            {
                var search = request.Search.ToLower();

                query = query.Where(u => u.Number.ToString().ToLower().Contains(search) ||
                    u.Speciality.Name.ToLower().Contains(search));
            }

            if (request.Courses != null && request.Courses.Count > 0)
            {
                query = query.Where(u => request.Courses.Contains(u.Course));
            }

            if (request.Number != null && request.Number.Count > 0)
            {
                query = query.Where(u => request.Number.Contains(u.Number));
            }

            if (request.SpecialityIds != null && request.SpecialityIds.Count > 0)
            {
                query = query.Where(u => request.SpecialityIds.Contains(u.Speciality.Id));
            }

            if (request.DepartmentIds != null && request.DepartmentIds.Count > 0)
            {
                query = query.Where(u => request.DepartmentIds.Contains(u.Department.Id));
            }

            if (request.ClassroomTeacherIds != null && request.ClassroomTeacherIds.Count > 0)
            {
                query = query.Where(u => request.ClassroomTeacherIds.Contains(u.ClassroomTeacher.Id));
            }

            if (request.DeputyHeadmaIds != null && request.DeputyHeadmaIds.Count > 0)
            {
                query = query.Where(u => request.DeputyHeadmaIds.Contains(u.DeputyHeadma.Id));
            }

            if (request.HeadmanIds != null && request.HeadmanIds.Count > 0)
            {
                query = query.Where(u => request.HeadmanIds.Contains(u.Headman.Id));
            }

            var groups = query
                .ProjectTo<GroupDetails>(_mapper.ConfigurationProvider);


            var groupsPaged = await PagedList<GroupDetails>.ToPagedList(groups, request.PageNumber, 
                request.PageSize);

            return new GroupListVm { Results =  groupsPaged };
        }
    }
}
