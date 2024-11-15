using Bloggie.Web.Models;
using Bloggie.Web.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Bloggie.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IBlogPostRepository _blogPostRepository;

        public HomeController(ILogger<HomeController> logger, IBlogPostRepository blogPostRepository)
        {
            _logger = logger;
            this._blogPostRepository = blogPostRepository;
        }

        public async Task<IActionResult> Index()
        {
            //Recupera los blogs
            var blogPostsList = await _blogPostRepository.GetAllAsync();

            return View(blogPostsList);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
