using Bloggie.Web.Models.View;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Bloggie.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public AccountController(UserManager<IdentityUser> userManager,
                                 SignInManager<IdentityUser> signInManager)
        {
            this._userManager = userManager;
            this._signInManager = signInManager;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(AccountRegisterPostRequest registerRequest)
        {

            if (!ModelState.IsValid)
            {
                return View();
            }

            var identityUser = new IdentityUser
            {
                UserName = registerRequest.UserName,
                Email = registerRequest.Email

            };
            identityUser.EmailConfirmed = true;
            identityUser.NormalizedEmail = registerRequest.Email.ToUpper();
            identityUser.NormalizedUserName = registerRequest.UserName.ToUpper();


            var identityResult = await _userManager.CreateAsync(identityUser, registerRequest.Password);

            if (identityResult.Succeeded)
            {
                var roleIdentityResult = await _userManager.AddToRoleAsync(identityUser, "User");

                if (roleIdentityResult.Succeeded)
                {
                    //Show success notification
                    return RedirectToAction("Register");
                }
            }

            return View();
        }

        [HttpGet]
        public IActionResult Login(string ReturnUrl)
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(Models.View.LoginRequest loginRequest)
        {
            if (!ModelState.IsValid)
                return View();

            var user = await _userManager.FindByNameAsync(loginRequest.UserName);
            if (user == null)
            {
                // User does not exist
                return View();
            }

            if (_userManager.Options.SignIn.RequireConfirmedEmail && !await _userManager.IsEmailConfirmedAsync(user))
            {
                // Email not confirmed
                return View();
            }

            if (await _userManager.IsLockedOutAsync(user))
            {
                // Account is locked out
                return View();
            }

            var signInResult = await _signInManager.PasswordSignInAsync(loginRequest.UserName,
                                                                        loginRequest.Password,
                                                                        false, false);

            if (signInResult.Succeeded)
            {

                if (!string.IsNullOrEmpty(loginRequest.ReturnUrl))
                {
                    return Redirect(loginRequest.ReturnUrl);
                }

                // Show success notification
                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult AccessDenied()
        {

            return View();

        }

    }
}
