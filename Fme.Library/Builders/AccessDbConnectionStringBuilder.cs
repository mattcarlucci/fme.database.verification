// ***********************************************************************
// Assembly         : Fme.Library
// Author           : mcarlucci
// Created          : 08-16-2017
//
// Last Modified By : mcarlucci
// Last Modified On : 08-16-2017
// ***********************************************************************
// <copyright file="AccessDbConnectionStringBuilder.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fme.Library
{


    /// <summary>
    /// Class AccessDbConnectionStringBuilder.
    /// </summary>
    /// <seealso cref="Fme.Library.AceDbConnectionStringBuilder" />
    public class AccessDbConnectionStringBuilder : AceDbConnectionStringBuilder
    {
        public AccessDbConnectionStringBuilder()
        {

        }
        /// <summary>
        /// Initializes a new instance of the <see cref="AccessDbConnectionStringBuilder"/> class.
        /// </summary>
        /// <param name="file">The file.</param>
        public AccessDbConnectionStringBuilder(string file) : base(file)
        {
        }
    }
}
