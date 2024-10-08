using Bloggie.Web.Data;
using Bloggie.Web.Models.Domain;
using Bloggie.Web.Models.View;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Bloggie.Web.Controllers
{
    public class AdminTagsController : Controller
    {
        private BloggieDbContext _bloggieDbContext;

        public AdminTagsController(BloggieDbContext contexto)
        {
            _bloggieDbContext = contexto;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
            List<Tag> tags = await _bloggieDbContext.Tags.ToListAsync();

            return View(tags);
        }

        //AdminTags/Add
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            Tag tag = await _bloggieDbContext.Tags.FindAsync(id);

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

        [HttpPost]
        public IActionResult Edit(EditTagRequest editTag)
        {
            Tag tag = _bloggieDbContext.Tags.Find(editTag.Id);

            if (tag != null)
            {
                tag.Name = editTag.Name;
                tag.DisplayName = editTag.DisplayName;

                _bloggieDbContext.SaveChanges();

                return RedirectToAction("List");
            }

            //Show failure notification
            return RedirectToAction("Edit", new { id = editTag.Id });


        }

        [HttpPost]
        public IActionResult Delete(Guid id)
        {
            Tag tag = _bloggieDbContext.Tags.Find(id);

            _bloggieDbContext.Tags.Remove(tag);
            _bloggieDbContext.SaveChanges();

            return RedirectToAction("List");
        }

        //[HttpDelete]
        //[Route("AdminTags/Delete/{id}")]
        //public IActionResult Delete(Guid id)
        //{
        //    // Your delete logic here
        //    return Ok();
        //}



        [HttpPost]
        public async Task<IActionResult> SubmitTag(AddTagRequest tagRequest)
        {
            await _bloggieDbContext.Tags.AddAsync(
                new Models.Domain.Tag
                {
                    Id = Guid.NewGuid(),
                    Name = tagRequest.Name,
                    DisplayName = tagRequest.DisplayName
                }
             );

            await _bloggieDbContext.SaveChangesAsync();

            //return View("Add");
            return RedirectToAction("List");
        }

    }
}
