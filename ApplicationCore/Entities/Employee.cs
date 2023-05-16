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
    public class Employee : BaseEntity
    {
        public string UserId { get; set; }
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Only English characters are allowed.")]
        public string UserName { get; set; }

        [NoNumberInName(ErrorMessage = "Please Enter only letter characters.")]
        [StringLength(20, ErrorMessage = "You can enter only 20 character long.", MinimumLength = 2)]
        public string? Name { get; set; }

        [NoNumberInName(ErrorMessage = "Please Enter only letter characters.")]
        public string? SecondName { get; set; }


        [NoNumberInName(ErrorMessage = "Please Enter only letter characters.")]
        [StringLength(20, ErrorMessage = "You can enter only 20 character long.", MinimumLength = 2)]
        public string? Surname { get; set; }

        [NoNumberInName(ErrorMessage = "Please Enter only letter characters.")]
        public string? SecondSurname { get; set; }

        [ValidIdentificationNumber(ErrorMessage = "Please enter a valid identification number.")]

        public long? IdentificationNumber { get; set; }

        public string? Mail { get; set; }


        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]

        public DateTime? BirthDate { get; set; }

        
        [ValidCharFormat]
        public string? BirthPlace { get; set; }

        public string? ImageName { get; set; }

        [NotMapped]
        [ValidImageFormat(ErrorMessage = "Only JPEG or PNG image format is allowed.")]
        public IFormFile? Image { get; set; }


        [StringLength(100, ErrorMessage = "Address can be max 100 characters.")]
        public string? Address { get; set; }


        [PhoneNumber(ErrorMessage = "Please enter a valid phone number.")]
        public string? Phone { get; set; }


        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? HireDate { get; set; }


        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? ExitDate { get; set; }
        
        public decimal? Salary { get; set; }
        
        public Profession? Profession { get; set; }
        public int? ProfessionId { get; set; }
        public Department? Department { get; set; }
        public int? DepartmentId { get; set; }
        public Company? Company { get; set; }
        public int? CompanyId { get; set; }
    }
}
