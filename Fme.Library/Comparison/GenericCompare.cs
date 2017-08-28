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
using Fme.Library.Enums;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Globalization;
using System.Reflection;
using Fme.Library.Extensions;

namespace Fme.Library.Comparison
{


    /// <summary>
    /// Class GenericCompare.
    /// </summary>
    /// <seealso cref="System.Collections.Generic.Dictionary{Fme.Library.Enums.ComparisonTypeEnum, System.Func{System.String, System.String, Fme.Library.Enums.OperatorEnums, Fme.Library.Comparison.CompareParameters, System.Boolean}}" />
    /// <seealso cref="System.Collections.Generic.Dictionary{Fme.Library.Enums.ComparisonTypeEnum, System.Func{System.String, System.String, Fme.Library.Enums.OperatorEnums, System.Object, System.Object, System.Boolean}}" />
    public class GenericCompare : Dictionary<ComparisonTypeEnum, Func<string, string, OperatorEnums, CompareParameters, bool>>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GenericCompare" /> class.
        /// </summary>
        public GenericCompare()
        {
            Add(ComparisonTypeEnum.Date, (left, right, ops, parms) => 
                CompareDate(left, right, ops, parms));

            Add(ComparisonTypeEnum.Datetime, (left, right, ops, parms) =>
               CompareDateTime(left, right, ops, parms));

            Add(ComparisonTypeEnum.String, (left, right, ops, parms) =>
              CompareString(left, right, ops, parms));

            Add(ComparisonTypeEnum.Integer, (left, right, ops, parms) =>
                CompareInteger(left, right, ops, parms));

            Add(ComparisonTypeEnum.Float, (left, right, ops, parms) =>
                CompareFloat(left, right, ops, parms));

            Add(ComparisonTypeEnum.Boolean, (left, right, ops, parms) =>
              CompareBoolean(left, right, ops, parms));
        }

        /// <summary>
        /// Executes the specified l.
        /// </summary>
        /// <param name="l">The l.</param>
        /// <param name="r">The r.</param>
        /// <param name="ops">The ops.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        private bool Execute(string l, string r, OperatorEnums ops)
        {
            Type type = l.GetType();
            MethodInfo method = l.GetType().GetMethods().
                Where(w => w.Name == Enum.GetName(typeof(OperatorEnums), ops)).FirstOrDefault();

            return (bool)method.Invoke(l, new object[] { r });
        }


        /// <summary>
        /// Compares the date time.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <param name="ops">The ops.</param>
        /// <param name="parms">The parms.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public bool CompareDateTime(string left, string right, OperatorEnums ops,  CompareParameters parms)
        {
            try
            {
                //var fmt = "ddd, dd MMM yyyy HH':'mm':'ss 'GMT' (*)";
                //var dta = "2015/01/22 12:08:51 (GMT+09:00)";
                //DateTime.TryParseExact(dta, fmt, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime result);

                var x = new DateTimeConverter();
                var l = x.Transform(left, parms.Offset1);
                var r = x.Transform(right, parms.Offset2);
                return Execute(l, r, ops);
            }
            catch(Exception ex)
            {
                parms.Exception = ex;
                return false;
            }
        }

        /// <summary>
        /// Compares the date.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <param name="ops">The ops.</param>
        /// <param name="parms">The parms.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public bool CompareDate(string left, string right, OperatorEnums ops, CompareParameters parms)
        {
            try
            {                
                var x = new DateConverter();
                var l = x.Transform(left, parms.Offset1);
                var r = x.Transform(right, parms.Offset2);
                return Execute(l, r, ops);
            }
            catch (Exception ex)
            {
                parms.Exception = ex;
                return false;
            }
        }

        /// <summary>
        /// Compares the integer.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <param name="ops">The ops.</param>
        /// <param name="parms">The parms.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public bool CompareInteger(string left, string right, OperatorEnums ops, CompareParameters parms)
        {
            try
            {
                var x = new IntegerConverter();
                var l = x.Transform(left, parms.Offset1);
                var r = x.Transform(right, parms.Offset2);
                return Execute(l, r, ops);
               
            }
            catch (Exception ex)
            {
                parms.Exception = ex;
                return false;
            }


        }

        /// <summary>
        /// Compares the float.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <param name="ops">The ops.</param>
        /// <param name="parms">The parms.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public bool CompareFloat(string left, string right, OperatorEnums ops, CompareParameters parms)
        {
            try
            {
                var x = new FloatConverter();
                var l = x.Transform(left, parms.Offset1);
                var r = x.Transform(right, parms.Offset2);
                return Execute(l, r, ops);
            }
            catch (Exception ex)
            {
                parms.Exception = ex;
                return false;
            }



        }

        /// <summary>
        /// Compares the boolean.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <param name="ops">The ops.</param>
        /// <param name="parms">The parms.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public bool CompareBoolean(string left, string right, OperatorEnums ops, CompareParameters parms)
        {
            try
            {
                var x = new BooleanConverter();
                var l = x.Transform(left, parms.Offset1);
                var r = x.Transform(right, parms.Offset2);
                return Execute(l, r, ops);
            }
            catch (Exception ex)
            {
                parms.Exception = ex;
                return false;
            }

        }

        /// <summary>
        /// Compares the string.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <param name="ops">The ops.</param>
        /// <param name="parms">The parms.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public bool CompareString(string left, string right, OperatorEnums ops,  CompareParameters parms)
        {

            try
            {
                var x = new StringConverter();
                var l = x.Transform(left, parms.Dictionary1);
                var r = x.Transform(right, parms.Dictionary1);
                return Execute(l, r, ops);
            }
            catch (Exception ex)
            {
                parms.Exception = ex;
                return false;
            }
        }
        
        
    }
   
   
}
