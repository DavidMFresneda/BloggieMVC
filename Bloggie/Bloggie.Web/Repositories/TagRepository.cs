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

        public async Task<IEnumerable<Tag>> GetAllAsync(int? pageSize,
                                                        int? currentPage,
                                                        string? searchQuery,
                                                        string? sortBy,
                                                        string? sortDirection
                                                        )
        {
            var query = _bloggieDbContext.Tags.AsQueryable();

            //filter by search query
            if (!(searchQuery == null || string.IsNullOrEmpty(searchQuery)))
                query = query.Where(t => t.Name.Contains(searchQuery) || t.DisplayName
                            .Contains(searchQuery)).AsQueryable();

            //sorting
            if (sortBy != null && sortDirection != null)
            {
                if (sortDirection.ToUpper() == "ASC")
                {
                    if (sortBy.ToUpper().Equals("NAME"))
                        query = query.OrderBy(columna => columna.Name).AsQueryable();
                    else
                        query = query.OrderBy(columna => columna.DisplayName).AsQueryable();
                }
                else
                {
                    if (sortBy.ToUpper().Equals("NAME"))
                        query = query.OrderByDescending(columna => columna.Name).AsQueryable();
                    else
                        query = query.OrderByDescending(columna => columna.DisplayName).AsQueryable();
                }
            }

            //pagination
            if (currentPage != null && pageSize != null)
            {
                query = query.Skip((currentPage.Value - 1) * pageSize.Value)
                         .Take(pageSize.Value).AsQueryable();
            }


            return await query.ToListAsync();
        }

        public async Task<Tag?> GetAsync(Guid id)
        {
            Tag? tags = await _bloggieDbContext.Tags.FindAsync(id);

            return tags;
        }

        public async Task<int> CountAsync()
        {
            return await _bloggieDbContext.Tags.CountAsync();
        }

    }
}
