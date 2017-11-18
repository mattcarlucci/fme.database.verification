using Fme.Library.Comparison;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Fme.Library.Extensions
{
    public static class StringExtension
    {
        /// <summary>
        /// To the integer.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>System.Int32.</returns>
        public static int ToInteger(this string value)
        {
            int.TryParse(value, out int i);
            return i;
        }

        /// <summary>
        /// Determines whether the specified value is integer.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>System.Int32.</returns>
        public static bool IsInteger(this string value)
        {
            return int.TryParse(value, out int i);            
        }

     
        /// <summary>
        /// Determines whether the specified input is unicode.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns><c>true</c> if the specified input is unicode; otherwise, <c>false</c>.</returns>
        public static bool IsMultiByteString(this string input)
        {
            const int MaxAnsiCode = 255;
            return input.All(c => c > MaxAnsiCode);
        }
        /// <summary>
        /// Determines whether the specified symbol is symbol.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="symbol">The symbol.</param>
        /// <returns><c>true</c> if the specified symbol is symbol; otherwise, <c>false</c>.</returns>
        public static bool IsSymbol(this string value,  string symbol)
        {
            return value.All(a => Char.IsSymbol(symbol.FirstOrDefault()) == true);
        }
        /// <summary>
        /// Determines whether the specified symbol is character.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="symbol">The symbol.</param>
        /// <returns><c>true</c> if the specified symbol is character; otherwise, <c>false</c>.</returns>
        public static bool IsChar(this string value, string symbol)
        {
            return value.FirstOrDefault().Equals(symbol.FirstOrDefault());
        }
        /// <summary>
        /// Determines whether the specified value is empty.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns><c>true</c> if the specified value is empty; otherwise, <c>false</c>.</returns>
        public static bool IsEmpty(this string value)
        {
            return string.IsNullOrEmpty(value) || string.IsNullOrWhiteSpace(value);
        }
        /// <summary>
        /// Determines whether the specified value is printable.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns><c>true</c> if the specified value is printable; otherwise, <c>false</c>.</returns>
        public static bool IsPrintable(this string value)
        {
            return value.All(a => Char.IsControl(a) == false);
        }

        /// <summary>
        /// Nots the printable.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public static bool NotPrintable(this string value)
        {
            return value.Any(a => Char.IsControl(a) == true);
        }
        /// <summary>
        /// Determines whether the specified value is alpha.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns><c>true</c> if the specified value is alpha; otherwise, <c>false</c>.</returns>
        public static bool IsAlpha(this string value)
        {
            return value.All(a => char.IsLetter(a) == true);
        }
        /// <summary>
        /// Determines whether the specified value is numeric.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns><c>true</c> if the specified value is numeric; otherwise, <c>false</c>.</returns>
        public static bool IsNumeric(this string value)
        {
            return value.All(a => char.IsNumber(a));
        }
        /// <summary>
        /// Determines whether [is alpha numeric] [the specified value].
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns><c>true</c> if [is alpha numeric] [the specified value]; otherwise, <c>false</c>.</returns>
        public static bool IsAlphaNumeric(this string value)
        {
            return value.All(a => char.IsLetter(a) || char.IsNumber(a));
        }

        /// <summary>
        /// Strings the lengh.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="len">The length.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public static bool StringLengh(string value, int len)
        {
            return value.Length == len;
        }
        /// <summary>
        /// Determines whether the specified value is match.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="expvalue">The expvalue.</param>
        /// <param name="match">The match.</param>
        /// <returns><c>true</c> if the specified value is match; otherwise, <c>false</c>.</returns>
        public static bool IsMatch(this string value, string expvalue, string match)
        {
            Regex exp = new Regex(expvalue);
            return exp.IsMatch(match);
        }

        /// <summary>
        /// To the date time.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>System.Nullable&lt;DateTime&gt;.</returns>
        public static DateTime ToDateTime(this string value)
        {             
            DateTime.TryParse(value, out DateTime result);
            return result;
        }

        /// <summary>
        /// Determines whether [is date time] [the specified value].
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns><c>true</c> if [is date time] [the specified value]; otherwise, <c>false</c>.</returns>
        public static bool IsDateTime(this string value)
        {           
            return DateTime.TryParse(value, out DateTime result);
        }

        /// <summary>
        /// Greaters the than.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="compare">The compare.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public static bool GreaterThan(this DateTime value, DateTime compare)
        {
            return value.CompareTo(compare) > 0;
        }
        
        /// <summary>
        /// Determines whether the specified format is format.
        /// </summary>
        /// <param name="format">The format.</param>
        /// <returns><c>true</c> if the specified format is format; otherwise, <c>false</c>.</returns>
        public static bool IsFormat(string format)
        {
            try
            {
                String dts = DateTime.Now.ToString(format);
                DateTime.ParseExact(dts, format, CultureInfo.InvariantCulture);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }

  
}
