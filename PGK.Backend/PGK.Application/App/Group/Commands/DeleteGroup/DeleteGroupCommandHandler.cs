using MediatR;
using PGK.Application.Common.Exceptions;
using PGK.Application.Interfaces;

namespace PGK.Application.App.Group.Commands.DeleteGroup
{
    public class DeleteGroupCommandHandler
        : IRequestHandler<DeleteGroupCommand>
    {
        private readonly IPGKDbContext _dbContext;

        public DeleteGroupCommandHandler(IPGKDbContext dbContext)
            => _dbContext = dbContext;

        public async Task<Unit> Handle(DeleteGroupCommand request,
            CancellationToken cancellationToken)
        {
            var group = await _dbContext.Groups.FindAsync(request.GroupId);

            if (group == null)
            {
                throw new NotFoundException(nameof(Domain.Group.Group), request.GroupId);
            }

            _dbContext.Groups.Remove(group);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
