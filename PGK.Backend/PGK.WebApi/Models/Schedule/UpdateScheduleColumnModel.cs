using System.ComponentModel.DataAnnotations;

namespace PGK.WebApi.Models.Schedule
{
    public class UpdateScheduleColumnModel
    {
        public string? Text { get; set; }
        [Required] public string Time { get; set; } = string.Empty;
        [Required] public int GroupId { get; set; }
    }
}
