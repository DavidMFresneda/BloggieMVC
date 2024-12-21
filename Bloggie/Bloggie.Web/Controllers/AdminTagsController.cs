using Bloggie.Web.Models.Domain;
using Bloggie.Web.Models.View;
using Bloggie.Web.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Bloggie.Web.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminTagsController : Controller
    {
        private ITagRepository _tagRepository;

        public AdminTagsController(ITagRepository tagRepository)
        {
            _tagRepository = tagRepository;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }


        [HttpGet]
        public async Task<IActionResult> List(string? searchQuery,
                                                string? sortBy,
                                                string? sortDirection,
                                                int pageSize = 3,
                                                int currentPage = 1)
        {

            var totalRecords = await _tagRepository.CountAsync();
            var totalPages = (int)Math.Ceiling((decimal)totalRecords / pageSize);



            if (sortDirection != null)
            {
                if (!sortDirection.ToUpper().Equals("ASC"))
                {
                    sortDirection = "Asc";
                }
                else
                {
                    sortDirection = "Dcs";
                }
            }

            if (sortDirection == null)
            {
                sortDirection = "Asc";
            }

            if (sortBy == null)
            {
                sortBy = "Name";
            }


            ViewBag.TotalPages = totalPages;
            ViewBag.SearchQuery = searchQuery;
            ViewBag.SortBy = sortBy;
            ViewBag.SortDirection = sortDirection;
            ViewBag.CurrentPage = currentPage;

            IEnumerable<Tag> tags = await _tagRepository.GetAllAsync(pageSize, currentPage, searchQuery, sortBy, sortDirection);

            return View(tags.ToList());
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            Tag? tag = await _tagRepository.GetAsync(id);

            if (tag != null)
            {
                EditTagRequest editTagRequest = new EditTagRequest
                {
                    Id = tag.Id,
                    Name = tag.Name,
                    DisplayName = tag.DisplayName
                };

                return View(editTagRequest);
            }
            else
            {

            }

            return View();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Edit(EditTagRequest editTag)
        {

            await _tagRepository.EditAsync(new Tag
            {
                Id = editTag.Id,
                Name = editTag.Name,
                DisplayName = editTag.DisplayName
            });

            return RedirectToAction("List");

        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Delete(Guid id)
        {

            Tag? tag = await _tagRepository.DeleteAsync(id);

            if (tag != null)
            {
                return RedirectToAction("List");
            }
            else
            {
                return RedirectToAction("Edit", new { id = id });
            }
        }


        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> SubmitTag(AddTagRequest tagRequest)
        {
            ValidateAddTagRequest(tagRequest);

            if (!ModelState.IsValid)
            {
                return View();
            }

            await _tagRepository.AddAsync(
                new Bloggie.Web.Models.Domain.Tag
                {
                    Id = Guid.NewGuid(),
                    Name = tagRequest.Name,
                    DisplayName = tagRequest.DisplayName
                }
             );

            return RedirectToAction("List");
        }

        private void ValidateAddTagRequest(AddTagRequest tagRequest)
        {
            if (tagRequest.Name.ToUpper().Equals(tagRequest.DisplayName.ToUpper()))
            {
                ModelState.AddModelError("DisplayName", "Name can't be the same as DisplayName");
            }
        }
    }
}
