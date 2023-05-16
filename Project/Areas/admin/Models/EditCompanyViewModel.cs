using ApplicationCore.Attributes;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project.Areas.admin.Models
{
	public class EditCompanyViewModel
	{
        [Required(ErrorMessage = "This area is required.")]
        [PhoneNumber(ErrorMessage = "Please enter a valid phone number.")]
        public string Phone { get; set; }

        [StringLength(100, ErrorMessage = "Address can be max 100 characters.")]
        [Required(ErrorMessage = "This area is required.")]
        public string Adress { get; set; }

        [NotMapped]
        [ValidImageFormat(ErrorMessage = "Only JPEG or PNG image format is allowed.")]
        public IFormFile? Image { get; set; }

        [Required(ErrorMessage = "This area is required.")]
        [ValidEmailAttribute(ErrorMessage = "Enter a valid email address.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "This area is required.")]
        [Range(0, int.MaxValue, ErrorMessage = "Employee count must be a positive integer.")]
        public int EmployeeCount { get; set; }

        [Required(ErrorMessage = "This area is required.")]
        public DateTime ContractStartDate { get; set; }

        [Required(ErrorMessage = "This area is required.")]
        public DateTime ContractEndDate
        {
            get { return _contractEndDate; }
            set
            {
                if (value < ContractStartDate)
                {
                    throw new ArgumentException("Contract end date cannot be earlier than contract start date");
                }
                else
                {
                    _contractEndDate = value;
                }
            }
        }
        private DateTime _contractEndDate;
    }
}
