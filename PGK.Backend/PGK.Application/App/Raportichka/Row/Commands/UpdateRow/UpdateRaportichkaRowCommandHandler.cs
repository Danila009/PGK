using MediatR;
using Microsoft.EntityFrameworkCore;
using PGK.Application.Common.Exceptions;
using PGK.Application.Interfaces;
using PGK.Domain.User;
using PGK.Domain.User.Student;
using PGK.Domain.User.Teacher;

namespace PGK.Application.App.Raportichka.Row.Commands.UpdateRow
{
    public class UpdateRaportichkaRowCommandHandler
        : IRequestHandler<UpdateRaportichkaRowCommand>
    {
        private readonly IPGKDbContext _dbContext;

        public UpdateRaportichkaRowCommandHandler(IPGKDbContext dbContext) =>
            _dbContext = dbContext;

        public async Task<Unit> Handle(UpdateRaportichkaRowCommand request,
            CancellationToken cancellationToken)
        {
            var row = await _dbContext.RaportichkaRows.FindAsync(request.RowId);

            if (row == null)
            {
                throw new NotFoundException(nameof(Domain.Raportichka.RaportichkaRow),
                    request.RowId);
            }

            var raportichka = await _dbContext.Raportichkas
                .Include(u => u.Group)
                .FirstOrDefaultAsync(u => u.Id == request.RaportichkaId);

            if (raportichka == null)
            {
                throw new NotFoundException(nameof(Domain.Raportichka.Raportichka),
                    request.RaportichkaId);
            }

            if (request.Role == UserRole.HEADMAN || request.Role == UserRole.DEPUTY_HEADMAN)
            {
                var studentHeadman = await _dbContext.StudentsUsers
                    .Include(u => u.Group)
                    .FirstOrDefaultAsync(u => u.Id == request.UserId);

                if (studentHeadman == null)
                {
                    throw new NotFoundException(nameof(StudentUser), request.RaportichkaId);
                }

                if (raportichka.Group.Id != studentHeadman.Group.Id)
                {
                    throw new UnauthorizedAccessException();
                }
            }

            var subject = await _dbContext.Subjects.FindAsync(request.SubjectId);

            if (subject == null)
            {
                throw new NotFoundException(nameof(Domain.Subject.Subject),
                    request.SubjectId);
            }

            var student = await _dbContext.StudentsUsers
               .Include(u => u.Group)
               .FirstOrDefaultAsync(u => u.Id == request.StudentId);

            if (student == null)
            {
                throw new NotFoundException(nameof(StudentUser),
                    request.StudentId);
            }

            if (raportichka.Group != student.Group)
            {
                throw new Exception();
            }

            if (request.Role != UserRole.TEACHER)
            {
                var teacher = await _dbContext.TeacherUsers.FindAsync(request.TeacherId);

                if (teacher == null)
                {
                    throw new NotFoundException(nameof(TeacherUser),
                        request.TeacherId);
                }

                row.Teacher = teacher;
            }

            row.NumberLesson = request.NumberLesson;
            row.Confirmation = request.Confirmation;
            row.Hours = request.Hours;
            row.Subject = subject;
            row.Student = student;
            row.Raportichka = raportichka;

            await _dbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
