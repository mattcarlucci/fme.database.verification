using Fme.Library.Models;
using System.Collections.Generic;
using System;

namespace Fme.Library.Comparison
{
    public class MappingModelComparer : IEqualityComparer<CompareMappingModel>
    {
        public bool Equals(CompareMappingModel x, CompareMappingModel y)
        {
            return (x.LeftSide + x.RightSide) == (y.LeftSide + y.RightSide);
        }

        public int GetHashCode(CompareMappingModel obj)
        {
            var key = obj.LeftSide + obj.RightSide;
            return key.GetHashCode();
        }
    }
    /// <summary>
    /// Class AggregateComparer.
    /// </summary>
    /// <seealso cref="System.Collections.Generic.IEqualityComparer{System.String}" />
    public class AggregateComparer : IEqualityComparer<string>
    {
        /// <summary>
        /// Determines whether the specified objects are equal.
        /// </summary>
        /// <param name="x">The first object of type <paramref name="T" /> to compare.</param>
        /// <param name="y">The second object of type <paramref name="T" /> to compare.</param>
        /// <returns>true if the specified objects are equal; otherwise, false.</returns>
        public bool Equals(string x, string y)
        {
            return x.Equals(y);
        }

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <param name="obj">The <see cref="T:System.Object" /> for which a hash code is to be returned.</param>
        /// <returns>A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table.</returns>
        public int GetHashCode(string obj)
        {
            return obj.GetHashCode();
        }
    }
}
