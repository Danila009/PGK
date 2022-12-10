using MediatR;
using PGK.Application.Common.Exceptions;
using PGK.Application.Interfaces;
using PGK.Domain.Journal;
using PGK.Domain.User;
using PGK.Domain.User.Teacher;

namespace PGK.Application.App.Journal.Commands.DeleteJournalSubject
{
    internal class DeleteJournalSubjectCommandHandler
        : IRequestHandler<DeleteJournalSubjectCommand>
    {
        private readonly IPGKDbContext _dbContext;

        public DeleteJournalSubjectCommandHandler(IPGKDbContext dbContext) =>
            _dbContext = dbContext;

        public async Task<Unit> Handle(DeleteJournalSubjectCommand request,
            CancellationToken cancellationToken)
        {
            var journalSubject = await _dbContext.JournalSubjects.FindAsync(request.Id);

            if (journalSubject == null)
            {
                throw new NotFoundException(nameof(JournalSubject), request.Id);
            }

            if (request.Role == UserRole.TEACHER)
            {
                var teacherUser = await _dbContext.TeacherUsers.FindAsync(request.UserId) ??
                    throw new NotFoundException(nameof(TeacherUser), request.UserId);

                if (!teacherUser.Subjects.Any(u => u.Id == journalSubject.Subject.Id))
                {
                    throw new ArgumentException("Преподаватель может взаимодействовать только со своим предметом");
                }
            }

            _dbContext.JournalSubjects.Remove(journalSubject);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
