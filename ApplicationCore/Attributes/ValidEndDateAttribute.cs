using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Attributes
{
    public class ValidEndDateAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            DateTime? exitDate = value as DateTime?;
            DateTime? hireDate = validationContext.ObjectType.GetProperty("HireDate").GetValue(validationContext.ObjectInstance, null) as DateTime?;

            if (exitDate.HasValue && hireDate.HasValue && exitDate.Value < hireDate.Value)
            {
                return new ValidationResult("Exit date cannot be earlier than hire date");
            }

            return ValidationResult.Success;
        }
    }
}
