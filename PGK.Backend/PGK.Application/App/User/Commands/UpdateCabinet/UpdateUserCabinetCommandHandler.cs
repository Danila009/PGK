using MediatR;
using PGK.Application.Interfaces;
using PGK.Domain.User;
using PGK.Application.Common.Exceptions;
using PGK.Domain.User.Director;
using PGK.Domain.User.Teacher;
using PGK.Domain.User.DepartmentHead;

namespace PGK.Application.App.User.Commands.UpdateCabinet
{
    internal class UpdateUserCabinetCommandHandler : IRequestHandler<UpdateUserCabinetCommand>
    {
        private readonly IPGKDbContext _dbContext;

        public UpdateUserCabinetCommandHandler(IPGKDbContext dbContext) =>
            _dbContext = dbContext;

        public async Task<Unit> Handle(UpdateUserCabinetCommand request,
            CancellationToken cancellationToken)
        {
            switch (request.UserRole)
            {
                case UserRole.DIRECTOR:
                    await DirectorUpdateCabinet(
                        request.Cabinet, request.UserId, cancellationToken);
                    break;

                case UserRole.DEPARTMENT_HEAD:
                    await DepartmentHeadUpdateCabinet(
                        request.Cabinet, request.UserId, cancellationToken);
                    break;

                case UserRole.TEACHER:
                    await TeacherUpdateCabinet(
                        request.Cabinet, request.UserId, cancellationToken);
                    break;
            }

            return Unit.Value;
        }

        private async Task DirectorUpdateCabinet(string? cabinet, int id,
            CancellationToken cancellationToken)
        {
            var director = await _dbContext.DirectorUsers.FindAsync(id);

            if(director == null)
            {
                throw new NotFoundException(nameof(DirectorUser), id);
            }

            director.Cabinet = cabinet;

            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        private async Task DepartmentHeadUpdateCabinet(string? cabinet, int id,
            CancellationToken cancellationToken)
        {
            var departmentHead = await _dbContext.DepartmentHeadUsers.FindAsync(id);

            if (departmentHead == null)
            {
                throw new NotFoundException(nameof(DepartmentHeadUser), id);
            }

            departmentHead.Cabinet = cabinet;

            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        private async Task TeacherUpdateCabinet(string? cabinet, int id,
            CancellationToken cancellationToken)
        {
            var teacher = await _dbContext.TeacherUsers.FindAsync(id);

            if (teacher == null)
            {
                throw new NotFoundException(nameof(TeacherUser), id);
            }

            teacher.Cabinet = cabinet;

            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
