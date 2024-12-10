
using Bloggie.Web.Data;
using Bloggie.Web.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace Bloggie.Web.Repositories
{
    public class BlogPostLikeRepository : IBlogPostLikeRepository
    {
        private readonly BloggieDbContext _bloggieDbContext;

        public BlogPostLikeRepository(BloggieDbContext bloggieDbContext)
        {
            _bloggieDbContext = bloggieDbContext;
        }

        public async Task<int> GetLikesCount(Guid blogPostId)
        {
            int likes = await _bloggieDbContext.BlogPostLike
                                    .Where(bpl => bpl.BlogPostId == blogPostId)
                                    .CountAsync();

            return likes;
        }

        public async Task<BlogPostLike> AddLike(BlogPostLike blogPostLike)
        {

            //Miramos si aun no hay likes de este usuario
            var numLikes = _bloggieDbContext.BlogPostLike.Where(l => l.UserId.Equals(blogPostLike.UserId)
            && l.BlogPostId.Equals(blogPostLike.BlogPostId)).Count();

            if (numLikes < 1)
            {
                var result = await _bloggieDbContext.BlogPostLike.AddAsync(blogPostLike);
                await _bloggieDbContext.SaveChangesAsync();
            }

            return blogPostLike;
        }

    }
}
