using MediatR;
using PGK.Application.Common.Exceptions;
using PGK.Application.Interfaces;
using PGK.Domain.Journal;
using PGK.Domain.User;
using PGK.Domain.User.Teacher;

namespace PGK.Application.App.Journal.Commands.DeleteJournalSubjectRow
{
    internal class DeleteJournalSubjectRowCommandHandler
        : IRequestHandler<DeleteJournalSubjectRowCommand>
    {
        private readonly IPGKDbContext _dbContext;

        public DeleteJournalSubjectRowCommandHandler(IPGKDbContext dbContext) =>
            _dbContext = dbContext;

        public async Task<Unit> Handle(DeleteJournalSubjectRowCommand request,
            CancellationToken cancellationToken)
        {
            var row = await _dbContext.JournalSubjectRows.FindAsync(request.Id);

            if(row == null)
            {
                throw new NotFoundException(nameof(JournalSubjectRow), request.Id);
            }

            if (request.Role == UserRole.TEACHER)
            {
                var teacher = await _dbContext.TeacherUsers.FindAsync(request.UserId);

                if (teacher == null)
                {
                    throw new NotFoundException(nameof(TeacherUser), request.UserId);
                }

                if (teacher.Id != row.JournalSubject.Teacher.Id)
                {
                    throw new ArgumentException("Преподаватель может взаимодействовать только со своим предметом");
                }
            }

            _dbContext.JournalSubjectRows.Remove(row);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
