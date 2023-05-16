using ApplicationCore.Attributes;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Project.Areas.admin.Models
{
    public class CreateViewModel
    {

        [Required(ErrorMessage = "This area is required.")]
        [StringLength(20, ErrorMessage = "You can enter only 20 character long.", MinimumLength = 2)]
        [ValidCharFormat(ErrorMessage = "Just enter letters.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "This area is required.")]
        [ValidCharFormat(ErrorMessage = "Just enter letters.")]
        public string Title { get; set; }

        [Required(ErrorMessage = "This area is required.")]
        [ValidIdentiyNoAttribute(ErrorMessage = "Invalid MERSIS number")]
        public long IdentificationNo { get; set; }

        [Required(ErrorMessage = "This area is required.")]
        [ValidTaxNo(ErrorMessage = "Just enter a number and it must be 10 characters long.")]
        public int TaxNo { get; set; }

        [Required(ErrorMessage = "This area is required.")]
        [ValidCharFormat(ErrorMessage = "Just enter letters.")]
        public string TaxOffice { get; set; }

        [NotMapped]
        [ValidImageFormat(ErrorMessage = "Only JPEG or PNG image format is allowed.")]
        public IFormFile? Image { get; set; }

        [Required(ErrorMessage = "This area is required.")]
        [PhoneNumber(ErrorMessage = "Please enter a valid phone number.")]
        public string Phone { get; set; }

        [StringLength(100, ErrorMessage = "Address can be max 100 characters.")]
        [Required(ErrorMessage = "This area is required.")]
        public string Adress { get; set; }

        [Required(ErrorMessage = "This area is required.")]
        [ValidEmailAttribute(ErrorMessage = "Enter a valid email address.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "This area is required.")]
        [Range(0, int.MaxValue, ErrorMessage = "Employee count must be a positive integer.")]
        public int EmployeeCount { get; set; }

        [ValidFountYear]
        [Required(ErrorMessage = "This area is required.")]
        public int FoundYear { get; set; }

        [Required(ErrorMessage = "This area is required.")]
        public DateTime ContractStartDate { get; set; }

        [Required(ErrorMessage = "This area is required.")]
        [ValidContractDateRange("ContractStartDate", ErrorMessage = "Contract end date cannot be earlier than contract start date")]
        public DateTime ContractEndDate { get; set; }
        
        

    }
}
