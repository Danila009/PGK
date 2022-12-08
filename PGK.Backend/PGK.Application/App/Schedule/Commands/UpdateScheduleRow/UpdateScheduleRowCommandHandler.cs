using AutoMapper;
using MediatR;
using PGK.Application.App.Schedule.Queries.GetScheduleRowList;
using PGK.Application.Common.Exceptions;
using PGK.Application.Interfaces;
using PGK.Domain.Schedules;
using PGK.Domain.User.Teacher;

namespace PGK.Application.App.Schedule.Commands.UpdateScheduleRow
{
    internal class UpdateScheduleRowCommandHandler
        : IRequestHandler<UpdateScheduleRowCommand, ScheduleRowDto>
    {
        private readonly IPGKDbContext _dbContext;
        private readonly IMapper _mapper;

        public UpdateScheduleRowCommandHandler(IPGKDbContext dbContext,
            IMapper mapper) => (_dbContext, _mapper) = (dbContext, mapper);

        public async Task<ScheduleRowDto> Handle(UpdateScheduleRowCommand request,
            CancellationToken cancellationToken)
        {
            var row = await _dbContext.ScheduleRows.FindAsync(request.Id);

            if(row == null)
            {
                throw new NotFoundException(nameof(ScheduleRow), request.Id);
            }

            var teacher = await _dbContext.TeacherUsers.FindAsync(request.TeacherId);

            if (teacher == null)
            {
                throw new NotFoundException(nameof(TeacherUser), request.TeacherId);
            }

            row.Text = request.Text;
            row.Teacher = teacher;

            await _dbContext.SaveChangesAsync(cancellationToken);

            return _mapper.Map<ScheduleRowDto>(row);
        }
    }
}
