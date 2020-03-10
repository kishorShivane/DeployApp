using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DeployApp
{
    public static class HexDecimalHelper
    {
        public static string ToHexString(this Decimal dec)
        {
            var sb = new StringBuilder();
            while (dec > 1)
            {
                var r = dec % 16;
                dec /= 16;
                sb.Insert(0, ((int)r).ToString("X"));
            }
            return sb.ToString();
        }
    }
}
