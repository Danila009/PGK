using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PGK.Application.Common.Paged;
using PGK.Application.Interfaces;
using PGK.Domain.Schedules;

namespace PGK.Application.App.Schedule.Queries.GetScheduleDepartmentList
{
    internal class GetScheduleDepartmentListQueryHandler
        : IRequestHandler<GetScheduleDepartmentListQuery, ScheduleDepartmentListVm>
    {
        private readonly IPGKDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetScheduleDepartmentListQueryHandler(IPGKDbContext dbContext,
            IMapper mapper) => (_dbContext, _mapper) = (dbContext, mapper);

        public async Task<ScheduleDepartmentListVm> Handle(GetScheduleDepartmentListQuery request,
            CancellationToken cancellationToken)
        {
            IQueryable<ScheduleDepartment> query = _dbContext.ScheduleDepartments
                .Include(u => u.Schedule)
                .Include(u => u.Department)
                .Include(u => u.Columns)
                    .ThenInclude(u => u.Group)
                        .ThenInclude(u => u.Speciality)
                .Include(u => u.Columns)
                    .ThenInclude(u => u.Group)
                        .ThenInclude(u => u.Headman)
                .Include(u => u.Columns)
                    .ThenInclude(u => u.Group)
                        .ThenInclude(u => u.ClassroomTeacher)
                .Include(u => u.Columns)
                    .ThenInclude(u => u.Rows)
                .Include(u => u.Columns)
                    .ThenInclude(u => u.Rows)
                        .ThenInclude(u => u.Teacher);

            var scheduleDepartments = query
                .ProjectTo<ScheduleDepartmentDto>(_mapper.ConfigurationProvider);

            var scheduleDepartmentPaged = await PagedList<ScheduleDepartmentDto>
                .ToPagedList(scheduleDepartments,request.PageNumber, request.PageSize);

            return new ScheduleDepartmentListVm { Results = scheduleDepartmentPaged };
        }
    }
}
