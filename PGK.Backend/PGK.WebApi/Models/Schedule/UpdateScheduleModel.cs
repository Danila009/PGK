using System.ComponentModel.DataAnnotations;

namespace PGK.WebApi.Models.Schedule
{
    public class UpdateScheduleModel
    {
        [Required] public DateTime Date { get; set; }
    }
}
