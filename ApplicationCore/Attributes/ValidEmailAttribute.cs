using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Attributes
{
    public class ValidEmailAttribute : ValidationAttribute
    {
        public override bool IsValid(object? value)
        {
            string input = value as string;
            if (input == null)
                return true;

            // Girdi, e-posta adresi formatında mı kontrol ediliyor
            if (!IsValidEmail(input))
            {
                ErrorMessage = "Please enter a valid email address.";
                return false;
            }

            return true;
        }

        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

    }
}
