using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Attributes
{
    public class ValidIdentiyNoAttribute : ValidationAttribute
    {
        public ValidIdentiyNoAttribute()
         : base("{0} must be a valid 16-digit identity number.")
        {
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
            {
                // Değer null ise doğrulama geçersiz sayılır.
                return new ValidationResult(ErrorMessage);
            }

            long identityNo;
            if (!long.TryParse(value.ToString(), out identityNo))
            {
                // Değer sayıya dönüştürülemezse veya 16 haneli değilse doğrulama geçersiz sayılır.
                return new ValidationResult(ErrorMessage);
            }

            if (identityNo.ToString().Length != 16)
            {
                // Sayı 16 haneli değilse doğrulama geçersiz sayılır.
                return new ValidationResult(ErrorMessage);
            }

            // Geçerli bir 16 haneli sayı girildiği için doğrulama başarılı sayılır.
            return ValidationResult.Success;
        }

    }
}
