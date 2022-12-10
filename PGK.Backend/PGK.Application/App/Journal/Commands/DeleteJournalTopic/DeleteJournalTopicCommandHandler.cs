using MediatR;
using PGK.Application.Common.Exceptions;
using PGK.Application.Interfaces;
using PGK.Domain.Journal;
using PGK.Domain.User;
using PGK.Domain.User.Teacher;

namespace PGK.Application.App.Journal.Commands.DeleteJournalTopic
{
    internal class DeleteJournalTopicCommandHandler
        : IRequestHandler<DeleteJournalTopicCommand>
    {
        private readonly IPGKDbContext _dbContext;

        public DeleteJournalTopicCommandHandler(IPGKDbContext dbContext) =>
            _dbContext = dbContext;

        public async Task<Unit> Handle(DeleteJournalTopicCommand request,
            CancellationToken cancellationToken)
        {
            var journalTopic = await _dbContext.JournalTopics.FindAsync(request.Id);

            if (journalTopic == null)
            {
                throw new NotFoundException(nameof(JournalTopic), request.Id);
            }

            if (request.Role == UserRole.TEACHER)
            {
                var teacher = await _dbContext.TeacherUsers.FindAsync(request.UserId);

                if (teacher == null)
                {
                    throw new NotFoundException(nameof(TeacherUser), request.UserId);
                }

                if (journalTopic.JournalSubject.Teacher.Id != teacher.Id)
                {
                    throw new UnauthorizedAccessException("Преподаватель может взаимодействовать только со своим предметом");
                }
            }

            _dbContext.JournalTopics.Remove(journalTopic);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
