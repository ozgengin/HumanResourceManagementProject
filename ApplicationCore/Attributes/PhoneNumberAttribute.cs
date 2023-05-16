using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ApplicationCore.Attributes
{
    public class PhoneNumberAttribute : ValidationAttribute
    {
       
        public override bool IsValid(object value)
        {
            if (value == null)
            {
                return true; // null değer kabul edilebilir
            }

            string phoneNumber = value.ToString();
            phoneNumber = phoneNumber.Replace(" ", "").Replace("(", "").Replace(")", "").Replace("-", "");

            
            if (phoneNumber.Length == 10)
            {
                return true;
            }

            return false;
        }

        public override string FormatErrorMessage(string name)
        {
            return "Please enter a valid phone number without the leading 0 (Example: 5339997788).";
        }
    }

}
