using MediatR;
using PGK.Application.Common.Exceptions;
using PGK.Application.Interfaces;
using PGK.Domain.User.Teacher;

namespace PGK.Application.App.User.Teacher.Commands.TeacherAddSubject
{
    public class TeacherAddSubjectCommandHandler
        : IRequestHandler<TeacherAddSubjectCommand>
    {
        private readonly IPGKDbContext _dbContext;

        public TeacherAddSubjectCommandHandler(IPGKDbContext dbContext) =>
            _dbContext = dbContext;

        public async Task<Unit> Handle(TeacherAddSubjectCommand request,
            CancellationToken cancellationToken)
        {
            var teacher = await _dbContext.TeacherUsers.FindAsync(request.TeacgerId);

            if (teacher == null)
            {
                throw new NotFoundException(nameof(TeacherUser), request.TeacgerId);
            }

            var subject = await _dbContext.Subjects.FindAsync(request.SubjectId);

            if (subject == null)
            {
                throw new NotFoundException(nameof(Domain.Subject.Subject), request.SubjectId);
            }

            teacher.Subjects.Add(subject);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
