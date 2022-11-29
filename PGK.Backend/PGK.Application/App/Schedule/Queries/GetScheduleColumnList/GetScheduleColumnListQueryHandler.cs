using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PGK.Application.Common.Paged;
using PGK.Application.Interfaces;
using PGK.Domain.Schedules;

namespace PGK.Application.App.Schedule.Queries.GetScheduleColumnList
{
    public class GetScheduleColumnListQueryHandler
        : IRequestHandler<GetScheduleColumnListQuery, ScheduleColumnListVm>
    {
        private readonly IPGKDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetScheduleColumnListQueryHandler(IPGKDbContext dbContext,
            IMapper mapper) => (_dbContext, _mapper) = (dbContext, mapper);

        public async Task<ScheduleColumnListVm> Handle(GetScheduleColumnListQuery request,
            CancellationToken cancellationToken)
        {
            IQueryable<ScheduleColumn> query = _dbContext.ScheduleColumns
                .Include(u => u.Group)
                    .ThenInclude(u => u.Speciality)
                .Include(u => u.Group)
                    .ThenInclude(u => u.Department)
                .Include(u => u.Group)
                    .ThenInclude(u => u.ClassroomTeacher)
                .Include(u => u.ScheduleDepartment)
                    .ThenInclude(u => u.Department)
                .Include(u => u.ScheduleDepartment)
                .Include(u => u.Rows)
                    .ThenInclude(u => u.Teacher);

            var scheduleDepartments = query
                .ProjectTo<ScheduleColumnDto>(_mapper.ConfigurationProvider);

            var scheduleDepartmentPaged = await PagedList<ScheduleColumnDto>
                .ToPagedList(scheduleDepartments, request.PageNumber, request.PageSize);

            return new ScheduleColumnListVm { Results = scheduleDepartmentPaged };
        }
    }
}
