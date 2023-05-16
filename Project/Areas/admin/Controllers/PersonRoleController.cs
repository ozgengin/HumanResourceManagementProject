using Infrastructure.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace Project.Areas.admin.Controllers
{
    [Area("admin")]
    [Authorize(Roles = "admin")]
    public class PersonRoleController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public PersonRoleController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }
        // GET: KisiRolController
        public async Task<ActionResult> Index() // Sistemde kayıtlı olan kişileri getir. Anlık kullancı hariç
        {
            var uıd = _userManager.GetUserId(HttpContext.User);

            var us = await _userManager.FindByIdAsync(uıd);
            if (us.HasLogin == false)
            {
                return RedirectToAction("LogIn", "Login", new { area = "Login" });
            }
            // anlık kullanıcıyı getir
            var anlıkKullaniciId = _userManager.GetUserId(HttpContext.User);

            // kendisi hariç diğer kullanıcıları getir
            var kendisiHaricKullanicilar = _userManager.Users.Where(u => u.Id != anlıkKullaniciId).ToList();

            return View(kendisiHaricKullanicilar);
        }


        // GET: KisiRolController/Create
        public async Task<IActionResult> RolEkle(string id)
        {
            var uıd = _userManager.GetUserId(HttpContext.User);

            var us = await _userManager.FindByIdAsync(uıd);
            if (us.HasLogin == false)
            {
                return RedirectToAction("LogIn", "Login", new { area = "Login" });
            }
            // Önce sayfadan gönderilen id'ye ait olan kullanıcıyı getir.
            //öğrenci yetkisi verilecek olan kullanıcıyı
            var yetkiVerilecekKullanici = await _userManager.FindByIdAsync(id);

            // Daha sonra bu kullanıcıya öğrenci rolünü EKLE.
            await _userManager.AddToRoleAsync(yetkiVerilecekKullanici, "Yönetici");
            return RedirectToAction("Index");
        }


        // GET: KisiRolController/Delete/5
        public async Task<ActionResult> RolSil(string id)
        {
            var uıd = _userManager.GetUserId(HttpContext.User);

            var us = await _userManager.FindByIdAsync(uıd);
            if (us.HasLogin == false)
            {
                return RedirectToAction("LogIn", "Login", new { area = "Login" });
            }
            // Önce sayfadan gönderilen id'ye ait olan kullanıcıyı getir.
            //öğrenci yetkisi verilecek olan kullanıcıyı
            var yetkiVerilecekKullanici = await _userManager.FindByIdAsync(id);

            // Daha sonra bu kullanıcıya öğrenci rolünü EKLE.
            await _userManager.RemoveFromRoleAsync(yetkiVerilecekKullanici, "Yönetici");
            return RedirectToAction("Index");

        }
    }
}

