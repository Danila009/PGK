using OfficeOpenXml;
using MediatR;
using PGK.Application.Interfaces;
using PGK.Domain.Schedules;
using Microsoft.EntityFrameworkCore;
using PGK.Domain.User.Teacher;
using PGK.Application.App.Schedule.GetScheduleList.Queries;
using AutoMapper;

namespace PGK.Application.App.Schedule.Commands.FileCreateSchedule
{
    public class FileCreateScheduleCommandHandler
        : IRequestHandler<FileCreateScheduleCommand, ScheduleDto>
    {
        private readonly IPGKDbContext _dbContext;
        private readonly IMapper _mapper;

        public FileCreateScheduleCommandHandler(IPGKDbContext dbContext, IMapper mapper) =>
            (_dbContext, _mapper) = (dbContext, mapper);

        public async Task<ScheduleDto> Handle(FileCreateScheduleCommand request,
            CancellationToken cancellationToken)
        {
            MemoryStream memoryStream = new MemoryStream();
            await request.File.CopyToAsync(memoryStream);

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            var schedule = await fromFileToEntity(memoryStream);

            await _dbContext.Schedules.AddAsync(schedule, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return _mapper.Map<ScheduleDto>(schedule);
        }

        private async Task<Domain.Schedules.Schedule> fromFileToEntity(MemoryStream memoryStream)
        {
            using var package = new ExcelPackage(memoryStream);

            ExcelWorksheet worksheet = package.Workbook.Worksheets[0];

            int colCount = worksheet.Dimension.End.Column;
            int rowCount = worksheet.Dimension.End.Row;

            var schedule = new Domain.Schedules.Schedule
            {
                Date = DateTime.Now
            };

            var scheduleDepartments = new List<ScheduleDepartment>();
            var scheduleColumns = new List<ScheduleColumn>();

            for (int row = 1; row <= rowCount; row++)
            {
                for (int col = 1; col <= colCount; col++)
                {
                    var value = worksheet.Cells[row, col].Value?.ToString()?.Trim();

                    if (value == null)
                    {
                        continue;
                    }

                    if (value.ToLower().Contains("отделение"))
                    {
                        var departmentName = value.Replace("отделение", "").Trim();

                        var department = await _dbContext.Departments.FirstOrDefaultAsync(
                            u => u.Name == departmentName);

                        var scheduleDepartment = new ScheduleDepartment
                        {
                            Text = departmentName,
                            Department = department,
                            Schedule = schedule
                        };

                        scheduleDepartments.Add(scheduleDepartment);
                    }
                    else if (col == 1)
                    {
                        var group = value;
                        var change = value;
                        var groupNumber = string.Empty;
                        var speciality = string.Empty;

                        foreach (var i in value)
                        {
                            if (value.IndexOf('(') < 0)
                            {
                                change = "";
                                break;
                            }

                            if (value.IndexOf(i) > value.IndexOf('('))
                            {
                                group = group.Replace(i.ToString(), string.Empty);
                            }
                            else
                            {
                                change = change.Replace(i.ToString(), string.Empty);
                            }
                        }

                        change = change.Replace("(", string.Empty);
                        change = change.Replace(")", string.Empty);

                        var groupNumberAndSpeciality = group.Replace("(", string.Empty).Split("-");

                        var groupDb = await _dbContext.Groups
                            .Include(u => u.Speciality)
                            .FirstOrDefaultAsync(u =>
                                u.Number == int.Parse(groupNumberAndSpeciality[1]) &&
                                u.Speciality.NameAbbreviation == groupNumberAndSpeciality[0]);

                        var scheduleColumn = new ScheduleColumn
                        {
                            Text = group.Replace("(", string.Empty),
                            Time = change,
                            ScheduleDepartment = scheduleDepartments.Last(),
                            Group = groupDb
                        };

                        scheduleDepartments.Last().Columns.Add(scheduleColumn);
                        scheduleColumns.Add(scheduleColumn);

                        //strings.Add($"Группа номер: {groupNumberAndSpeciality[1]}");
                        //strings.Add($"Спецальность: {groupNumberAndSpeciality[0]}");
                        //strings.Add($"Смена: {change}");
                    }
                    else
                    {
                        var officeTeacher = value.Split(" ");

                        var scheduleRow = new ScheduleRow
                        {
                            Text = value,
                            Column = scheduleColumns.Last()
                        };

                        if (officeTeacher.Length == 2)
                        {
                            var teacherName = officeTeacher[0].Split(" ");

                            string? firstName = null;
                            string lastName = officeTeacher[0];
                            string? middleName = null;

                            if (teacherName.Length == 1)
                            {
                                lastName = teacherName[0];
                            }else if(teacherName.Length == 2)
                            {
                                lastName = teacherName[0];

                                var firstNameMiddleName = teacherName[1].Split(".");

                                firstName = firstNameMiddleName[0];
                                middleName = firstNameMiddleName[1];
                            }

                            var query = _dbContext.TeacherUsers.Where(u => u.LastName == lastName);
                            
                            TeacherUser? teacher = null;

                            if(firstName != null)
                            {
                                query = query.Where(u => u.FirstName.StartsWith(firstName));
                            }

                            if(middleName != null)
                            {
                                query = query.Where(u => u.LastName.StartsWith(lastName));
                            }

                            var teachers = await query.ToListAsync();

                            if(teachers.Count > 1)
                            {
                                throw new Exception($"ряд: {row} Колонка: {col} приподаватель: " +
                                    $"{firstName} найдено больше одного");

                            }else if(teachers.Count == 1)
                            {
                                teacher = teachers.Last();
                            }

                            var text = teacher == null ? value : officeTeacher[1];

                            scheduleRow.Teacher = teacher;
                            scheduleRow.Text = text;
                        }

                        scheduleColumns.Last().Rows.Add(scheduleRow);
                    }
                }
            }

            schedule.ScheduleDepartments = scheduleDepartments;
            

            return schedule;
        }
    }
}
