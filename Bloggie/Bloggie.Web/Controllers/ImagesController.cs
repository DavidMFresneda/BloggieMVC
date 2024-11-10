using Bloggie.Web.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Bloggie.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private readonly IImageRepository _iRepository;

        public ImagesController(IImageRepository iRepository)
        {
            this._iRepository = iRepository;
        }

        [HttpPost]
        public async Task<IActionResult> UploadImageAsync(IFormFile file)
        {
            var result = await _iRepository.UploadImageAsync(file);

            if (result == null)
            {
                return Problem("Error!", null, (int)HttpStatusCode.InternalServerError);
            }

            return new JsonResult(result);
        }


    }
}
