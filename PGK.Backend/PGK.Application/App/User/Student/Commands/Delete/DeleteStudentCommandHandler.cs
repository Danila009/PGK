using MediatR;
using PGK.Application.Interfaces;
using PGK.Application.Common.Exceptions;
using PGK.Domain.User.Student;

namespace PGK.Application.App.User.Student.Commands.Delete
{
    internal class DeleteStudentCommandHandler
        : IRequestHandler<DeleteStudentCommand>
    {
        private readonly IPGKDbContext _dbContext;

        public DeleteStudentCommandHandler(IPGKDbContext dbContext) =>
            _dbContext = dbContext;

        public async Task<Unit> Handle(DeleteStudentCommand request,
            CancellationToken cancellationToken)
        {
            var user = await _dbContext.StudentsUsers.FindAsync(request.StudentId);

            if (user == null)
            {
                throw new NotFoundException(nameof(StudentUser), request.StudentId);
            }

            _dbContext.StudentsUsers.Remove(user);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
