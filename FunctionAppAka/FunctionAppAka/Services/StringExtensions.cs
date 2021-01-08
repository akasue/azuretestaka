using System;
using System.Collections.Generic;
using System.Text;

namespace FunctionAppAka.Services
{
    public static class StringExtensions
    {
        public static bool IsNotNullOrEmpty(this string str)
        {
            return !string.IsNullOrEmpty(str);
        }

        public static decimal ToDecimal (this string str)
        {
            return decimal.Parse(str);
        }
    }
}
