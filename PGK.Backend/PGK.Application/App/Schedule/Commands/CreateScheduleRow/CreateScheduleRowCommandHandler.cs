using MediatR;
using PGK.Application.Common.Exceptions;
using PGK.Application.Interfaces;
using PGK.Domain.Schedules;
using PGK.Domain.User.Teacher;

namespace PGK.Application.App.Schedule.Commands.CreateScheduleRow
{
    public class CreateScheduleRowCommandHandler
        : IRequestHandler<CreateScheduleRowCommand, CreateScheduleRowVm>
    {
        private readonly IPGKDbContext _dbContext;

        public CreateScheduleRowCommandHandler(IPGKDbContext dbContext) =>
            _dbContext = dbContext;

        public async Task<CreateScheduleRowVm> Handle(CreateScheduleRowCommand request,
            CancellationToken cancellationToken)
        {
            var column = await _dbContext.ScheduleColumns.FindAsync(request.ScheduleColumnId);

            if (column == null)
            {
                throw new NotFoundException(nameof(ScheduleColumn), request.ScheduleColumnId);
            }

            var teacher = await _dbContext.TeacherUsers.FindAsync(request.TeacherId);

            if(teacher == null)
            {
                throw new NotFoundException(nameof(TeacherUser), request.TeacherId);
            }

            var row = new ScheduleRow
            {
                Teacher = teacher,
                Column = column
            };

            await _dbContext.ScheduleRows.AddAsync(row, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return new CreateScheduleRowVm
            {
                ScheduleRowId = row.Id
            };
        }
    }
}
