using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Attributes
{
    public class ValidFountYearAttribute : ValidationAttribute
    {
        private const int MinYear = 1900;

        public ValidFountYearAttribute()
            : base("Found Year must be a valid year between " + MinYear + " and the current year.")
        {
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
            {
                // Değer null ise doğrulama geçersiz sayılır.
                return new ValidationResult(ErrorMessage);
            }

            int year;
            if (!int.TryParse(value.ToString(), out year))
            {
                // Değer sayıya dönüştürülemezse doğrulama geçersiz sayılır.
                return new ValidationResult(ErrorMessage);
            }

            if (year < MinYear || year > DateTime.Now.Year)
            {
                // Yıl, minimum yıldan küçük veya şu anki yıldan büyükse doğrulama geçersiz sayılır.
                return new ValidationResult(ErrorMessage);
            }

            // Geçerli bir yıl girildiği için doğrulama başarılı sayılır.
            return ValidationResult.Success;
        }

    }
}
