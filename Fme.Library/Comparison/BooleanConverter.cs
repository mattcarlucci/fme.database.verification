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
    /// Class BooleanConverter.
    /// </summary>
    /// <seealso cref="Fme.Library.Comparison.GenericConverter{System.Boolean}" />
    public class BooleanConverter : GenericConverter<bool>
    {
        /// <summary>
        /// Transforms the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="offset">The offset.</param>
        /// <returns>System.String.</returns>
        public virtual string Transform(string value, int offset)
        {
            return base.Transform(value, (dt) => ToBoolean(dt));
        }

        /// <summary>
        /// To the integer.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>System.Int32.</returns>
        private bool ToBoolean(bool value)
        {
            return (bool)System.Convert.ChangeType(value, typeof(bool));
        }
    }
   
   
}
