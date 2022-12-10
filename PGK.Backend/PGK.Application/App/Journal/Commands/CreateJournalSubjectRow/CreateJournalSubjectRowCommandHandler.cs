using AutoMapper;
using MediatR;
using PGK.Application.App.Journal.Queries.GetJournalSubjectRowList;
using PGK.Application.Common.Exceptions;
using PGK.Application.Interfaces;
using PGK.Domain.Journal;
using PGK.Domain.User;
using PGK.Domain.User.Student;
using PGK.Domain.User.Teacher;

namespace PGK.Application.App.Journal.Commands.CreateJournalSubjectRow
{
    internal class CreateJournalSubjectRowCommandHandler
        : IRequestHandler<CreateJournalSubjectRowCommand, JournalSubjectRowDto>
    {
        private readonly IPGKDbContext _dbContext;
        private readonly IMapper _mapper;

        public CreateJournalSubjectRowCommandHandler(IPGKDbContext dbContext,
            IMapper mapper) => (_dbContext, _mapper) = (dbContext, mapper);

        public async Task<JournalSubjectRowDto> Handle(CreateJournalSubjectRowCommand request,
            CancellationToken cancellationToken)
        {
            var journalSubject = await _dbContext.JournalSubjects.FindAsync(request.JournalSubjectId);

            if(journalSubject == null)
            {
                throw new NotFoundException(nameof(JournalSubject), request.JournalSubjectId);
            }

            if(request.Role == UserRole.TEACHER)
            {
                var teacher = await _dbContext.TeacherUsers.FindAsync(request.UserId);

                if(teacher == null)
                {
                    throw new NotFoundException(nameof(TeacherUser), request.UserId);
                }

                if(teacher.Id != journalSubject.Teacher.Id)
                {
                    throw new ArgumentException("Преподаватель может взаимодействовать только со своим предметом");
                }
            }

            var student = await _dbContext.StudentsUsers.FindAsync(request.StudentId);

            if(student == null)
            {
                throw new NotFoundException(nameof(StudentUser), request.StudentId);
            }

            if(journalSubject.Journal.Group.Id != student.Group.Id)
            {
                throw new ArgumentException();
            }

            var row = new JournalSubjectRow
            {
                Student = student,
                JournalSubject = journalSubject
            };

            await _dbContext.JournalSubjectRows.AddAsync(row, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return _mapper.Map<JournalSubjectRowDto>(row);
        }
    }
}
