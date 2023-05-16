using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ApplicationCore.Attributes
{
    public class IsEnglishLettersOnlyAttribute : ValidationAttribute
    {
        public static bool IsEnglishLettersOnly(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return false;
            }

            Regex regex = new Regex("^[a-zA-Z]*$");
            return regex.IsMatch(input);
        }
    }
}
