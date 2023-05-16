using ApplicationCore.Attributes;
using ApplicationCore.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Project.Areas.admin.Models
{
    public class ManagerUserCreateViewModel
    {
        [IsEnglishLettersOnly(ErrorMessage ="Please enter only Eng characters")]
        public string UserName { get; set; }
        [NoNumberInName(ErrorMessage = "Please Enter only letter characters.")]
        [Required(ErrorMessage = "This area is required.")]
        [StringLength(20, ErrorMessage = "You can enter only 20 character long.", MinimumLength = 2)]
        public string Name { get; set; }

        [NoNumberInName(ErrorMessage = "Please Enter only letter characters.")]
        public string? SecondName { get; set; }

        [Required(ErrorMessage = "This area is required.")]
        [NoNumberInName(ErrorMessage = "Please Enter only letter characters.")]
        [StringLength(20, ErrorMessage = "You can enter only 20 character long.", MinimumLength = 2)]
        public string Surname { get; set; }

        [NoNumberInName(ErrorMessage = "Please Enter only letter characters.")]
        public string? SecondSurname { get; set; }

        public string Mail
        {
            get
            {
                return CreateEmail(Name, Surname);
            }
        }

        [ValidIdentificationNumber(ErrorMessage = "Please enter a valid identification number.")]
        [Required(ErrorMessage = "This area is required.")]
        public long? IdentificationNumber { get; set; }

        [Required(ErrorMessage = "This area is required.")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime BirthDate { get; set; }

        [Required(ErrorMessage = "This area is required.")]
        public string BirthPlace { get; set; }

        public string? ImageName { get; set; }

        [NotMapped]
        [ValidImageFormat(ErrorMessage = "Only JPEG or PNG image format is allowed.")]
        public IFormFile? Image { get; set; }

        [Required(ErrorMessage = "This area is required.")]
        [StringLength(100, ErrorMessage = "Address can be max 100 characters.")]
        public string Address { get; set; }

        [Required(ErrorMessage = "This area is required.")]
        [PhoneNumber(ErrorMessage = "Please enter a valid phone number.")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "This area is required.")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime HireDate { get; set; }
        public Profession? Profession { get; set; }
        public int? ProfessionId { get; set; }
        public Department? Department { get; set; }
        public int? DepartmentId { get; set; }

        public string CreateEmail(string firstName, string lastName)
        {
            string email = firstName.ToLower() + "." + lastName.ToLower() + "@bilgeadamboost.com";
            return email;
        }

        [Required(ErrorMessage = "This area is required.")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime ExitDate { get; set; }
        public decimal Salary { get; set; }

        public Company? Company { get; set; }
        public int? CompanyId { get; set; }
    }
}
