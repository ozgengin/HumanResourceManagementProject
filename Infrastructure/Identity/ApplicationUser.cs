using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using ApplicationCore.Attributes;
using ApplicationCore.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Identity
{

    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {

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

        public string Mail
        {
            get
            {
                return CreateEmail(UserName);
            }
        }

        [ValidIdentificationNumber(ErrorMessage = "Please enter a valid identification number.")]
        public long? IdentificationNumber { get; set; }

        
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

        public string CreateEmail(string UserName)
        {
            string email = "";
            if (!string.IsNullOrEmpty(UserName))
            {
                email = UserName.ToLower() + "@bilgeadamboost.com";
            }

            return email;
        }

        public bool HasLogin { get; set; } = false;


    }

}


