using Bloggie.Web.Models.Domain;

namespace Bloggie.Web.Repositories
{
    public interface IBlogPostCommentRepository
    {

        public Task<BlogPostComment> AddComment(BlogPostComment blogPostComment);
        public Task<IEnumerable<BlogPostComment>> RecuperaComentariosPorBlogId(Guid blogPostId);

    }
}
