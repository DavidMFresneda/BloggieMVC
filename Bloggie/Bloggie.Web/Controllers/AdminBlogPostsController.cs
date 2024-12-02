using Bloggie.Web.Models.Domain;
using Bloggie.Web.Models.View;
using Bloggie.Web.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Bloggie.Web.Controllers
{

    [Authorize(Roles = "Admin")]
    public class AdminBlogPostsController : Controller
    {
        private readonly ITagRepository _tagRepository;
        private readonly IBlogPostRepository _blogPostRepository;

        public AdminBlogPostsController(ITagRepository tagRepository,
                                        IBlogPostRepository blogPostRepository)
        {
            this._tagRepository = tagRepository;
            this._blogPostRepository = blogPostRepository;
        }


        [HttpGet]
        public async Task<IActionResult> Add()
        {
            //Get Tags From Database
            var tags = await _tagRepository.GetAllAsync();

            AddBlogPostRequest addBlogPostRequest = new AddBlogPostRequest
            {
                Tags = tags.Select(t => new SelectListItem
                {
                    Text = t.Name,
                    Value = t.Id.ToString()
                })
            };

            return View(addBlogPostRequest);
        }


        [HttpPost]
        public async Task<IActionResult> Add(AddBlogPostRequest blogPostRequest)
        {
            ICollection<Tag> tags = new List<Tag>();

            if (blogPostRequest.SelectedTags != null)
            {
                foreach (var tag in blogPostRequest.SelectedTags)
                {
                    var tagRepository = await _tagRepository.GetAsync(new Guid(tag));

                    if (tagRepository != null)
                        tags.Add(tagRepository);
                }
            }


            await _blogPostRepository.AddAsync(
                new Models.Domain.BlogPost
                {
                    Author = blogPostRequest.Author,
                    Content = blogPostRequest.Content,
                    Heading = blogPostRequest.Heading,
                    PageTitle = blogPostRequest.PageTitle,
                    PublishedDate = blogPostRequest.PublishedDate,
                    ShortDescription = blogPostRequest.ShortDescription,
                    Tags = tags,
                    UrlHandle = blogPostRequest.UrlHandle,
                    Visible = blogPostRequest.Visible,
                    FeaturedImageUrl = blogPostRequest.FeaturedImageUrl

                });

            return RedirectToAction("List");
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
            var listadoBlogs = await _blogPostRepository.GetAllAsync();

            return View(listadoBlogs);
        }


        [HttpGet]
        public async Task<ActionResult> Edit(Guid id)
        {
            var blogPost = await _blogPostRepository.GetAsync(id);
            var tags = await _tagRepository.GetAllAsync();
            List<SelectListItem> listadoTags = new List<SelectListItem>();


            foreach (var tag in tags)
            {
                listadoTags.Add(new SelectListItem
                {
                    Value = tag.Id.ToString(),
                    Text = tag.Name
                });
            }

            EditBlogPostRequest editBlogPostRequest = new EditBlogPostRequest
            {
                Id = blogPost.Id,
                Author = blogPost.Author,
                Content = blogPost.Content,
                Heading = blogPost.Heading,
                PageTitle = blogPost.PageTitle,
                PublishedDate = blogPost.PublishedDate,
                ShortDescription = blogPost.ShortDescription,
                UrlHandle = blogPost.UrlHandle,
                Visible = blogPost.Visible,
                FeaturedImageUrl = blogPost.FeaturedImageUrl,
                Tags = listadoTags,
                SelectedTags = blogPost.Tags.Select(t => t.Id.ToString()).ToArray()
            };

            return View(editBlogPostRequest);
        }


        [HttpPost]
        public async Task<ActionResult> Edit(EditBlogPostRequest editBlogPostRequest)
        {
            BlogPost blogPost = await _blogPostRepository.GetAsync(editBlogPostRequest.Id);

            blogPost.Id = editBlogPostRequest.Id;
            blogPost.Author = editBlogPostRequest.Author;
            blogPost.Content = editBlogPostRequest.Content;
            blogPost.Heading = editBlogPostRequest.Heading;
            blogPost.PageTitle = editBlogPostRequest.PageTitle;
            blogPost.PublishedDate = editBlogPostRequest.PublishedDate;
            blogPost.ShortDescription = editBlogPostRequest.ShortDescription;
            blogPost.UrlHandle = editBlogPostRequest.UrlHandle;
            blogPost.Visible = editBlogPostRequest.Visible;
            blogPost.FeaturedImageUrl = editBlogPostRequest.FeaturedImageUrl;

            foreach (var tag in editBlogPostRequest.SelectedTags)
            {
                var _tag = await _tagRepository.GetAsync(new Guid(tag));

                if (!blogPost.Tags.Contains(_tag))
                {
                    blogPost.Tags.Add(_tag);
                }
            }


            await _blogPostRepository.EditAsync(blogPost);


            return RedirectToAction("List");
        }


        [HttpPost]
        public async Task<ActionResult> Delete(Guid id)
        {
            await _blogPostRepository.DeleteAsync(id);

            return RedirectToAction("List");

        }

    }

}