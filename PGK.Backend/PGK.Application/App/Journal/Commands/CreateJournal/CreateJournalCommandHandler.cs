using AutoMapper;
using MediatR;
using PGK.Application.App.Journal.Queries.GetJournalList;
using PGK.Application.Interfaces;
using PGK.Application.Common.Exceptions;
using PGK.Domain.User;
using PGK.Domain.User.Teacher;

namespace PGK.Application.App.Journal.Commands.CreateJournal
{
    internal class CreateJournalCommandHandler
        : IRequestHandler<CreateJournalCommand, JournalDto>
    {
        private readonly IPGKDbContext _dbContext;
        private readonly IMapper _mapper;

        public CreateJournalCommandHandler(IPGKDbContext dbContext,
            IMapper mapper) => (_dbContext, _mapper) = (dbContext, mapper);

        public async Task<JournalDto> Handle(CreateJournalCommand request,
            CancellationToken cancellationToken)
        {
            var group = await _dbContext.Groups.FindAsync(request.GroupId);

            if(group == null)
            {
                throw new NotFoundException(nameof(Domain.Group.Group), request.GroupId);
            }

            if(request.Role == UserRole.TEACHER)
            {
                var teacher = await _dbContext.TeacherUsers.FindAsync(request.UserId);

                if(teacher == null)
                {
                    throw new NotFoundException(nameof(TeacherUser), request.UserId);
                }

                if(!teacher.Groups.Any(u => u.Id == group.Id))
                {
                    throw new UnauthorizedAccessException();
                }
            }

            var journal = new Domain.Journal.Journal
            {
                Course = request.Course,
                Semester = request.Semester,
                Group = group
            };

            await _dbContext.Journals.AddAsync(journal, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return _mapper.Map<JournalDto>(journal);
        }
    }
}
