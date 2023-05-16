using Infrastructure.Identity;
using Project.Models;

namespace Project.Extensions
{
    public static class MappingExtensions
    {
        public static AdminViewModel ToAdminViewModel(this ApplicationUser admin)
        {
            return new AdminViewModel()
            {
                Name = admin.Name,
                SecondName = admin.SecondName,
                Surname = admin.Surname,
                SecondSurname = admin.SecondSurname,
                BirthDate = admin.BirthDate,
                BirthPlace = admin.BirthPlace,
                IdentificationNumber = admin.IdentificationNumber,
                Address = admin.Address,
                Phone = admin.Phone,
                Mail= admin.Email,
                ImageName = admin.ImageName
            };
        }
    }
}
