using Microsoft.EntityFrameworkCore;

namespace Bloggie.Web.Data
{
    public class BloggieDbContext : DbContext
    {
        public BloggieDbContext(DbContextOptions options) : base(options)
        {


        }

        public DbSet<Models.Domain.BlogPost> BlogPosts { get; set; }
        public DbSet<Models.Domain.Tag> Tags { get; set; }

    }

}
