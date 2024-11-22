namespace Bloggie.Web.Repositories
{
    public interface IBlogPostRepository
    {

        Task<IEnumerable<Bloggie.Web.Models.Domain.BlogPost>> GetAllAsync();
        Task<Bloggie.Web.Models.Domain.BlogPost?> GetAsync(Guid id);
        Task<Bloggie.Web.Models.Domain.BlogPost?> GetByUrlHandleAsync(string urlHandle);
        Task<Bloggie.Web.Models.Domain.BlogPost> AddAsync(Bloggie.Web.Models.Domain.BlogPost blogPost);
        Task<Bloggie.Web.Models.Domain.BlogPost?> EditAsync(Bloggie.Web.Models.Domain.BlogPost blogPost);
        Task<Bloggie.Web.Models.Domain.BlogPost?> DeleteAsync(Guid id);

    }
}
