using Bloggie.Web.Models.Domain;

namespace Bloggie.Web.Repositories
{
    public interface IBlogPostLikeRepository
    {

        Task<int> GetLikesCount(Guid blogPostId);

        Task<BlogPostLike> AddLike(BlogPostLike blogPostLike);

    }
}
