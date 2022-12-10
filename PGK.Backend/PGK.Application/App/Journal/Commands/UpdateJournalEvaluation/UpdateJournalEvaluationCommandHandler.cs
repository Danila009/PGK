using AutoMapper;
using MediatR;
using PGK.Application.App.Journal.Queries.GetJournalSubjectColumnList;
using PGK.Application.Common.Exceptions;
using PGK.Application.Interfaces;
using PGK.Domain.Journal;
using PGK.Domain.User;
using PGK.Domain.User.Teacher;

namespace PGK.Application.App.Journal.Commands.UpdateJournalEvaluation
{
    internal class UpdateJournalEvaluationCommandHandler
        : IRequestHandler<UpdateJournalEvaluationCommand, JournalSubjectColumnDto>
    {
        private readonly IPGKDbContext _dbContext;
        private readonly IMapper _mapper;

        public UpdateJournalEvaluationCommandHandler(IPGKDbContext dbContext,
            IMapper mapper) => (_dbContext, _mapper) = (dbContext, mapper);

        public async Task<JournalSubjectColumnDto> Handle(UpdateJournalEvaluationCommand request, 
            CancellationToken cancellationToken)
        {
            var column = await _dbContext.JournalSubjectColumns.FindAsync(request.JournalColumnId);

            if(column == null)
            {
                throw new NotFoundException(nameof(JournalSubjectColumn), request.JournalColumnId);
            }

            if(request.Role == UserRole.TEACHER)
            {
                var teacher = await _dbContext.TeacherUsers.FindAsync(request.UserId);

                if (teacher == null)
                {
                    throw new NotFoundException(nameof(TeacherUser), request.UserId);
                }

                if (teacher.Id != column.Row.JournalSubject.Teacher.Id)
                {
                    throw new ArgumentException("Преподаватель может взаимодействовать только со своим предметом");
                }
            }

            column.Evaluation = request.Evaluation;
            await _dbContext.SaveChangesAsync(cancellationToken);

            return _mapper.Map<JournalSubjectColumnDto>(column);
        }
    }
}
