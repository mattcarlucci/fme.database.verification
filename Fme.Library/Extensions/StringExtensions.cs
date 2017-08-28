// ***********************************************************************
// Assembly         : Fme.Common
// Author           : Matt.Carlucci
// Created          : 01-29-2017
//
// Last Modified By : Matt.Carlucci
// Last Modified On : 01-29-2017
// ***********************************************************************
// <copyright file="EnumableExtensions.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace Fme.Library.Extensions
{
    /// <summary>
    /// Class StringExtensions.
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// Tables the specified value2.
        /// </summary>
        /// <param name="value1">The value1.</param>
        /// <param name="value2">The value2.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        [Deprecated]
        public static bool Table(this string value1, string value2)
        {
            return value1.Equals(value2);
        }
    }
}
