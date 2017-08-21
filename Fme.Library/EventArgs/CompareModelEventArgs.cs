// ***********************************************************************
// Assembly         : Fme.Library
// Author           : mcarlucci
// Created          : 08-19-2017
//
// Last Modified By : mcarlucci
// Last Modified On : 08-19-2017
// ***********************************************************************
// <copyright file="CompareModelEventArgs.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
using Fme.Library.Models;
using System;

namespace Fme.Library
{
    /// <summary>
    /// Class CompareModelStatusEventArgs.
    /// </summary>
    /// <seealso cref="System.EventArgs" />
    public class CompareModelStatusEventArgs : EventArgs
    {
        /// <summary>
        /// Gets or sets the status message.
        /// </summary>
        /// <value>The status message.</value>
        public string StatusMessage { get; set; }
        /// <summary>
        /// Gets or sets the data.
        /// </summary>
        /// <value>The data.</value>
        public string Data { get; set; }
        /// <summary>
        /// Gets or sets the data source.
        /// </summary>
        /// <value>The data source.</value>
        public DataSourceModel DataSource { get; set; }
    }
    
}