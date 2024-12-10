using Bloggie.Web.Models.Domain;
using Bloggie.Web.Models.View;
using Bloggie.Web.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Bloggie.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogPostLikeController : ControllerBase
    {
        private readonly IBlogPostLikeRepository _blogPostLikeRepository;

        public BlogPostLikeController(IBlogPostLikeRepository blogPostLikeRepository)
        {
            this._blogPostLikeRepository = blogPostLikeRepository;
        }

        [Route("Add")]
        [HttpPost]
        public async Task<IActionResult> AddLike([FromBody] AddLikeRequest addLikeRequest)
        {
            _blogPostLikeRepository.AddLike(new BlogPostLike
            {
                BlogPostId = addLikeRequest.BlogPostId,
                UserId = addLikeRequest.UserId
            });

            return Ok();

        }

        [Route("NumLikes/{blogPostId:Guid}")]
        [HttpGet]
        public async Task<IActionResult> NumLikes(Guid blogPostId)
        {

            int totalLikes = await _blogPostLikeRepository.GetLikesCount(blogPostId);

            return Ok(totalLikes);
        }

    }
}
