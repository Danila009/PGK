using AutoMapper;
using MediatR;
using PGK.Application.App.Journal.Queries.GetJournalSubjectColumnList;
using PGK.Application.Common.Exceptions;
using PGK.Application.Interfaces;
using PGK.Application.Services.FCMService;
using PGK.Domain.Journal;
using PGK.Domain.Notification;
using PGK.Domain.User;
using PGK.Domain.User.Teacher;

namespace PGK.Application.App.Journal.Commands.CreateJournalSubjectColumn
{
    internal class CreateJournalSubjectColumnCommandHandler
        : IRequestHandler<CreateJournalSubjectColumnCommand, JournalSubjectColumnDto>
    {
        private readonly IPGKDbContext _dbContext;
        private readonly IFCMService _fCMService;
        private readonly IMapper _mapper;

        public CreateJournalSubjectColumnCommandHandler(IPGKDbContext dbContext,
            IFCMService fCMService,IMapper mapper) => 
            (_dbContext, _fCMService, _mapper) = (dbContext, fCMService, mapper);

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

            var notification = new Notification
            {
                Title = "Вам поставили оценку в журнале",
                Message = $"Оценка {column.Evaluation}," +
                $" предмет {journalSubjectRow.JournalSubject.Subject.SubjectTitle}, " +
                $"преподаватель {journalSubjectRow.JournalSubject.Teacher.LastName}",
                Users = new List<Domain.User.User> { journalSubjectRow.Student }
            };

            await _dbContext.JournalSubjectColumns.AddAsync(column, cancellationToken);
            await _dbContext.Notifications.AddAsync(notification, cancellationToken);

            await _dbContext.SaveChangesAsync(cancellationToken);

            if (journalSubjectRow.Student.IncludedJournalNotifications)
            {
                await _fCMService.SendMessage(
                    notification.Title,
                    notification.Message,
                    notification.Users.Last().Id.ToString()
               );
            }

            return _mapper.Map<JournalSubjectColumnDto>(column);
        }
    }
}
