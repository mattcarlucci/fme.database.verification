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
    /// Class FloatConverter.
    /// </summary>
    /// <seealso cref="Fme.Library.Comparison.GenericConverter{System.Single}" />
    public class FloatConverter : GenericConverter<float>
    {
        /// <summary>
        /// Transforms the specified value.
        /// </summary>
        /// <param name="values">The values.</param>
        /// <param name="offset">The offset.</param>
        /// <returns>System.String.</returns>
        public virtual string Transform(string values, int offset)
        {
            return base.Transform(values, (value) => ToFloat(value));            
        }
        /// <summary>
        /// To the float.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>System.Single.</returns>
        public static float ToFloat(object value)
        {
            return (float)System.Convert.ChangeType(value, typeof(float));
        }
    }
   
   
}
