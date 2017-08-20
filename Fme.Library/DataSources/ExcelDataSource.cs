// ***********************************************************************
// Assembly         : Fme.Library
// Author           : mcarlucci
// Created          : 08-16-2017
//
// Last Modified By : mcarlucci
// Last Modified On : 08-16-2017
// ***********************************************************************
// <copyright file="OleDbDataSource.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************

using System.Data.Common;

namespace Fme.Library
{
    public class ExcelDataSource : OleDbDataSource
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ExcelDataSource"/> class.
        /// </summary>
        public ExcelDataSource()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ExcelDataSource"/> class.
        /// </summary>
        /// <param name="connectionString">The connection string.</param>
        public ExcelDataSource(string connectionString) : base(connectionString)
        {
        }

        
        /// <summary>
        /// Gets the connection string builder.
        /// </summary>
        /// <returns>System.Data.Common.DbConnectionStringBuilder.</returns>
        public override DbConnectionStringBuilder GetConnectionStringBuilder()
        {
            var builder = new ExcelDbConnectionStringBuilder();
            ((DbConnectionStringBuilder)builder).ConnectionString = this.ConnectionString;
            return builder;

        }
        /// <summary>
        /// Gets the query builder.
        /// </summary>
        /// <returns>QueryBuilder.</returns>
        public override QueryBuilder GetQueryBuilder()
        {
            return new QueryBuilder();
        }
        
    }
}
