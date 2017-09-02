// ***********************************************************************
// Assembly         : Fme.Library
// Author           : mcarlucci
// Created          : 08-21-2017
//
// Last Modified By : mcarlucci
// Last Modified On : 09-02-2017
// ***********************************************************************
// <copyright file="DataTableEventArgs.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
using Fme.Library.Models;
using System;
using System.Collections.Generic;
using System.Data;

namespace Fme.Library.Repositories
{
    /// <summary>
    /// Class DataTableEventArgs.
    /// </summary>
    /// <seealso cref="System.EventArgs" />
    public class DataTableEventArgs : EventArgs
    {

        /// <summary>
        /// Gets or sets the table.
        /// </summary>
        /// <value>The table.</value>
        public DataTable Table { get; set; }
        /// <summary>
        /// Gets or sets the message.
        /// </summary>
        /// <value>The message.</value>
        public string Message { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="DataTableEventArgs"/> class.
        /// </summary>
        /// <param name="table">The table.</param>
        public DataTableEventArgs(DataTable table)
        {
            this.Table = table;
        }
    }

 
}