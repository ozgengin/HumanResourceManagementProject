using Infrastructure.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Project.Areas.admin.Controllers;
using Project.Interfaces;
using Project.Models;
using SendGrid.Helpers.Mail;
using SendGrid;

using Infrastructure.Data;
using ApplicationCore.Entities;
using Microsoft.EntityFrameworkCore;

namespace Project.Areas.PasswordChange.Controllers
{

    [Area("PasswordChange")]
    public class PasswordChangeController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IWebHostEnvironment _env;
        private readonly HumanResourceContext _db;
        private string code;

        public PasswordChangeController(UserManager<ApplicationUser> userManager,
          SignInManager<ApplicationUser> signInManager, IWebHostEnvironment env, HumanResourceContext db)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _env = env;
            _db = db;
        }
        [Authorize]
        public async Task<IActionResult> Index()
        {
            return View();
        }
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(PasswordChangeViewModel passwordVM)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var userId = _userManager.GetUserId(HttpContext.User);
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            var changePasswordResult = await _userManager.ChangePasswordAsync(user, passwordVM.OldPassword, passwordVM.NewPassword);
            if (!changePasswordResult.Succeeded)
            {
                foreach (var error in changePasswordResult.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
                return View();
            }

            user.HasLogin = true;
            await _userManager.UpdateAsync(user);
            await _signInManager.RefreshSignInAsync(user);

            return RedirectToAction("LogIn", "Login", new { area = "Login" });
        }
        public async Task<IActionResult> Forgot()
        {

            return View();
        }
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Forgot(string eMail)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(eMail);
                if (user == null) { return NotFound(); }

                var code = getCode();
                var pasCode = new PasswordCode()
                {
                    Code = code,
                    EmployeeId = user.Id,
                };
                await _db.PasswordCodes.AddAsync(pasCode);
                await _db.SaveChangesAsync();
                var apiKey = "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";
                var client = new SendGridClient(apiKey);
                var from = new EmailAddress("hrplatformproject@gmail.com", "HR Platform Manager");
                var subject = "HR Platform";
                var to = new EmailAddress(user.Mail, "New Employee");
                var text = "<h1>Sıfırlama için kodunuz : </h1>";
                var htmlContent = "şifreniz:" + code;
                var msg = MailHelper.CreateSingleEmail(from, to, subject, text, htmlContent);
                var response = await client.SendEmailAsync(msg);

                return RedirectToAction("PasswordCode");

            }
            return View();
        }
        public async Task<IActionResult> PasswordCode()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> PasswordCode(string code, string newPassword)
        {
            var userId = _db.PasswordCodes.FirstOrDefault(x => x.Code == code)!.EmployeeId.ToString();
            var passCode = _db.PasswordCodes.FirstOrDefault(x => x.Code == code);
            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Id == userId);
            if (user == null) { return NotFound(); }

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var result = await _userManager.ResetPasswordAsync(user, token, newPassword);
            _db.PasswordCodes.Remove(passCode);
            await _db.SaveChangesAsync();
            await _userManager.UpdateAsync(user);
            if (!result.Succeeded)
            {
                // Şifre değiştirme başarısız oldu
                return BadRequest(result.Errors);
            }
            return RedirectToAction("LogIn", "Login", new { area = "Login" });
        }
        public string getCode()
        {
            if (code == null)
            {
                Random rnd = new Random();
                code = "";
                for (int i = 0; i < 6; i++)
                {
                    char tmp = Convert.ToChar(rnd.Next(48, 58));
                    code += tmp;
                }

            }
            return code;
        }
    }
}