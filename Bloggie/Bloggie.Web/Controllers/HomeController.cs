using Bloggie.Web.Models;
using Bloggie.Web.Models.View;
using Bloggie.Web.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Bloggie.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IBlogPostRepository _blogPostRepository;
        private readonly ITagRepository _tagRepository;

        public HomeController(ILogger<HomeController> logger,
                              IBlogPostRepository blogPostRepository,
                              ITagRepository tagRepository)
        {
            _logger = logger;
            this._blogPostRepository = blogPostRepository;
            this._tagRepository = tagRepository;
        }

        public async Task<IActionResult> Index()
        {
            //Recupera los blogs
            var blogPostsList = await _blogPostRepository.GetAllAsync();

            //Recupera todas las Tags
            var tags = await _tagRepository.GetAllAsync();

            HomeViewModel homeViewModel = new HomeViewModel();
            homeViewModel.BlogPosts = blogPostsList;
            homeViewModel.Tags = tags;

            return View(homeViewModel);
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
