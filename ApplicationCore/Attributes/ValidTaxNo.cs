using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Attributes
{
    public class ValidTaxNo : ValidationAttribute
    {
        public override bool IsValid(object? value)
        {
            string input = value as string;
            if (input == null)
                return true;

            // Girdi, sadece sayıları içeriyor mu kontrol ediliyor
            foreach (char c in input)
            {
                if (!char.IsDigit(c))
                {
                    ErrorMessage = "Sadece sayılar içermelidir.";
                    return false;
                }
            }

            // Girdi, 10 karakter uzunluğunda mı kontrol ediliyor
            if (input.Length != 10)
            {
                ErrorMessage = "Sadece 10 karakter uzunluğunda olmalıdır.";
                return false;
            }

            return true;
        }

    }
}
