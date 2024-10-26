using Bloggie.Web.Data;
using Bloggie.Web.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace Bloggie.Web.Repositories
{
    public class BlogPostRepository : IBlogPostRepository
    {
        private readonly BloggieDbContext _blogPostDbContext;

        public BlogPostRepository(BloggieDbContext blogPostDbContext)
        {
            this._blogPostDbContext = blogPostDbContext;
        }

        public async Task<BlogPost> AddAsync(BlogPost blogPost)
        {

            await _blogPostDbContext.AddAsync(blogPost);

            await _blogPostDbContext.SaveChangesAsync();

            return blogPost;

        }

        public Task<BlogPost?> DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<BlogPost?> EditAsync(BlogPost blogPost)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<BlogPost>> GetAllAsync()
        {
            var blogs = await _blogPostDbContext.BlogPosts.Include("Tags").ToListAsync();

            return blogs;
        }

        public async Task<BlogPost?> GetAsync(Guid id)
        {
            var blog = await _blogPostDbContext.BlogPosts.Include("Tags").FirstOrDefaultAsync(t => t.Id.Equals(id));

            return blog;
        }
    }
}
