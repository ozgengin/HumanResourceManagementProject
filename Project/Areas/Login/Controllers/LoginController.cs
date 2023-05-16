using Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Project.Areas.admin.Controllers;
using Project.Areas.Login.Models;
using Project.Interfaces;

namespace Project.Areas.Login.Controllers
{
    [Area("Login")]
    public class LoginController : Controller
    {
        private readonly IAdminViewModelService _adminViewModelService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IWebHostEnvironment _env;

        public LoginController(ILogger<AdminController> logger, IAdminViewModelService adminViewModelService,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager, IWebHostEnvironment env)
        {
            _adminViewModelService = adminViewModelService;
            _userManager = userManager;
            _signInManager = signInManager;
            _env = env;
        }
        public async Task<IActionResult> SignOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("LogIn");

        }
        [HttpGet]
        public async Task<IActionResult> LogIn()
        {

            ViewBag.ErrorMessage = null; // Set error message to null when form is first loaded
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> LogIn(LoginViewModel lgnVm)
        {
            if (string.IsNullOrEmpty(lgnVm.Mail) || string.IsNullOrEmpty(lgnVm.Password))
            {
                ViewBag.ErrorMessage = "Please provide both email and password.";
                return View();
            }

            var user = await _userManager.FindByEmailAsync(lgnVm.Mail);
            if (user != null)
            {
                var signInResult = await _signInManager.PasswordSignInAsync(user.UserName, lgnVm.Password, isPersistent: false, lockoutOnFailure: false);

                await _signInManager.RefreshSignInAsync(user);
                var roles = await _userManager.GetRolesAsync(user);

                if (signInResult.Succeeded)
                {
                    if (user.HasLogin == false)
                    {
                        return RedirectToAction("Index", "PasswordChange", new { area = "PasswordChange" });
                    }
                    if (roles.Contains("admin"))
                    {
                        return RedirectToAction("Index", "Admin", new { area = "admin" });
                    }
                    else if (roles.Contains("Worker"))
                    {
                        return RedirectToAction("Index", "Worker", new { area = "Worker" });
                    }
                    else if (roles.Contains("Manager"))
                    {
                        return RedirectToAction("Index", "CoExecutive", new { area = "CompanyExecutive" });
                    }
                }
            }

            else if (user == null)
            {
                //ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                ViewBag.ErrorMessage = "Invalid login attempt.";
                return View();
            }
            return View();

        }
    }
}