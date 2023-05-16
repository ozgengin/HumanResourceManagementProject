using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Attributes
{
    public class ValidCharFormat : ValidationAttribute
    {
        public override bool IsValid(object? value)
        {
            string input = value as string;

            if (input == null)
                return true;

            // Girdi, sadece harfleri içeriyor mu kontrol ediliyor
            foreach (char c in input)
            {
                if (!char.IsLetter(c) && c != ' ' && c != '.')
                {
                    ErrorMessage = "Just enter letters.";
                    return false;
                }
            }

            return true;
        }

    }
}
