using ApplicationCore.Attributes;
using System.ComponentModel.DataAnnotations;

namespace Project.Models
{
    public class EditViewModel
    {
        [StringLength(100, ErrorMessage = "Address can be max 100 characters.")]
        public string Address { get; set; }

        [PhoneNumber(ErrorMessage = "Please enter a valid phone number.")]
        [Required(ErrorMessage = "This area is required.")]
        public string Phone { get; set; }

        [ValidImageFormat(ErrorMessage = "Only JPEG or PNG image format is allowed.")]
        public IFormFile Image { get; set; }
    }
}
