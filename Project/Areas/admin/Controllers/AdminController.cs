using Infrastructure.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Project.Interfaces;
using Project.Models;
using System;
using System.Security.Claims;

namespace Project.Areas.admin.Controllers
{
    [Authorize(Roles =("admin"))]
    [Area("admin")]
    public class AdminController : Controller
    {
        private readonly IAdminViewModelService _adminViewModelService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IWebHostEnvironment _env;

        public AdminController(ILogger<AdminController> logger, IAdminViewModelService adminViewModelService,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager, IWebHostEnvironment env)
        {
            _adminViewModelService = adminViewModelService;
            _userManager = userManager;
            _signInManager = signInManager;
            _env = env;
        }
        public async Task<IActionResult> Index()
        {
            var uıd = _userManager.GetUserId(HttpContext.User);

            var us = await _userManager.FindByIdAsync(uıd);
            if (us.HasLogin == false)
            {
                return RedirectToAction("LogIn", "Login", new { area = "Login" });
            }
            if (_signInManager.IsSignedIn(User))
            {
                var userId = _userManager.GetUserId(HttpContext.User);

                var user = await _userManager.FindByIdAsync(userId);
                var vm = await _adminViewModelService.GetAdminViewModelAsync(user.Id);
                if (user != null)
                {

                    return View(vm);
                }
            }

            return View();
        }

        public async Task<IActionResult> Details()
        {
            var uıd = _userManager.GetUserId(HttpContext.User);

            var us = await _userManager.FindByIdAsync(uıd);
            if (us.HasLogin == false)
            {
                return RedirectToAction("LogIn", "Login", new { area = "Login" });
            }
            if (_signInManager.IsSignedIn(User))
            {
                var userId = _userManager.GetUserId(HttpContext.User);

                var user = await _userManager.FindByIdAsync(userId);
                var vm = await _adminViewModelService.GetAdminViewModelAsync(user.Id);
                if (user != null)
                {

                    return View(vm);
                }
            }

            return View();

        }

        public async Task<IActionResult> Edit()
        {
            var uıd = _userManager.GetUserId(HttpContext.User);

            var us = await _userManager.FindByIdAsync(uıd);
            if (us.HasLogin == false)
            {
                return RedirectToAction("LogIn", "Login", new { area = "Login" });
            }
            if (_signInManager.IsSignedIn(User))
            {

                var userId = _userManager.GetUserId(HttpContext.User);
                var user = await _userManager.FindByIdAsync(userId);
                var evm = new EditViewModel();
                if (user != null)
                {
                    evm.Address = user.Address;
                    evm.Phone = user.Phone;
                    return View(evm);
                }
            }

            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Edit(EditViewModel vm)
        {

            if (_signInManager.IsSignedIn(User))
            {
                var userId = _userManager.GetUserId(HttpContext.User);

                var user = await _userManager.FindByIdAsync(userId);

                if (user != null && ModelState.IsValid)
                {

                    user.Address = vm.Address;
                    user.Phone = vm.Phone;

                    if (vm.Image != null)
                    {
                        string dosyaAd = Guid.NewGuid().ToString() + Path.GetExtension(vm.Image.FileName);
                        string kayitYolu = Path.Combine(_env.WebRootPath, "img", dosyaAd);
                        using (var stream = new FileStream(kayitYolu, FileMode.Create))
                        {
                            vm.Image.CopyTo(stream);
                        }

                        user.ImageName = dosyaAd;
                    }

                    await _userManager.UpdateAsync(user);
                    await _signInManager.RefreshSignInAsync(user);
                    return RedirectToAction(nameof(Index));

                }
            }
            return View(vm);

           
        }
    }
}

