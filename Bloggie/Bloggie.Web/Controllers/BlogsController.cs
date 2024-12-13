using Bloggie.Web.Models.Domain;
using Bloggie.Web.Models.View;
using Bloggie.Web.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;


namespace Bloggie.Web.Controllers
{
    public class BlogsController : Controller
    {
        private readonly IBlogPostRepository _bpRepository;
        private readonly IBlogPostLikeRepository _bpLikeRepository;
        private readonly IBlogPostCommentRepository _bpCommentRepository;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public BlogsController(IBlogPostRepository bpRepository,
                               IBlogPostLikeRepository bpLikeRepository,
                               IBlogPostCommentRepository bpCommentRepository,
                               UserManager<IdentityUser> userManager,
                               SignInManager<IdentityUser> signInManager)
        {
            this._bpRepository = bpRepository;
            this._bpLikeRepository = bpLikeRepository;
            this._bpCommentRepository = bpCommentRepository;
            this._userManager = userManager;
            this._signInManager = signInManager;
        }

        [HttpGet]
        public async Task<IActionResult> Index(string urlHandle)
        {
            var blogPost = await _bpRepository.GetByUrlHandleAsync(urlHandle);
            int numLikes = 0;
            var dbComentarios = await _bpCommentRepository.RecuperaComentariosPorBlogId(blogPost.Id);
            List<BlogComment> comentarios = new List<BlogComment>();

            foreach (BlogPostComment blogPostComment in dbComentarios)
            {
                comentarios.Add(new BlogComment
                {
                    DateAdded = blogPostComment.DateAdded,
                    Description = blogPostComment.Description,
                    Username = (await _userManager.FindByIdAsync(blogPostComment.UserId.ToString())).UserName
                });
            }

            if (blogPost != null)
            {
                numLikes = await _bpLikeRepository.GetLikesCount(blogPost.Id);
            }

            BlogDetailsViewModel blogDetailsViewModel = new BlogDetailsViewModel
            {
                BlogPost = blogPost,
                NumLikes = numLikes,
                Comentarios = comentarios
            };

            return View(blogDetailsViewModel);
        }


        [HttpPost]
        public async Task<IActionResult> Index(BlogDetailsViewModel blogDetailsViewModel, [FromForm] string _blogPostId)
        {
            await _bpCommentRepository.AddComment(new BlogPostComment
            {
                Id = Guid.NewGuid(),
                BlogPostId = new Guid(_blogPostId),
                Description = blogDetailsViewModel.CommentDescription,
                DateAdded = DateTime.Now,
                UserId = blogDetailsViewModel.UserId
            });

            return RedirectToAction("Index", "Blogs", new { urlHandle = Request.Form["_urlHandle"] });
        }


    }
}
