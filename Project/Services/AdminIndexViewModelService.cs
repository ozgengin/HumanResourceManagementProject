using Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Project.Extensions;
using Project.Interfaces;
using Project.Models;

namespace Project.Services
{
    public class AdminIndexViewModelService : IAdminViewModelService
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public AdminIndexViewModelService(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }
        public async Task<AdminIndexViewModel> GetAdminViewModelAsync(string id)
        {
            var vm = new AdminIndexViewModel();

            if(await _userManager.Users.AnyAsync())
            {
                var admin = await _userManager.FindByIdAsync(id);
                vm.AdminViewModel = admin.ToAdminViewModel();
            }
            return vm;
        }
    }
}
