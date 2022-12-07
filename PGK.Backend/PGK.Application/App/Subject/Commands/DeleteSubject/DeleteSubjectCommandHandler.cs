using MediatR;
using PGK.Application.Common.Exceptions;
using PGK.Application.Interfaces;

namespace PGK.Application.App.Subject.Commands.DeleteSubject
{
    internal class DeleteSubjectCommandHandler
        : IRequestHandler<DeleteSubjectCommand>
    {
        private readonly IPGKDbContext _dbContext;

        public DeleteSubjectCommandHandler(IPGKDbContext dbContext) =>
            _dbContext = dbContext;

        public async Task<Unit> Handle(DeleteSubjectCommand request,
            CancellationToken cancellationToken)
        {
            var subject = await _dbContext.Subjects.FindAsync(request.Id);

            if (subject == null)
            {
                throw new NotFoundException(nameof(Domain.Subject.Subject), request.Id);
            }

            _dbContext.Subjects.Remove(subject);
            await _dbContext.SaveChangesAsync(cancellationToken);
        
            return Unit.Value;
        }
    }
}
