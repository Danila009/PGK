using MediatR;
using PGK.Application.Interfaces;

namespace PGK.Application.App.Subject.Commands.CreateSubject
{
    public class CreateSubjectCommandHandler
        : IRequestHandler<CreateSubjectCommand, CreateSubjectVm>
    {
        private readonly IPGKDbContext _dbContext;

        public CreateSubjectCommandHandler(IPGKDbContext dbContext) =>
            _dbContext = dbContext;

        public async Task<CreateSubjectVm> Handle(CreateSubjectCommand request,
            CancellationToken cancellationToken)
        {
            var subject = new Domain.Subject.Subject
            {
                SubjectTitle = request.SubjectTitle
            };

            await _dbContext.Subjects.AddAsync(subject, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return new CreateSubjectVm
            {
                Id = subject.Id
            };
        }
    }
}
