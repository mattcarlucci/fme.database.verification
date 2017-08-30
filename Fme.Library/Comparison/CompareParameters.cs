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
using System.Linq;

namespace Fme.Library.Comparison
{
    /// <summary>
    /// Class CompareParameters.
    /// </summary>
    public class CompareParameters
    {
        /// <summary>
        /// Gets or sets the exception.
        /// </summary>
        /// <value>The exception.</value>
        public Exception Exception { get; set; }
        /// <summary>
        /// Gets or sets the parameters.
        /// </summary>
        /// <value>The parameters.</value>
        public List<object> Parameters { get; set; }
        /// <summary>
        /// Gets or sets the operator.
        /// </summary>
        /// <value>The operator.</value>
        public OperatorEnums Operator { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="CompareParameters" /> class.
        /// </summary>
        /// <param name="operator">The operator.</param>
        public CompareParameters(OperatorEnums @operator)
        {
            this.Operator = @operator;
            Parameters = new List<object>();            
        }
        /// <summary>
        /// Gets the exception. Then resets to null.
        /// </summary>
        /// <returns>System.String.</returns>
        public string GetException()
        {
            var exception = string.Format("{0}", Exception);
            this.Exception = null;
            return exception;
        }
        /// <summary>
        /// Adds the specified values.
        /// </summary>
        /// <param name="values">The values.</param>
        public void Add(params object[] values)
        {
            values.ToList().ForEach(value => Parameters.Add(value));
        }
        /// <summary>
        /// Gets the offset1.
        /// </summary>
        /// <value>The offset1.</value>
        public int Offset1
        {
            get { return (int?)Parameters[0] ?? 0; }
        }
        /// <summary>
        /// Gets the offset2.
        /// </summary>
        /// <value>The offset2.</value>
        public int Offset2
        {
            get { return (int?)Parameters[1] ?? 0; }
        }
        /// <summary>
        /// Gets the dictionary1.
        /// </summary>
        /// <value>The dictionary1.</value>
        public Dictionary<string,string> Dictionary1
        {
            get { return (Dictionary<string, string>)Parameters[2] ?? new Dictionary<string, string>(); }
        }
        /// <summary>
        /// Gets the dictionary2.
        /// </summary>
        /// <value>The dictionary2.</value>
        public Dictionary<string, string> Dictionary2
        {
            get { return (Dictionary<string, string>)Parameters[3] ?? new Dictionary<string, string>(); }
        }
    }
   
   
}
