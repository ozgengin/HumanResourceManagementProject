using ApplicationCore.Attributes;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Entities
{
    public class Company : BaseEntity
    {
      

        [Required(ErrorMessage = "This area is required.")]
        [StringLength(20, ErrorMessage = "You can enter only 20 character long.", MinimumLength = 2)]
        [ValidCharFormat(ErrorMessage = "Just enter letters.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "This area is required.")]
        [ValidCharFormat(ErrorMessage = "Just enter letters.")]

        public string Title { get; set; }

        [Required(ErrorMessage = "This area is required.")]
        [ValidIdentiyNoAttribute]
        public long IdentificationNo { get; set; }

        [Required(ErrorMessage = "This area is required.")]
        [ValidTaxNo(ErrorMessage = "Just enter a number and it must be 10 characters long.")]
        public long TaxNo { get; set; }

        [Required(ErrorMessage = "This area is required.")]
        [ValidCharFormat(ErrorMessage = "Just enter letters.")]

        public string TaxOffice { get; set; }

        public string? LogoImageName { get; set; }
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
        [ValidEmailAttribute(ErrorMessage ="Enter a valid email address.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "This area is required.")]
        [Range(0, int.MaxValue, ErrorMessage = "Employee count must be a positive integer.")]
        public int EmployeeCount { get; set; }

        [ValidFountYearAttribute(ErrorMessage ="Enter a valid year.")]
        public int FoundYear { get; set; }

        [Required(ErrorMessage = "This area is required.")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime ContractStartDate { get; set; }

        [Required(ErrorMessage = "This area is required.")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
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

        public bool Status {
            get
            {
                return (ContractEndDate - DateTime.Now).Days > 0 && (ContractEndDate - ContractStartDate).Days >= 0;
            }
        }

        public List<Employee> Employees { get; set; } = new List<Employee>();


    }
}
