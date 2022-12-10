using AutoMapper;
using MediatR;
using PGK.Application.App.Journal.Queries.GetJournalSubjectColumnList;
using PGK.Application.Common.Exceptions;
using PGK.Application.Interfaces;
using PGK.Domain.Journal;
using PGK.Domain.User;
using PGK.Domain.User.Teacher;

namespace PGK.Application.App.Journal.Commands.CreateJournalSubjectColumn
{
    internal class CreateJournalSubjectColumnCommandHandler
        : IRequestHandler<CreateJournalSubjectColumnCommand, JournalSubjectColumnDto>
    {
        private readonly IPGKDbContext _dbContext;
        private readonly IMapper _mapper;

        public CreateJournalSubjectColumnCommandHandler(IPGKDbContext dbContext,
            IMapper mapper) => (_dbContext, _mapper) = (dbContext, mapper);

        public async Task<JournalSubjectColumnDto> Handle(CreateJournalSubjectColumnCommand request,
            CancellationToken cancellationToken)
        {
            var journalSubjectRow = await _dbContext.JournalSubjectRows.FindAsync(request.JournalSubjectRowId);

            if(journalSubjectRow == null)
            {
                throw new NotFoundException(nameof(JournalSubjectRow), request.JournalSubjectRowId);
            }

            var column = new JournalSubjectColumn
            {
                Evaluation = request.Evaluation,
                Date = request.Date,
                Row = journalSubjectRow
            };

            if(request.Role == UserRole.TEACHER)
            {
                var teacher = await _dbContext.TeacherUsers.FindAsync(request.UserId);

                if(teacher == null)
                {
                    throw new NotFoundException(nameof(TeacherUser), request.UserId);
                }

                if(journalSubjectRow.JournalSubject.Teacher.Id != teacher.Id)
                {
                    throw new UnauthorizedAccessException("Преподаватель может взаимодействовать только со своим предметом");
                }
            }

            await _dbContext.JournalSubjectColumns.AddAsync(column, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return _mapper.Map<JournalSubjectColumnDto>(column);
        }
    }
}
