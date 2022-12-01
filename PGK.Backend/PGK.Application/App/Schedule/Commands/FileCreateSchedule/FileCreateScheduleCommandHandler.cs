using OfficeOpenXml;
using MediatR;
using PGK.Application.Interfaces;
using PGK.Domain.Schedules;

namespace PGK.Application.App.Schedule.Commands.FileCreateSchedule
{
    public class FileCreateScheduleCommandHandler
        : IRequestHandler<FileCreateScheduleCommand>
    {
        private readonly IPGKDbContext _dbContext;

        public FileCreateScheduleCommandHandler(IPGKDbContext dbContext) =>
            _dbContext = dbContext;

        public async Task<Unit> Handle(FileCreateScheduleCommand request,
            CancellationToken cancellationToken)
        {
            MemoryStream memoryStream = new MemoryStream();
            await request.File.CopyToAsync(memoryStream);

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            var schedule = fromFileToEntity(memoryStream);

            Console.WriteLine(schedule);

            await _dbContext.Schedules.AddAsync(schedule, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }

        private Domain.Schedules.Schedule fromFileToEntity(MemoryStream memoryStream)
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
                        var scheduleDepartment = new ScheduleDepartment
                        {
                            Text = value,
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

                        var scheduleColumn = new ScheduleColumn
                        {
                            Text = group.Replace("(", string.Empty),
                            Time = change,
                            ScheduleDepartment = scheduleDepartments.Last()
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

                        scheduleColumns.Last().Rows.Add(scheduleRow);

                        if (officeTeacher.Length == 1)
                        {
                            //strings.Add($"Кабинет: {officeTeacher[0]}");
                        }
                        else if (officeTeacher.Length == 2)
                        {
                          //  strings.Add($"Кабинет: {officeTeacher[1]}");
                        //    strings.Add($"Преподаватель: {officeTeacher[0]}");
                        }
                        else
                        {
                            //strings.Add($"Кабинет: {value}");
                        }
                    }
                }
            }

            schedule.ScheduleDepartments = scheduleDepartments;
            

            return schedule;
        }
    }
}
