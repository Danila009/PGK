using MediatR;
using PGK.Application.Common.Exceptions;
using PGK.Application.Interfaces;
using PGK.Domain.User;
using PGK.Domain.User.DepartmentHead;
using PGK.Domain.User.Director;
using PGK.Domain.User.Teacher;

namespace PGK.Application.App.User.Commands.UpdateInformation
{
    internal class UpdateInformationCommandHandler
        : IRequestHandler<UpdateInformationCommand>
    {
        private readonly IPGKDbContext _dbContext;

        public UpdateInformationCommandHandler(IPGKDbContext dbContext) =>
            _dbContext = dbContext;

        public async Task<Unit> Handle(UpdateInformationCommand request,
            CancellationToken cancellationToken)
        {
            switch (request.UserRole)
            {
                case UserRole.DIRECTOR:
                    await DirectorUpdateInformation(
                        request.Information, request.UserId, cancellationToken);
                    break;

                case UserRole.DEPARTMENT_HEAD:
                    await DepartmentHeadUpdateInformation(
                        request.Information, request.UserId, cancellationToken);
                    break;

                case UserRole.TEACHER:
                    await TeacherUpdateInformation(
                        request.Information, request.UserId, cancellationToken);
                    break;
            }

            return Unit.Value;
        }

        private async Task DirectorUpdateInformation(string? information, int id,
            CancellationToken cancellationToken)
        {
            var director = await _dbContext.DirectorUsers.FindAsync(id);

            if (director == null)
            {
                throw new NotFoundException(nameof(DirectorUser), id);
            }

            director.Information = information;

            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        private async Task DepartmentHeadUpdateInformation(string? information, int id,
            CancellationToken cancellationToken)
        {
            var departmentHead = await _dbContext.DepartmentHeadUsers.FindAsync(id);

            if (departmentHead == null)
            {
                throw new NotFoundException(nameof(DepartmentHeadUser), id);
            }

            departmentHead.Information = information;

            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        private async Task TeacherUpdateInformation(string? information, int id,
            CancellationToken cancellationToken)
        {
            var teacher = await _dbContext.TeacherUsers.FindAsync(id);

            if (teacher == null)
            {
                throw new NotFoundException(nameof(TeacherUser), id);
            }

            teacher.Information = information;

            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
