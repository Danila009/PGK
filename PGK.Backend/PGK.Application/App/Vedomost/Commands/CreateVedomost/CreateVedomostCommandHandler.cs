using MediatR;
using Microsoft.EntityFrameworkCore;
using PGK.Application.Common;
using PGK.Application.Common.Exceptions;
using PGK.Application.Interfaces;
using PGK.Application.Repository.FileRepository;
using PGK.Domain.User;
using PGK.Domain.User.Student;

namespace PGK.Application.App.Vedomost.Commands.CreateVedomost
{
    internal class CreateVedomostCommandHandler
        : IRequestHandler<CreateVedomostCommand, CreateVedomostVm>
    {
        private readonly IPGKDbContext _dbContext;
        private readonly IFileRepository _fileRepository;

        public CreateVedomostCommandHandler(IPGKDbContext dbContext,
            IFileRepository fileRepository) => (_dbContext, _fileRepository) = (dbContext, fileRepository);

        public async Task<CreateVedomostVm> Handle(CreateVedomostCommand request,
            CancellationToken cancellationToken)
        {
            Domain.Group.Group group;

            if(request.Role == UserRole.HEADMAN || request.Role == UserRole.DEPUTY_HEADMAN)
            {
                var student = await _dbContext.StudentsUsers
                    .Include(u => u.Group)
                    .FirstOrDefaultAsync(u => u.Id == request.UserId);
            
                if(student == null)
                {
                    throw new NotFoundException(nameof(StudentUser), request.UserId);
                }

                group = student.Group;
            }else if(request.Role == UserRole.ADMIN)
            {
                if(request.GroupId == null)
                {
                    throw new ArgumentException("GroupId not found");
                }

                var adminGroup = await _dbContext.Groups.FindAsync(request.GroupId);


                if (adminGroup == null)
                {
                    throw new NotFoundException(nameof(Domain.Group.Group), request.GroupId);
                }

                group = adminGroup;
            } else
            {
                throw new UnauthorizedAccessException("У вас нет доступа к создании ведомасти");
            }

            var fileId = await _fileRepository.UploadFile(
                file: request.File,
                path: Constants.STATEMENT_FILE_PATH
                );

            var statement = new Domain.Vedomost.Vedomost
            {
                Url = $"{Constants.BASE_URL}/Vedomost/File/{fileId}",
                Date = request.Date,
                Group = group
            };

            await _dbContext.Vedomost.AddAsync(statement, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return new CreateVedomostVm
            {
                StatementId = statement.Id,
                StatementUrl = statement.Url
            };
        }
    }
}
