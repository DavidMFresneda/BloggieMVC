using Bloggie.Web.Models.View;
using Bloggie.Web.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Bloggie.Web.Controllers
{
    public class BlogsController : Controller
    {
        private readonly IBlogPostRepository _bpRepository;
        private readonly IBlogPostLikeRepository _bpLikeRepository;

        public BlogsController(IBlogPostRepository bpRepository,
                               IBlogPostLikeRepository bpLikeRepository)
        {
            this._bpRepository = bpRepository;
            this._bpLikeRepository = bpLikeRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Index(string urlHandle)
        {
            var blogPost = await _bpRepository.GetByUrlHandleAsync(urlHandle);
            int numLikes = 0;

            if (blogPost != null)
            {
                numLikes = await _bpLikeRepository.GetLikesCount(blogPost.Id);
            }

            BlogDetailsViewModel blogDetailsViewModel = new BlogDetailsViewModel
            {
                BlogPost = blogPost,
                NumLikes = numLikes
            };

            return View(blogDetailsViewModel);
        }
    }
}
