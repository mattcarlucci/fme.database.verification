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
using System.Data;
using System.Runtime.Serialization;
using System.Linq;

namespace Fme.Library.Comparison
{
    /// <summary>
    /// Class GenericConverter.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <seealso cref="Fme.Library.Comparison.IGenericConverter{T}" />
    public class GenericConverter<T> : IGenericConverter<T>
    {
        /// <summary>
        /// The token
        /// </summary>
        public static string Token = "|";

        /// <summary>
        /// Joins the specified items.
        /// </summary>
        /// <param name="items">The items.</param>
        /// <returns>System.String.</returns>
        protected string Join(T[] items)
        {
            return string.Join(Token, items);
        }

        /// <summary>
        /// Splits the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>System.String[].</returns>
        protected string[] Split(string value)
        {
            return value.Split(new string[] { Token }, StringSplitOptions.RemoveEmptyEntries).ToArray();
        }
               
        /// <summary>
        /// Converts the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>T.</returns>
        public T Convert(string value)
        {
            FormatterConverter converter = new FormatterConverter();
            return (T)converter.Convert(value, typeof(T));
        }
        /// <summary>
        /// Converts the specified values.
        /// </summary>
        /// <param name="values">The values.</param>
        /// <returns>List&lt;T&gt;.</returns>
        public List<T> Convert(string[] values)
        {
            FormatterConverter converter = new FormatterConverter();
            List<T> items = new List<T>();
            foreach (var value in values)
                items.Add((T)converter.Convert(value, typeof(T)));
            return items;
        }
        /// <summary>
        /// Joins the specified values.
        /// </summary>
        /// <param name="values">The values.</param>
        /// <param name="selector">The selector.</param>
        /// <returns>System.String.</returns>
        public string Join(T[] values, Func<T,T> selector)
        {
            List<T> items = new List<T>();
            values.ToList().ForEach(value => items.Add(selector(value)));
            return Join(items.OrderBy(o => o).ToArray());            
        }
        /// <summary>
        /// Transforms the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="selector">The selector.</param>
        /// <returns>System.String.</returns>
        public string Transform(string value, Func<T,T> selector)
        {
            string[] values = Split(value);
            List<T> converts = (List<T>)Convert(values);
            return Join(converts.ToArray(), selector);
        }

        /// <summary>
        /// Compares the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="selector">The selector.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public bool Compare(T value, Func<T, bool> selector)
        {
            return selector(value);
           //  return value.CompareTo(selector);           
        }
    }
   
   
}
