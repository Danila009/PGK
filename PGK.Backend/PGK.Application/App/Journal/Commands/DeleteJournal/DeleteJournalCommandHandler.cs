using MediatR;
using PGK.Application.Interfaces;
using PGK.Application.Common.Exceptions;
using PGK.Domain.User;
using PGK.Domain.User.Teacher;

namespace PGK.Application.App.Journal.Commands.DeleteJournal
{
    internal class DeleteJournalCommandHandler
        : IRequestHandler<DeleteJournalCommand>
    {
        private readonly IPGKDbContext _dbContext;

        public DeleteJournalCommandHandler(IPGKDbContext dbContext) =>
            _dbContext = dbContext;

        public async Task<Unit> Handle(DeleteJournalCommand request,
            CancellationToken cancellationToken)
        {
            var journal = await _dbContext.Journals.FindAsync(request.Id);

            if (journal == null)
            {
                throw new NotFoundException(nameof(Domain.Journal.Journal), request.Id);
            }

            if (request.Role == UserRole.TEACHER)
            {
                var teacher = await _dbContext.TeacherUsers.FindAsync(request.UserId);

                if (teacher == null)
                {
                    throw new NotFoundException(nameof(TeacherUser), request.UserId);
                }

                if (!teacher.Сurator.Any(u => u.Id == journal.Group.Id))
                {
                    throw new UnauthorizedAccessException("Классный руковадитель может удалить журнал только своей группы");
                }
            }

            _dbContext.Journals.Remove(journal);
            await _dbContext.SaveChangesAsync(cancellationToken);
            
            return Unit.Value;
        }
    }
}
