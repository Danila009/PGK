using AutoMapper;
using MediatR;
using PGK.Application.Common.Exceptions;
using PGK.Application.Interfaces;
using PGK.Domain.Journal;
using PGK.Domain.User;
using PGK.Domain.User.Teacher;

namespace PGK.Application.App.Journal.Commands.CreateJournalTopic
{
    internal class CreateJournalTopicCommandHandler
        : IRequestHandler<CreateJournalTopicCommand>
    {
        private readonly IPGKDbContext _dbContext;
        private readonly IMapper _mapper;

        public CreateJournalTopicCommandHandler(IPGKDbContext dbContext,
            IMapper mapper) => (_dbContext, _mapper) = (dbContext, mapper);

        public async Task<Unit> Handle(CreateJournalTopicCommand request,
            CancellationToken cancellationToken)
        {
            var journalSubject = await _dbContext.JournalSubjects.FindAsync(request.JournalSubjectId);

            if (journalSubject == null)
            {
                throw new NotFoundException(nameof(JournalSubject), request.JournalSubjectId);
            }

            if(request.Role == UserRole.TEACHER)
            {
                var teacher = await _dbContext.TeacherUsers.FindAsync(request.UserId);

                if (teacher == null)
                {
                    throw new NotFoundException(nameof(TeacherUser), request.UserId);
                }

                if (journalSubject.Teacher.Id != teacher.Id)
                {
                    throw new UnauthorizedAccessException("Преподаватель может взаимодействовать только со своим предметом");
                }
            }

            var topic = new JournalTopic
            {
                Title = request.Title,
                HomeWork = request.HomeWork,
                Hours = request.Hours,
                Date = request.Date,
                JournalSubject = journalSubject
            };

            await _dbContext.JournalTopics.AddAsync(topic);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
