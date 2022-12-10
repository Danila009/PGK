using MediatR;
using Microsoft.EntityFrameworkCore;
using PGK.Application.Common.Exceptions;
using PGK.Application.Interfaces;
using PGK.Domain.Raportichka;
using PGK.Domain.User;
using PGK.Domain.User.Student;
using PGK.Domain.User.Teacher;

namespace PGK.Application.App.Raportichka.Row.Commands.CreateRow
{
    public class CreateRaportichkaRowCommandHandler
        : IRequestHandler<CreateRaportichkaRowCommand, CreateRaportichkaRowVm>
    {
        private readonly IPGKDbContext _dbContext;

        public CreateRaportichkaRowCommandHandler(IPGKDbContext dbContext) =>
            _dbContext = dbContext;

        public async Task<CreateRaportichkaRowVm> Handle(CreateRaportichkaRowCommand request,
            CancellationToken cancellationToken)
        {
            var teacherId = request.Role == UserRole.TEACHER ? request.UserId : request.TeacherId;

            var teacher = await _dbContext.TeacherUsers
                .Include(u => u.RaportichkaRows)
                    .ThenInclude(u => u.Raportichka)
                .FirstOrDefaultAsync(u => u.Id == teacherId);

            if (teacher == null)
            {
                throw new NotFoundException(nameof(TeacherUser), teacherId ?? 0);
            }

            //if (
            //    request.Role == UserRole.TEACHER && !teacher.RaportichkaRows
            //    .Any(u => u.Raportichka.Id == request.RaportichkaId)
            //    )
            //{
            //    throw new UnauthorizedAccessException("");
            //}

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

                if(raportichka.Group.Id != studentHeadman.Group.Id)
                {
                    throw new UnauthorizedAccessException("Вы пытаетесь изменить не свою группу");
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

            if(raportichka.Group != student.Group)
            {
                throw new Exception("У студент и рапортички разные группы");
            }

            var row = new RaportichkaRow
            {
                NumberLesson = request.NumberLesson,
                Confirmation = false,
                Hours = request.Hours,
                Student = student,
                Subject = subject,
                Teacher = teacher,
                Raportichka = raportichka
            };

            await _dbContext.RaportichkaRows.AddAsync(row);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return new CreateRaportichkaRowVm
            {
                Id = row.Id
            };
        }
    }
}
