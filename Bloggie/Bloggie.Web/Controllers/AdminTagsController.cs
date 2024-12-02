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
        public async Task<IActionResult> List()
        {
            IEnumerable<Tag> tags = await _tagRepository.GetAllAsync();

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



    }
}
