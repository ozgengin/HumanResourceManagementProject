using Infrastructure.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Project.Areas.admin.Controllers
{
    [Area("admin")]
    [Authorize(Roles = "admin")]

    public class RoleController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public RoleController(RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _signInManager = signInManager;
        }
        // GET: RolController
        public async Task<ActionResult> Index() // Rollerin listeleneceği bölüm
        {
            var uıd = _userManager.GetUserId(HttpContext.User);

            var us = await _userManager.FindByIdAsync(uıd);
            if (us.HasLogin == false)
            {
                return RedirectToAction("LogIn", "Login", new { area = "Login" });
            }
            var butunRoller = await _roleManager.Roles.ToListAsync();
            return View(butunRoller);
        }

        // GET: RolController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: RolController/Create
        public async Task<ActionResult> Create()
        {
            var uıd = _userManager.GetUserId(HttpContext.User);

            var us = await _userManager.FindByIdAsync(uıd);
            if (us.HasLogin == false)
            {
                return RedirectToAction("LogIn", "Login", new { area = "Login" });
            }
            return View();
        }

        // POST: RolController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(IdentityRole rol)
        {

            try
            {
                //formdan gelen rolVM'in adı bizim ekleyeceğimiz HAKİKİ rolün adıdır
                // İşte bu adı rol adını role manager'i kullanarak ekleyeceğiz...

                // önce yeni bir rol oluşturalım.
                var yeniRol = new IdentityRole();

                // Sonra rolümüzün adını verelim...
                yeniRol.Name = rol.Name;

                // şimdi de rolü db'ye ekleyelim.
                await _roleManager.CreateAsync(yeniRol);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                // Hata olursa direk kendi view'ına dön..
                return View();
            }
        }

        // GET: RolController/Edit/5
        public async Task<ActionResult> Edit(string id) // Gönderilen ID'ye ait olan rolün adının yazılı olduğu forma git.
        {
            var uıd = _userManager.GetUserId(HttpContext.User);

            var us = await _userManager.FindByIdAsync(uıd);
            if (us.HasLogin == false)
            {
                return RedirectToAction("LogIn", "Login", new { area = "Login" });
            }
            // rolü getir.
            var guncellenecekRol = await _roleManager.FindByIdAsync(id);

            // bu formu güncelleme formuna gönder
            return View(guncellenecekRol);
        }

        // POST: RolController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(IdentityRole rol)
        {
            try
            {
                // Formdan post edilen rölü güncelle
                var rol2 = await _roleManager.FindByIdAsync(rol.Id);
                rol2.Name = rol.Name;
                await _roleManager.UpdateAsync(rol2);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: RolController/Delete/5
        public async Task<ActionResult> Delete(string id)
        {
            var uıd = _userManager.GetUserId(HttpContext.User);

            var us = await _userManager.FindByIdAsync(uıd);
            if (us.HasLogin == false)
            {
                return RedirectToAction("LogIn", "Login", new { area = "Login" });
            }
            // Gönderilken ıd'ye ait olan rolü sil.
            var silinecekRol = await _roleManager.FindByIdAsync(id);
            await _roleManager.DeleteAsync(silinecekRol);

            // kaldıraktan sonra da ana listeye dön...
            return RedirectToAction(nameof(Index));
        }

    }
}

