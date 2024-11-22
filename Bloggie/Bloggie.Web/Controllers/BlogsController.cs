using Bloggie.Web.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Bloggie.Web.Controllers
{
    public class BlogsController : Controller
    {
        private readonly IBlogPostRepository _bpRepository;

        public BlogsController(IBlogPostRepository bpRepository)
        {
            this._bpRepository = bpRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Index(string urlHandle)
        {
            var blogPost = await _bpRepository.GetByUrlHandleAsync(urlHandle);

            return View(blogPost);
        }
    }
}
