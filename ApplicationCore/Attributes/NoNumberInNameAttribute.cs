using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ApplicationCore.Attributes
{
    public class NoNumberInNameAttribute : ValidationAttribute
    {
        public override bool IsValid(object? value)
        {
            if (value == null)
                return true;

            string name = value.ToString();

            // Sayı içeriyorsa false döndür
            if (name.Any(char.IsDigit))
                return false;

            return true;
        }
    }

}
