using System.ComponentModel.DataAnnotations;

namespace PGK.WebApi.Models.Schedule
{
    public class UpdateScheduleRowModel
    {
        public string? Text { get; set; }
        [Required] public int TeacherId { get; set; }
    }
}
