// ***********************************************************************
// Assembly         : DqlProvider
// Author           : Matt.Carlucci
// Created          : 08-12-2017
//
// Last Modified By : Matt.Carlucci
// Last Modified On : 08-12-2017
// ***********************************************************************
// <copyright file="DqlConnectionStringBuilder.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fme.DqlProvider
{
    /// <summary>
    /// Class DqlConnectionStringBuilder.
    /// </summary>
    /// <seealso cref="System.Data.Common.DbConnectionStringBuilder" />
    public class DqlConnectionStringBuilder : DbConnectionStringBuilder
    {


        /// <summary>
        /// Initializes a new instance of the <see cref="DqlConnectionStringBuilder"/> class.
        /// </summary>
        public DqlConnectionStringBuilder()
        {
            this.Add("Provider", "DqlDataProvider");
            this.Add("User ID", null);
            this.Add("Password", null);
            this.Add("Data Source", null);
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="DqlConnectionStringBuilder"/> class.
        /// </summary>
        /// <param name="connectionString">The connection string.</param>
        public DqlConnectionStringBuilder(string connectionString)
        {
            this.ConnectionString = connectionString;
        }
        /// <summary>
        /// Gets or sets the provider.
        /// </summary>
        /// <value>The provider.</value>
        public string Provider { get; set;}

        /// <summary>
        /// Gets or sets the data source.
        /// </summary>
        /// <value>The data source.</value>
        public string DataSource { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="DqlConnectionStringBuilder"/> class.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="password">The password.</param>
        /// <param name="repository">The repository.</param>
        public DqlConnectionStringBuilder(string user, string password, string repository)
        {
           // Provider = "Dql.Client.Provider";
            DataSource = repository;

            this.Add("Provider", "DqlDataProvider");
            this.Add("User ID", user);
            this.Add("Password", password);
            this.Add("Data Source", repository);
            this.Add("Extended Properties", "");
            
           // this.Add("User", user);
           // this.Add("Repository", repository);            
        }
        
        /// <summary>
        /// Gets the user.
        /// </summary>
        /// <value>The user.</value>
        public String UserId
        {
            get { return this.ContainsKey("User") ? this["User"] as string : this["User Id"] as string; }
        }
        /// <summary>
        /// Gets the password.
        /// </summary>
        /// <value>The password.</value>
        public String Password
        {
            get { return this["Password"] as string; }
        }
        /// <summary>
        /// Gets the repository.
        /// </summary>
        /// <value>The repository.</value>
        public String Repository
        {
            get { return this.ContainsKey("Data Source") ? this["Data Source"] as string : this["Repository"] as string; }
        }
        

    }
}
