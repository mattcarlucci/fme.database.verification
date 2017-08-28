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

namespace Fme.Library.Comparison
{
    /// <summary>
    /// Class DateConverter.
    /// </summary>
    /// <seealso cref="Fme.Library.Comparison.DateTimeConverter" />
    public class DateConverter : DateTimeConverter
    {
        /// <summary>
        /// Transforms the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="offset">The offset.</param>
        /// <returns>System.String.</returns>
        public override string Transform(string value, int offset)
        {
            return base.Transform(value, (dt) =>  dt.AddHours(offset).Date);
        }
    }
   
   
}
