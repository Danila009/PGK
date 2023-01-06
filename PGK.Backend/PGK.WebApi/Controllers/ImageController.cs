using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PGK.Application.App.Image.Command.AddImage;
using PGK.Application.App.Image.Query;

namespace PGK.WebApi.Controllers
{
    public class ImageController : Controller
    {

        [HttpGet("{imageName}.{extension}")]
        public async Task<ActionResult> Get(string imageName, string extension)
        {
            var query = new GetImageQuery
            {
                ImageName = imageName,
                Extension = extension
            };

            var image = await Mediator.Send(query);

            return File(image, "image/jpeg");
        }

        [Authorize(Roles = "ADMIN")]
        [HttpPost]
        public async Task<ActionResult> Post(IFormFile image)
        {
            var fileName = image.FileName.Replace(Path.GetExtension(image.FileName), "");
            var extension = Path.GetExtension(image.FileName).Replace(".", "");

            var command = new AddImageCommand
            {
                Image = image,
                ImageName = fileName,
                Extension = extension
            };

            await Mediator.Send(command);

            return Ok();
        }
    }
}
