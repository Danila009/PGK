using MediatR;
using PGK.Application.Common.Exceptions;
using PGK.Application.Interfaces;
using PGK.Domain.User.EducationalSector;

namespace PGK.Application.App.User.EducationalSector.Commands.DeleteEducationalSector
{
    internal class DeleteEducationalSectorCommandHandler
        : IRequestHandler<DeleteEducationalSectorCommand>
    {
        private readonly IPGKDbContext _dbContext;

        public DeleteEducationalSectorCommandHandler(IPGKDbContext dbContext) =>
            _dbContext = dbContext;

        public async Task<Unit> Handle(DeleteEducationalSectorCommand request,
            CancellationToken cancellationToken)
        {
            var educationalSector = await _dbContext.EducationalSectorUsers.FindAsync(request.Id);

            if (educationalSector == null)
            {
                throw new NotFoundException(nameof(EducationalSectorUser), request.Id);
            }

            _dbContext.EducationalSectorUsers.Remove(educationalSector);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
