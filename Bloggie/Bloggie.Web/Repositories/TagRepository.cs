using Bloggie.Web.Data;
using Bloggie.Web.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace Bloggie.Web.Repositories
{
    public class TagRepository : ITagRepository
    {
        private readonly BloggieDbContext _bloggieDbContext;

        public TagRepository(BloggieDbContext context)
        {
            _bloggieDbContext = context;
        }

        public async Task<Tag> AddAsync(Bloggie.Web.Models.Domain.Tag tag)
        {
            await _bloggieDbContext.Tags.AddAsync(tag);
            await _bloggieDbContext.SaveChangesAsync();

            return tag;
        }

        public async Task<Tag?> DeleteAsync(Guid id)
        {
            Tag? tag = await _bloggieDbContext.Tags.FindAsync(id);

            if (tag != null)
            {
                _bloggieDbContext.Tags.Remove(tag);
                await _bloggieDbContext.SaveChangesAsync();
            }

            return tag;
        }

        public async Task<Tag?> EditAsync(Tag tag)
        {
            Tag? _tag = await _bloggieDbContext.Tags.FindAsync(tag.Id);

            if (_tag != null)
            {
                _tag.Name = tag.Name;
                _tag.DisplayName = tag.DisplayName;

                await _bloggieDbContext.SaveChangesAsync();

            }

            return _tag;
        }

        public async Task<IEnumerable<Tag>> GetAllAsync()
        {
            List<Tag> tags = await _bloggieDbContext.Tags.ToListAsync();

            return tags;
        }

        public async Task<Tag?> GetAsync(Guid id)
        {
            Tag? tags = await _bloggieDbContext.Tags.FindAsync(id);

            return tags;
        }
    }
}
