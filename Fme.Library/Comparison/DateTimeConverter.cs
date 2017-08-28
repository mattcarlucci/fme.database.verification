// ***********************************************************************
// Assembly         : Fme.Library
// Author           : mcarlucci
// Created          : 08-24-2017
//
// Last Modified By : mcarlucci
// Last Modified On : 08-27-2017
// ***********************************************************************
// <copyright file="GenericCompare.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;

namespace Fme.Library.Comparison
{
    /// <summary>
    /// Class DateTimeConverter.
    /// </summary>
    /// <seealso cref="Fme.Library.Comparison.GenericConverter{System.DateTime}" />
    public class DateTimeConverter : GenericConverter<DateTime>
    {
        /// <summary>
        /// Transforms the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="offset">The offset.</param>
        /// <returns>System.String.</returns>
        public virtual string Transform(string values, int offset)
        {
            return base.Transform(values, (dt) => dt.AddHours(offset));            
        }

        public virtual string Transform(string values, int offset, IFormatProvider provider)
        {
            return base.Transform(values, (value) => ToDateTime(value, provider));
        }

        public static DateTime ToDateTime(object value, IFormatProvider provider)
        {          
            return (DateTime)System.Convert.ChangeType(value, typeof(DateTime), provider);
        }
    }
   
   
}
