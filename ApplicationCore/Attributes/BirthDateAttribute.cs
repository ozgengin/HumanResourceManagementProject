using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Attributes
{
    public class BirthDateAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            DateTime birthDate = (DateTime)value;

            if (birthDate > DateTime.Now)
            {
                return new ValidationResult("Birth date cannot be in future.");
            }

            return ValidationResult.Success;
        }
    }
}
