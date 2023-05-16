using Project.Models;

namespace Project.Interfaces
{
    public interface IAdminViewModelService
    {
        Task<AdminIndexViewModel> GetAdminViewModelAsync(string id);
    }
}
