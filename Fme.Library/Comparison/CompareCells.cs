using Fme.Library.Enums;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fme.Library.Comparison
{
    public class CompareCells
    {
        /// <summary>
        /// Determines whether the specified left is equal.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <param name="compareType">Type of the compare.</param>
        /// <param name="operator">The operator.</param>
        /// <returns><c>true</c> if the specified left is equal; otherwise, <c>false</c>.</returns>
        public static bool IsEqual(string left, string right, ComparisonTypeEnum compareType, OperatorEnums @operator, string ignoreChars)
        {
            right = string.IsNullOrEmpty(right) ? string.Empty : right;
            left = string.IsNullOrEmpty(left) ? string.Empty : left;
            ignoreChars = string.IsNullOrEmpty(ignoreChars) ? string.Empty : ignoreChars;

            ignoreChars.ToList().ForEach(@char => right = right.Replace(@char, ' '));
            ignoreChars.ToList().ForEach(@char => left = left.Replace(@char, ' '));

            if (@operator == OperatorEnums.In)
                return CompareIn(left, right, compareType);

            if (@operator == OperatorEnums.Equals)
                return CompareEqual(left, right, compareType);

            if (@operator == OperatorEnums.StartsWidth)
                return right.StartsWith(left);

            if (@operator == OperatorEnums.EndsWidth)
                return right.EndsWith(left);

            if (@operator == OperatorEnums.Contains)
                return right.Contains(left);

            return false;
        }

        /// <summary>
        /// Compares the in.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <param name="compareType">Type of the compare.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        private static bool CompareIn(string left, string right, ComparisonTypeEnum compareType)
        {
            var list = right.Split(new char[] { '|' }, StringSplitOptions.None);
            if (compareType == ComparisonTypeEnum.Datetime)
                return CompareDateTime(left, list);

            return list.Contains(left);
        }
        /// <summary>
        /// Compares the equal.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <param name="compareType">Type of the compare.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        private static bool CompareEqual(string left, string right, ComparisonTypeEnum compareType)
        {
            var list = right.Split(new char[] { '|' }, StringSplitOptions.None);
            if (compareType == ComparisonTypeEnum.Datetime)
                return CompareDateTime(left, right);

            return left == right;
        }
        /// <summary>
        /// Compares the starts with.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <param name="compareType">Type of the compare.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        private static bool CompareStartsWith(string left, string right, ComparisonTypeEnum compareType)
        {
            var list = right.Split(new char[] { '|' }, StringSplitOptions.None);
            if (compareType == ComparisonTypeEnum.Datetime)
                return CompareDateTime(left, right);

            return left == right;
        }

        /// <summary>
        /// Compares the date time.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        private static bool CompareDateTime(string left, string right)
        {
            DateTimeCompare dti = new DateTimeCompare();
            left = dti.Parse(left);
            right = dti.Parse(right);
            return left == right;
        }

        /// <summary>
        /// Compares the date time.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="list">The list.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        private static bool CompareDateTime(string left, string[] list)
        {
            DateTimeCompare dti = new DateTimeCompare();
            left = dti.Parse(left);

            var right = dti.Parse(list);
            return right.Contains(left);
        }

        /// <summary>
        /// Compares the date.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="list">The list.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        private static bool CompareDate(string left, string[] list)
        {
            DateCompare dti = new DateCompare();
            left = dti.Parse(left);

            var right = dti.Parse(list);
            return right.Contains(left);
        }

    }
}
