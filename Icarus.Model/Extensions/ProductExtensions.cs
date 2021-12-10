using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Icarus.Model.Extensions
{
    public static class ProductExtensions
    {
        // Londra saat dilimine dönüştüren fonksiyon.
        public static DateTime toLondonTimeZone(this DateTime trTimeZone)
        {
            return trTimeZone.AddHours(-3);
        }

        // Tokyo saat dilimine dönüştüren fonksiyon.
        public static DateTime toTokyoTimeZone(this DateTime trTimeZone)
        {
            return trTimeZone.AddHours(6);
        }
    }
}
