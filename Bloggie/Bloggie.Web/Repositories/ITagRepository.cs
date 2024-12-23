﻿namespace Bloggie.Web.Repositories
{
    public interface ITagRepository
    {
        Task<IEnumerable<Bloggie.Web.Models.Domain.Tag>> GetAllAsync(int? pageSize = null,
                                                                    int? currentPage = null,
                                                                    string? searchQuery = null,
                                                                    string? sortBy = null,
                                                                    string? sortDirection = null);
        Task<Bloggie.Web.Models.Domain.Tag?> GetAsync(Guid id);
        Task<Bloggie.Web.Models.Domain.Tag> AddAsync(Bloggie.Web.Models.Domain.Tag tag);
        Task<Bloggie.Web.Models.Domain.Tag?> EditAsync(Bloggie.Web.Models.Domain.Tag tag);
        Task<Bloggie.Web.Models.Domain.Tag?> DeleteAsync(Guid id);
        Task<int> CountAsync();
    }
}
