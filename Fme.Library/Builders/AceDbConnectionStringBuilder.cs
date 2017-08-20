// ***********************************************************************
// Assembly         : Fme.Library
// Author           : mcarlucci
// Created          : 08-16-2017
//
// Last Modified By : mcarlucci
// Last Modified On : 08-16-2017
// ***********************************************************************
// <copyright file="AceDbConnectionStringBuilder.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.Data.Common;

namespace Fme.Library
{
    /// <summary>
    /// Class AceDbConnectionStringBuilder.
    /// </summary>
    /// <seealso cref="System.Data.Common.DbConnectionStringBuilder" />
    public class AceDbConnectionStringBuilder : DbConnectionStringBuilder
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AceDbConnectionStringBuilder"/> class.
        /// </summary>
        public AceDbConnectionStringBuilder() : base()
        {
            this["Provider"] = "Microsoft.ACE.OLEDB.12.0";
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AceDbConnectionStringBuilder"/> class.
        /// </summary>
        /// <param name="file">The file.</param>
        public AceDbConnectionStringBuilder(string file) : this()
        {            
            this["Data Source"] = file;           
        }
    }
}
