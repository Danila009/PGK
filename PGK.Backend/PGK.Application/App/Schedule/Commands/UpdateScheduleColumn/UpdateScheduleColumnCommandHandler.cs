using AutoMapper;
using MediatR;
using PGK.Application.App.Schedule.Queries.GetScheduleColumnList;
using PGK.Application.Common.Exceptions;
using PGK.Application.Interfaces;

namespace PGK.Application.App.Schedule.Commands.UpdateScheduleColumn
{
    internal class UpdateScheduleColumnCommandHandler
        : IRequestHandler<UpdateScheduleColumnCommand, ScheduleColumnDto>
    {
        private readonly IPGKDbContext _dbContext;
        private readonly IMapper _mapper;

        public UpdateScheduleColumnCommandHandler(IPGKDbContext dbContext,
            IMapper mapper) => (_dbContext, _mapper) = (dbContext, mapper);

        public async Task<ScheduleColumnDto> Handle(UpdateScheduleColumnCommand request,
            CancellationToken cancellationToken)
        {
            var column = await _dbContext.ScheduleColumns.FindAsync(request.Id);

            if (column == null)
            {
                throw new NotFoundException(nameof(Domain.Schedules.ScheduleColumn), request.Id);
            }

            var group = await _dbContext.Groups.FindAsync(request.GroupId);

            if (group == null)
            {
                throw new NotFoundException(nameof(Domain.Group.Group), request.GroupId);
            }

            column.Text = request.Text;
            column.Time = request.Time;
            column.Group = group;

            await _dbContext.SaveChangesAsync(cancellationToken);

            return _mapper.Map<ScheduleColumnDto>(column);
        }
    }
}
