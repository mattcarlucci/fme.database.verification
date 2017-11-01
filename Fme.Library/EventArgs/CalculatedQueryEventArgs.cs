// ***********************************************************************
// Assembly         : Fme.Library
// Author           : mcarlucci
// Created          : 10-31-2017
//
// Last Modified By : mcarlucci
// Last Modified On : 10-31-2017
// ***********************************************************************
// <copyright file="CalculatedQueryEventArgs.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
using Fme.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fme.Librar
{
    /// <summary>
    /// Class CalculatedQueryEventArgs.
    /// </summary>
    /// <seealso cref="System.EventArgs" />
    public class CalculatedQueryEventArgs : EventArgs
    {
        /// <summary>
        /// Gets or sets the query.
        /// </summary>
        /// <value>The query.</value>
        public QueryMessageModel Query { get; set; }
        /// <summary>
        /// Initializes a new instance of the <see cref="CalculatedQueryEventArgs"/> class.
        /// </summary>
        public CalculatedQueryEventArgs()
        {

        }
        /// <summary>
        /// Initializes a new instance of the <see cref="CalculatedQueryEventArgs"/> class.
        /// </summary>
        /// <param name="query">The query.</param>
        public CalculatedQueryEventArgs(QueryMessageModel query)
        {
            this.Query = query;
        }
    }
}
