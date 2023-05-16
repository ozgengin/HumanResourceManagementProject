using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Attributes
{
    public static class Methods
    {
        public static string FormatPhoneNumber(string phoneNumber)
        {
            // Telefon numarasındaki sadece sayıları alın
            string digitsOnly = new string(phoneNumber.Where(char.IsDigit).ToArray());

            // Telefon numarasının 11 haneli olup olmadığını kontrol edin
            if (digitsOnly.Length == 11)
            {
                // 11 haneli numaraların ilk hanesi "0" olmalı
                if (digitsOnly[0] == '0')
                {
                    // Numarayı formatlayın: 0XXX XXX XX XX
                    return string.Format("{0:(0##) ### ## ##}", double.Parse(digitsOnly));
                }
            }
            // Numarayı formatlayın: XXX XXX XX XX
            return string.Format("{0:### ### ## ##}", double.Parse(digitsOnly));
        }
    }
}
