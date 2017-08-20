using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fme.DqlProvider.Extensions
{
    /// <summary>
    /// Class IEnumerableExtensions.
    /// </summary>
    public static class IEnumerableExtensions
    {
        /// <summary>
        /// Merges the specified seperator.
        /// </summary>
        /// <param name="items">The items.</param>
        /// <param name="seperator">The seperator.</param>
        /// <returns>System.String.</returns>
        public static string Merge(this object[] items, string seperator = "|")
        {
            return string.Join(seperator, items);
        }
    }
}
