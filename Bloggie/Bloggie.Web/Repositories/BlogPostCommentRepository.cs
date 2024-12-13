﻿using Bloggie.Web.Data;
using Bloggie.Web.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace Bloggie.Web.Repositories
{
    public class BlogPostCommentRepository : IBlogPostCommentRepository
    {
        private readonly BloggieDbContext _bloggieDbContext;

        public BlogPostCommentRepository(BloggieDbContext bloggieDbContext)
        {
            this._bloggieDbContext = bloggieDbContext;
        }

        public async Task<BlogPostComment> AddComment(BlogPostComment blogPostComment)
        {
            await _bloggieDbContext.BlogPostComment.AddAsync(blogPostComment);
            await _bloggieDbContext.SaveChangesAsync();

            return blogPostComment;
        }

        public async Task<IEnumerable<BlogPostComment>> RecuperaComentariosPorBlogId(Guid blogPostId)
        {
            var listaComentarios = await _bloggieDbContext.BlogPostComment
                                                .Where(l => l.BlogPostId.Equals(blogPostId))
                                                .ToListAsync();

            return listaComentarios;
        }

    }
}