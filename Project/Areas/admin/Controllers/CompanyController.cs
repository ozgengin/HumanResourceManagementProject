using ApplicationCore.Entities;
using Ardalis.Specification.EntityFrameworkCore;
using Infrastructure.Data;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project.Areas.admin.Models;

namespace Project.Areas.admin.Controllers
{
    [Area("admin")]
    public class CompanyController : Controller
    {
        private readonly HumanResourceContext _db;
        private readonly IWebHostEnvironment _env;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public CompanyController(HumanResourceContext db, IWebHostEnvironment env, UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager)
        {
            _db = db;
            _env = env;
            _userManager = userManager;
            _signInManager = signInManager;
        }
        // GET: CompanyController
        public async Task<IActionResult> Index()
        {
            var uıd = _userManager.GetUserId(HttpContext.User);

            var us = await _userManager.FindByIdAsync(uıd);
            if (us.HasLogin == false)
            {
                return RedirectToAction("LogIn", "Login", new { area = "Login" });
            }
            return View(await _db.Companies.ToListAsync());
        }

        // GET: CompanyController/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            var uıd = _userManager.GetUserId(HttpContext.User);

            var us = await _userManager.FindByIdAsync(uıd);
            if (us.HasLogin == false)
            {
                return RedirectToAction("LogIn", "Login", new { area = "Login" });
            }
            if (id == null || _db.Companies == null)
            {
                return NotFound();
            }

            var company = await _db.Companies
                .FirstOrDefaultAsync(m => m.Id == id);
            if (company == null)
            {
                return NotFound();
            }

            return View(company);
        }

        // GET: CompanyController/Create
        public async Task<IActionResult> Create()
        {
            var uıd = _userManager.GetUserId(HttpContext.User);

            var us = await _userManager.FindByIdAsync(uıd);
            if (us.HasLogin == false)
            {
                return RedirectToAction("LogIn", "Login", new { area = "Login" });
            }
            return View();
        }

        // POST: CompanyController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateViewModel cvm)
        {
            if (ModelState.IsValid)
            {
                var company = new Company()
                {
                    Name = cvm.Name,
                    Title = cvm.Title,
                    IdentificationNo = cvm.IdentificationNo,
                    TaxNo = cvm.TaxNo,
                    TaxOffice = cvm.TaxOffice,
                    Phone = cvm.Phone,
                    Adress = cvm.Adress,
                    Email = cvm.Email,
                    EmployeeCount = cvm.EmployeeCount,
                    FoundYear = cvm.FoundYear,
                    ContractStartDate = cvm.ContractStartDate,
                    ContractEndDate = cvm.ContractEndDate
                };

                if (cvm.Image != null)
                {
                    string dosyaAd = Guid.NewGuid().ToString() + Path.GetExtension(cvm.Image.FileName);
                    string kayitYolu = Path.Combine(_env.WebRootPath, "img", dosyaAd);
                    using (var stream = new FileStream(kayitYolu, FileMode.Create))
                    {
                        cvm.Image.CopyTo(stream);
                    }

                    company.LogoImageName = dosyaAd;
                }

                _db.Companies.Add(company);

                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));

            } 

          
               return View();
        }

        // GET: CompanyController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            var uıd = _userManager.GetUserId(HttpContext.User);

            var us = await _userManager.FindByIdAsync(uıd);
            if (us.HasLogin == false)
            {
                return RedirectToAction("LogIn", "Login", new { area = "Login" });
            }
            Company company = _db.Companies.Find(id);
            var ecVm = new EditCompanyViewModel()
            {
                Adress = company.Adress,
                Phone = company.Phone,
                Email = company.Email,
                EmployeeCount = company.EmployeeCount,
                ContractStartDate = company.ContractStartDate,
                ContractEndDate = company.ContractEndDate
            };
            TempData["Id"] = id;
            return View(ecVm);

        }

        // POST: CompanyController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(EditCompanyViewModel ecVm)
        {
            if (ModelState.IsValid)
            {
                Company company = _db.Companies.Find(TempData["Id"]);
                company.Adress = ecVm.Adress;
                company.Phone = ecVm.Phone;
                company.Email = ecVm.Email;
                company.EmployeeCount = ecVm.EmployeeCount;
                company.ContractStartDate = ecVm.ContractStartDate;
                company.ContractEndDate = ecVm.ContractEndDate;

                if (ecVm.Image != null)
                {
                    string dosyaAd = Guid.NewGuid().ToString() + Path.GetExtension(ecVm.Image.FileName);
                    string kayitYolu = Path.Combine(_env.WebRootPath, "img", dosyaAd);
                    using (var stream = new FileStream(kayitYolu, FileMode.Create))
                    {
                        ecVm.Image.CopyTo(stream);
                    }

                    company.LogoImageName = dosyaAd;
                }

                _db.Update(company);

                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            // Geçersiz model durumunda geri dönün
            return View(ecVm);

        }

        // GET: CompanyController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: CompanyController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
