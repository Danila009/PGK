using System.ComponentModel.DataAnnotations;

namespace PGK.Application.App.User.Commands.AddPhoto
{
    public class UserPhotoVm
    {
        [Required] public string Url { get; set; } = string.Empty;
    }
}
