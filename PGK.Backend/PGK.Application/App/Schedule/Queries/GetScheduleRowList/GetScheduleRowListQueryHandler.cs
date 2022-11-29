using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PGK.Application.Common.Paged;
using PGK.Application.Interfaces;

namespace PGK.Application.App.Schedule.Queries.GetScheduleRowList
{
    public class GetScheduleRowListQueryHandler
        : IRequestHandler<GetScheduleRowListQuery, ScheduleRowListVm>
    {
        private readonly IPGKDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetScheduleRowListQueryHandler(IPGKDbContext dbContext,
            IMapper mapper) => (_dbContext, _mapper) = (dbContext, mapper);

        public async Task<ScheduleRowListVm> Handle(GetScheduleRowListQuery request, 
            CancellationToken cancellationToken)
        {
            var query = _dbContext.ScheduleRows
                .Include(u => u.Teacher)
                .Include(u => u.Column)
                    .ThenInclude(u => u.Group)
                        .ThenInclude(u => u.Speciality)
                .Include(u => u.Column)
                    .ThenInclude(u => u.Group)
                        .ThenInclude(u => u.Department)
                .Include(u => u.Column)
                    .ThenInclude(u => u.Group)
                        .ThenInclude(u => u.ClassroomTeacher)
                .Include(u => u.Column)
                    .ThenInclude(u => u.ScheduleDepartment)
                        .ThenInclude(u => u.Department);

            var scheduleRows = query
                .ProjectTo<ScheduleRowDto>(_mapper.ConfigurationProvider);

            var scheduleRowPaged = await PagedList<ScheduleRowDto>.ToPagedList(scheduleRows,
                request.PageNumber, request.PageSize);

            return new ScheduleRowListVm { Results = scheduleRowPaged };
        }
    }
}
