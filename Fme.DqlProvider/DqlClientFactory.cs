// ***********************************************************************
// Assembly         : DqlProvider
// Author           : mcarlucci
// Created          : 08-12-2017
//
// Last Modified By : mcarlucci
// Last Modified On : 08-12-2017
// ***********************************************************************
// <copyright file="DqlProviderFactory.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fme.DqlProvider
{
    /// <summary>
    /// Class DqlProviderFactory.
    /// </summary>
    /// <seealso cref="System.Data.Common.DbProviderFactory" />
    public sealed class DqlClientFactory : DbProviderFactory
    {
        public static readonly DqlClientFactory Instance = new DqlClientFactory();

        /// <summary>
        /// Prevents a default instance of the <see cref="DqlClientFactory"/> class from being created.
        /// </summary>
        private DqlClientFactory()
        {

        }
        /// <summary>
        /// Returns a new instance of the provider's class that implements the <see cref="T:System.Data.Common.DbCommand" /> class.
        /// </summary>
        /// <returns>A new instance of <see cref="T:System.Data.Common.DbCommand" />.</returns>
        public override DbCommand CreateCommand()
        {
            return new DqlCommand();
        }
        /// <summary>
        /// Returns a new instance of the provider's class that implements the <see cref="T:System.Data.Common.DbConnectionStringBuilder" /> class.
        /// </summary>
        /// <returns>A new instance of <see cref="T:System.Data.Common.DbConnectionStringBuilder" />.</returns>
        public override DbConnectionStringBuilder CreateConnectionStringBuilder()
        {
            return new DqlConnectionStringBuilder();
        }
        /// <summary>
        /// Returns a new instance of the provider's class that implements the <see cref="T:System.Data.Common.DbConnection" /> class.
        /// </summary>
        /// <returns>A new instance of <see cref="T:System.Data.Common.DbConnection" />.</returns>
        public override DbConnection CreateConnection()
        {
            return new DqlConnection();
        }
        /// <summary>
        /// Returns a new instance of the provider's class that implements the <see cref="T:System.Data.Common.DbDataAdapter" /> class.
        /// </summary>
        /// <returns>A new instance of <see cref="T:System.Data.Common.DbDataAdapter" />.</returns>
        public override DbDataAdapter CreateDataAdapter()
        {            
            return new DqlDataAdapter();
        }
        /// <summary>
        /// Returns a new instance of the provider's class that implements the <see cref="T:System.Data.Common.DbParameter" /> class.
        /// </summary>
        /// <returns>A new instance of <see cref="T:System.Data.Common.DbParameter" />.</returns>
        /// <exception cref="NotImplementedException"></exception>
        public override DbParameter CreateParameter()
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// Returns a new instance of the provider's class that implements the provider's version of the <see cref="T:System.Security.CodeAccessPermission" /> class.
        /// </summary>
        /// <param name="state">One of the <see cref="T:System.Security.Permissions.PermissionState" /> values.</param>
        /// <returns>A <see cref="T:System.Security.CodeAccessPermission" /> object for the specified <see cref="T:System.Security.Permissions.PermissionState" />.</returns>
        /// <exception cref="NotImplementedException"></exception>
        public override System.Security.CodeAccessPermission CreatePermission(System.Security.Permissions.PermissionState state)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// Returns a new instance of the provider's class that implements the <see cref="T:System.Data.Common.DbCommandBuilder" /> class.
        /// </summary>
        /// <returns>A new instance of <see cref="T:System.Data.Common.DbCommandBuilder" />.</returns>
        /// <exception cref="NotImplementedException"></exception>
        public override DbCommandBuilder CreateCommandBuilder()
        {
            throw new NotImplementedException();
        }


    }

}
