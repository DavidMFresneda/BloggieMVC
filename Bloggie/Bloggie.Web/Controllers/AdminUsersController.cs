using Bloggie.Web.Models.View;
using Bloggie.Web.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Bloggie.Web.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminUsersController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly UserManager<IdentityUser> _userManager;

        public AdminUsersController(IUserRepository userRepository,
                                    UserManager<IdentityUser> userManager)
        {
            this._userRepository = userRepository;
            this._userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
            var users = await _userRepository.GetAll();
            UserViewModel userViewModel = new UserViewModel();
            userViewModel.Users = new List<User>();

            foreach (var user in users)
            {
                userViewModel
                    .Users
                    .Add(
                    new User
                    {
                        Id = new Guid(user.Id),
                        Username = user.UserName,
                        EmailAddress = user.Email
                    });
            }

            return View(userViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> List(UserViewModel userViewModel)
        {
            IdentityUser identityUser = new IdentityUser
            {
                Email = userViewModel.Email,
                UserName = userViewModel.Username,
                EmailConfirmed = true
            };

            var identityResult = await _userManager.CreateAsync(identityUser, userViewModel.Password);

            if (identityResult.Succeeded)
            {
                var roles = new List<string>();
                roles.Add("User");


                if (userViewModel.AdminRoleCheck)
                {
                    roles.Add("Admin");

                }

                identityResult = await _userManager.AddToRolesAsync(identityUser, roles);
            }

            if (identityResult.Succeeded)
            {
                return RedirectToAction("List", "AdminUsers");
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user != null)
            {
                var result = await _userManager.DeleteAsync(user);
                if (result.Succeeded)
                {
                    return RedirectToAction("List", "AdminUsers");
                }
            }
            return View();
        }

    }
}
