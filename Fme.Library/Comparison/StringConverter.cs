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
using System.Collections.Generic;

namespace Fme.Library.Comparison
{
    /// <summary>
    /// Class StringConverter.
    /// </summary>
    /// <seealso cref="Fme.Library.Comparison.GenericConverter{System.String}" />
    public class StringConverter :GenericConverter<string>
    {
        /// <summary>
        /// Joins the specified values.
        /// </summary>
        /// <param name="values">The values.</param>
        /// <param name="offset">The offset.</param>
        /// <returns>System.String.</returns>
        public string Join(string[] values, int offset)
        {
            return base.Join(values, (value) => value);
        }
        /// <summary>
        /// Transforms the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="offset">The offset.</param>
        /// <returns>System.String.</returns>
        public string Transform(string value, int offset)
        {
            return base.Transform(value, (dt) => dt);
        }
        /// <summary>
        /// Transforms the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="translate">The translate.</param>
        /// <returns>System.String.</returns>
        public string Transform(string value, Dictionary<string,string> translate)
        {

            return base.Transform(value, (dt) => translate.ContainsKey(dt) ? translate[dt] : dt);
        }
        /// <summary>
        /// Startses the width.
        /// </summary>
        /// <param name="value1">The value1.</param>
        /// <param name="value2">The value2.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public bool StartsWith(string value1, string value2)
        {
           return base.Compare(value1, (x) => x.StartsWith(value2));
        }

        /// <summary>
        /// Endses the with.
        /// </summary>
        /// <param name="value1">The value1.</param>
        /// <param name="value2">The value2.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public bool EndsWith(string value1, string value2)
        {
            return base.Compare(value1, (x) => x.EndsWith(value2));
        }

        /// <summary>
        /// Determines whether [contains] [the specified value1].
        /// </summary>
        /// <param name="value1">The value1.</param>
        /// <param name="value2">The value2.</param>
        /// <returns><c>true</c> if [contains] [the specified value1]; otherwise, <c>false</c>.</returns>
        public bool Contains(string value1, string value2)
        {
            return base.Compare(value1, (x) => x.Contains(value2));
        }
        /// <summary>
        /// Equalses the specified value1.
        /// </summary>
        /// <param name="value1">The value1.</param>
        /// <param name="value2">The value2.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public bool Equals(string value1, string value2)
        {
            return base.Compare(value1, (x) => x.Equals(value2));
        }
    }
   
   
}
