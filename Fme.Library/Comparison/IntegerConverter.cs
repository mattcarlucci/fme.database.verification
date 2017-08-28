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
using System.Collections.Generic;
using System.Linq;

namespace Fme.Library.Comparison
{
    /// <summary>
    /// Class IntegerConverter.
    /// </summary>
    /// <seealso cref="Fme.Library.Comparison.GenericConverter{System.Int32}" />
    public class IntegerConverter : GenericConverter<int>
    {
        /// <summary>
        /// Transforms the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="offset">The offset.</param>
        /// <returns>System.String.</returns>
        public string Transform(string value, int offset)
        {
            string[] values = Split(value);
            List<int> converts = new List<int>();

            values.ToList().ForEach(item => converts.Add(ToInteger(item)));            
            return Join(converts.ToArray());
        }

        /// <summary>
        /// To the integer.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>System.Int32.</returns>
        public static int ToInteger(object value)
        {
            return (int)Math.Floor((float)System.Convert.ChangeType(value, typeof(float)));
        }
    }
   
   
}
