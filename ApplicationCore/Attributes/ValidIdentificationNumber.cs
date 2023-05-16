using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Attributes
{
    public class ValidIdentificationNumber : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value == null)
                return true;

            string deger = value.ToString();

            int toplam = 0, ciftToplam = 0, tekToplam = 0;

            if (deger.Length != 11 || deger[0] == '0')
                return false;

            for (int i = 0; i <= 9; i++)
            {
                toplam += Convert.ToInt32(deger[i].ToString());

                if (i % 2 == 0)
                    tekToplam += Convert.ToInt32(deger[i].ToString());
                else if (i % 2 != 0 && i <= 7)
                    ciftToplam += Convert.ToInt32(deger[i].ToString());
            }

            if ((7 * tekToplam - ciftToplam) % 10 != Convert.ToInt32(deger[9].ToString()))
                return false;

            if (toplam % 10 != Convert.ToInt32(deger[10].ToString()))
                return false;

            return true;

        }
    }
}
