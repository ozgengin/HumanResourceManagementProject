using ApplicationCore.Attributes;
using ApplicationCore.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project.Models
{
    public class AdminViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? SecondName { get; set; }
        public string Surname { get; set; }
        public string? SecondSurname { get; set; }
        public string? Mail { get; set; }
        public long? IdentificationNumber { get; set; }
        public DateTime? BirthDate { get; set; }
        public string BirthPlace { get; set; }
        public string? ImageName { get; set; }
        public IFormFile? Image { get; set; }
        public string Address { get; set; }

        [Required(ErrorMessage = "This area is required.")]
        [PhoneNumber(ErrorMessage = "Enter your number without 0.and Insert the - symbol after every 3 digits.")]
        public string Phone { get; set; }
        public DateTime HireDate { get; set; }
        public Profession? Profession { get; set; }
        public int? ProfessionId { get; set; }
        public Department? Department { get; set; }
        public int? DepartmentId { get; set; }
    }
}
