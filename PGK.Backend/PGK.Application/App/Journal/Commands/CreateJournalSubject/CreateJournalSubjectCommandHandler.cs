using AutoMapper;
using MediatR;
using PGK.Application.App.Journal.Queries.GetJournalSubjectList;
using PGK.Application.Common.Exceptions;
using PGK.Application.Interfaces;
using PGK.Domain.Journal;
using PGK.Domain.User;
using PGK.Domain.User.Teacher;

namespace PGK.Application.App.Journal.Commands.CreateJournalSubject
{
    internal class CreateJournalSubjectCommandHandler
        : IRequestHandler<CreateJournalSubjectCommand, JournalSubjectDto>
    {
        private readonly IPGKDbContext _dbContext;
        private readonly IMapper _mapper;

        public CreateJournalSubjectCommandHandler(IPGKDbContext dbContext,
            IMapper mapper) => (_dbContext, _mapper) = (dbContext, mapper);

        public async Task<JournalSubjectDto> Handle(CreateJournalSubjectCommand request,
            CancellationToken cancellationToken)
        {
            var subject = await _dbContext.Subjects.FindAsync(request.SubjectId);

            if(subject == null)
            {
                throw new NotFoundException(nameof(Domain.Subject.Subject), request.SubjectId);
            }

            TeacherUser teacherUser;

            if(request.Role == UserRole.TEACHER)
            {
                teacherUser = await _dbContext.TeacherUsers.FindAsync(request.UserId) ?? 
                    throw new NotFoundException(nameof(TeacherUser), request.UserId);

                if(!teacherUser.Subjects.Any(u => u.Id == subject.Id))
                {
                    throw new ArgumentException("Преподаватель может взаимодействовать только со своим предметом");
                }
            }
            else
            {
                if(request.TeacherId == null)
                {
                    throw new ArgumentException("TeacherId not found");
                }

                teacherUser = await _dbContext.TeacherUsers.FindAsync(request.TeacherId) ??
                    throw new NotFoundException(nameof(TeacherUser), request.TeacherId);
            }

            var journal = await _dbContext.Journals.FindAsync(request.JournalId);

            if(journal == null)
            {
                throw new NotFoundException(nameof(Domain.Journal.Journal), request.JournalId);
            }

            var journalSubject = new JournalSubject
            {
                Hours = request.Hours,
                Subject = subject,
                Teacher = teacherUser,
                Journal = journal
            };

            await _dbContext.JournalSubjects.AddAsync(journalSubject, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return _mapper.Map<JournalSubjectDto>(journalSubject);
        }
    }
}
