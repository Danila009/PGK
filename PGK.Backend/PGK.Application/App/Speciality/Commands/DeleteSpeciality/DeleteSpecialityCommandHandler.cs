using MediatR;
using PGK.Application.Common.Exceptions;
using PGK.Application.Interfaces;

namespace PGK.Application.App.Speciality.Commands.DeleteSpeciality
{
    internal class DeleteSpecialityCommandHandler
        : IRequestHandler<DeleteSpecialityCommand>
    {
        private readonly IPGKDbContext _dbContext;

        public DeleteSpecialityCommandHandler(IPGKDbContext dbContext) =>
            _dbContext = dbContext;


        public async Task<Unit> Handle(DeleteSpecialityCommand request,
            CancellationToken cancellationToken)
        {
            var speciality = await _dbContext.Specialties.FindAsync(request.Id);

            if (speciality == null)
            {
                throw new NotFoundException(nameof(Domain.Speciality.Speciality), request.Id);
            }

            _dbContext.Specialties.Remove(speciality);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
