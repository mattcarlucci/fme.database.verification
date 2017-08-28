using Fme.Library.Enums;
using System;
using System.Collections.Generic;
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
        public static bool IsEqual(string left, string right, ComparisonTypeEnum compareType, OperatorEnums @operator, string ignoreChars, int leftZone, int rightZone)
        {


            right = string.IsNullOrEmpty(right) ? string.Empty : right;
            left = string.IsNullOrEmpty(left) ? string.Empty : left;
            ignoreChars = string.IsNullOrEmpty(ignoreChars) ? string.Empty : ignoreChars;

            ignoreChars.ToList().ForEach(@char => right = right.Replace(@char, ' '));
            ignoreChars.ToList().ForEach(@char => left = left.Replace(@char, ' '));

            if (@operator == OperatorEnums.In)
                return CompareIn(left, right, compareType);

            if (@operator == OperatorEnums.Table)
                return CompareTable(left, right, compareType, leftZone, rightZone);

            if (@operator == OperatorEnums.Equals)
                return CompareEqual(left, right, compareType, leftZone, rightZone);

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
        /// Compares the tabe.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <param name="compareType">Type of the compare.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        private static bool CompareTable(string left, string right, ComparisonTypeEnum compareType, int leftZone, int rightZone)
        {
            List<string> date1 = new List<string>();
            List<string> date2 = new List<string>();

            var list1 = left.Split(new char[] { '|' }, StringSplitOptions.None);
            var list2 = right.Split(new char[] { '|' }, StringSplitOptions.None);
            if (compareType == ComparisonTypeEnum.Datetime)
            {
                foreach (var item in list1)
                {
                    DateTime.TryParse(item, out DateTime d1);
                    date1.Add(d1.AddHours(leftZone).ToString());
                }
                foreach (var item in list2)
                {
                    DateTime.TryParse(item, out DateTime d2);
                    date2.Add(d2.AddHours(rightZone).ToString());
                }
                left = string.Join("|", date1.ToArray());
                right = string.Join("|", date2.ToArray());
                list1 = left.Split(new char[] { '|' }, StringSplitOptions.None);
                list2 = right.Split(new char[] { '|' }, StringSplitOptions.None);

            }

            var o1 = string.Join("|", list1.OrderBy(o => o));
            var o2 = string.Join("|", list2.OrderBy(o => o));
            return o1 == o2;


           // var count1 = list1.Except(list2).Count();
            //var count1 = list1.Except(list2).Count();

            //return list1.Except(list2).Count() == 0 && list2.Except(list1).Count() == 0;            
        }
        /// <summary>
        /// Compares the equal.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <param name="compareType">Type of the compare.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        private static bool CompareEqual(string left, string right, ComparisonTypeEnum compareType, int leftZone, int rightZone)
        {
            var list = right.Split(new char[] { '|' }, StringSplitOptions.None);
            if (compareType == ComparisonTypeEnum.Datetime)
                return CompareDateTime(left, right, leftZone, rightZone);
            else if (compareType == ComparisonTypeEnum.Date)
                return CompareDate(left, right);
            else if (compareType == ComparisonTypeEnum.Integer)
                return CompareInteger(left, right);
            
                
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

            throw new NotImplementedException("Compare option not implimented.");
            return left == right;
        }

        /// <summary>
        /// Compares the date time.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        private static bool CompareDateTime(string left, string right, int leftZone, int rightZone)
        {
            DateTimeCompare dti = new DateTimeCompare();
            left = dti.Parse(left, leftZone);
            right = dti.Parse(right, rightZone);
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
            left = dti.Parse(left, 0);
            right = dti.Parse(right, 0);
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
            left = dti.Parse(left, 0);

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

        /// <summary>
        /// Compares the date.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        private static bool CompareDate(string left, string right)
        {
            DateCompare dti = new DateCompare();
            left = dti.Parse(left);
            right = dti.Parse(right);
            return left == right;            
        }

        /// <summary>
        /// Compares the integer.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public static bool CompareInteger(string left, string right)
        {
            try
            {
                if (string.IsNullOrEmpty(left) && string.IsNullOrEmpty(right))
                    return true;

                var l = (int)Math.Floor((float)Convert.ChangeType(left, typeof(float)));
                var r = (int)Math.Floor((float)Convert.ChangeType(right, typeof(float)));

                return l == r;
            }
            catch(Exception)
            {
                return false;
            }

        }

    }
}
