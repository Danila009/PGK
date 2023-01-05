using MediatR;
using PGK.Application.Interfaces;
using PGK.Application.Common.Exceptions;
using PGK.Domain.User.Director;

namespace PGK.Application.App.User.Director.Commands.UpdateCurrent
{
    internal class DirectorUpdateCurrentCommandHandler
        : IRequestHandler<DirectorUpdateCurrentCommand>
    {
        private readonly IPGKDbContext _dbContext;

        public DirectorUpdateCurrentCommandHandler(IPGKDbContext dbContext) =>
            _dbContext = dbContext;

        public async Task<Unit> Handle(DirectorUpdateCurrentCommand request,
            CancellationToken cancellationToken)
        {
            var director = await _dbContext.DirectorUsers.FindAsync(request.DirectorId);

            if(director == null)
            {
                throw new NotFoundException(nameof(DirectorUser), request.DirectorId);
            }

            director.Current = !director.Current;

            await _dbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
