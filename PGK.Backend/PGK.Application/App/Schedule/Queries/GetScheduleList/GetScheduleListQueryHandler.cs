using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PGK.Application.Common.Paged;
using PGK.Application.Interfaces;

namespace PGK.Application.App.Schedule.GetScheduleList.Queries
{
    public class GetScheduleListQueryHandler 
        : IRequestHandler<GetScheduleListQuery, ScheduleListVm>
    {
        private readonly IPGKDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetScheduleListQueryHandler(IPGKDbContext dbContext,
            IMapper mapper) => (_dbContext, _mapper) = (dbContext, mapper);

        public async Task<ScheduleListVm> Handle(GetScheduleListQuery request,
            CancellationToken cancellationToken)
        {
            IQueryable<Domain.Schedules.Schedule> query = _dbContext.Schedules
                .Include(u => u.ScheduleDepartments)
                    .ThenInclude(u => u.Department)
                .Include(u => u.ScheduleDepartments)
                    .ThenInclude(u => u.Columns)
                        .ThenInclude(u => u.Group)
                .Include(u => u.ScheduleDepartments)
                    .ThenInclude(u => u.Columns)
                        .ThenInclude(u => u.Rows)
                            .ThenInclude(u => u.Teacher);


            if(request.OnlyDate != null)
            {
                query = query.Where(u => u.Date == request.OnlyDate);
            }

            if(request.StartDate != null && request.OnlyDate == null)
            {
                query = query.Where(u => u.Date > request.StartDate);
            }

            if (request.EndDate != null && request.OnlyDate == null)
            {
                query = query.Where(u => u.Date < request.EndDate);
            }

            if(request.DepartmentIds != null && request.DepartmentIds.Count > 0)
            {
                query = query.Where(u => u.ScheduleDepartments
                    .Any(u => request.DepartmentIds.Contains(u.Department.Id)));
            }

            if(request.GroupIds != null && request.GroupIds.Count > 0)
            {
                query = query.Where(u => u.ScheduleDepartments
                    .Any(u => u.Columns.Any(u => request.GroupIds.Contains(u.Group.Id))));
            }

            var schedules = query
                .ProjectTo<ScheduleDto>(_mapper.ConfigurationProvider);

            var schedulePaged = await PagedList<ScheduleDto>.ToPagedList(schedules,
                request.PageNumber, request.PageSize);

            return new ScheduleListVm { Results = schedulePaged };
        }
    }
}
