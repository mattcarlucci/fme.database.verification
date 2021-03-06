﻿using System;
using System.Collections.Generic;

namespace Fme.Library.Comparison
{
    /// <summary>
    /// Class DateCompare.
    /// </summary>
    /// <seealso cref="System.Collections.Generic.IEqualityComparer{System.String}" />
    public class DateCompare : IEqualityComparer<string>
    {

        /// <summary>
        /// Determines whether the specified objects are equal.
        /// </summary>
        /// <param name="x">The first object of type <paramref name="T" /> to compare.</param>
        /// <param name="y">The second object of type <paramref name="T" /> to compare.</param>
        /// <returns>true if the specified objects are equal; otherwise, false.</returns>
        public bool Equals(string x, string y)
        {
            try
            {
                return DateTime.Parse(x).Date == DateTime.Parse(y).Date;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <param name="obj">The <see cref="T:System.Object" /> for which a hash code is to be returned.</param>
        /// <returns>A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table.</returns>
        public int GetHashCode(string obj)
        {
            try
            {
                return DateTime.Parse(obj).Date.GetHashCode();
            }
            catch (Exception)
            {
                return 0;
            }
        }
        /// <summary>
        /// Parses the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>System.String.</returns>
        public string Parse(string value)
        {
            DateTime dt;
            if (DateTime.TryParse(value, out dt))
                return dt.Date.Ticks.ToString();
            return value;
        }

        /// <summary>
        /// Parses the specified values.
        /// </summary>
        /// <param name="values">The values.</param>
        /// <returns>System.String[].</returns>
        public string[] Parse(string[] values)
        {
            List<string> items = new List<string>();

            foreach (var item in values)
                items.Add(Parse(item));

            return items.ToArray();
        }
    }
}
