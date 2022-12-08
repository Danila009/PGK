using MediatR;
using PGK.Application.Common.Exceptions;
using PGK.Application.Interfaces;
using PGK.Domain.User.Teacher;

namespace PGK.Application.App.User.Teacher.Commands.DeleteTeacher
{
    internal class DeleteTeacherCommandHandler
        : IRequestHandler<DeleteTeacherCommand>
    {
        private readonly IPGKDbContext _dbContext;

        public DeleteTeacherCommandHandler(IPGKDbContext dbContext) =>
            _dbContext = dbContext;

        public async Task<Unit> Handle(DeleteTeacherCommand request,
            CancellationToken cancellationToken)
        {
            var teacher = await _dbContext.TeacherUsers.FindAsync(request.Id);

            if (teacher == null)
            {
                throw new NotFoundException(nameof(TeacherUser), request.Id);
            }

            _dbContext.TeacherUsers.Remove(teacher);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
