using Microsoft.AspNetCore.Mvc;

namespace Bloggie.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {

        [HttpPost]
        public async Task<IActionResult> UploadImageAsync(IFormFile file)
        {
            return Ok();
        }

    }
}
