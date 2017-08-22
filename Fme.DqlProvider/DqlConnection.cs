// ***********************************************************************
// Assembly         : DqlProvider
// Author           : Matt.Carlucci
// Created          : 08-12-2017
//
// Last Modified By : Matt.Carlucci
// Last Modified On : 08-12-2017
// ***********************************************************************
// <copyright file="DlqConnection.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Documentum.Interop.DFC;
using System.Data.Common;
using System.Data.SqlClient;

namespace Fme.DqlProvider
{
    /// <summary>
    /// Class DlqConnection.
    /// </summary>
    /// <seealso cref="System.Data.Common.DbConnection" />
    /// <seealso cref="System.ICloneable" />
    /// <seealso cref="System.Data.IDbConnection" />
    public class DqlConnection : DbConnection, ICloneable
    {             

        /// <summary>
        /// The state
        /// </summary>
        private ConnectionState state = ConnectionState.Closed;

        private int batchSize = 1000;
        /// <summary>
        /// The connection string
        /// </summary>
        private string connectionString;
        /// <summary>
        /// The builder
        /// </summary>
        private DqlConnectionStringBuilder builder;// = new DqlConnectionStringBuilder();
        /// <summary>
        /// The credential
        /// </summary>
        DqlCredentials credential;

        /// <summary>
        /// The clientx
        /// </summary>
        DfClientX _clientx = null;
        /// <summary>
        /// The client
        /// </summary>
        IDfClient _client = null;
        /// <summary>
        /// The session
        /// </summary>
        IDfSession _session = null;
        /// <summary>
        /// The login information object
        /// </summary>
        IDfLoginInfo _loginInfoObj = null;

        public List<string> Catalogs { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="DqlConnection"/> class.
        /// </summary>
        /// <exception cref="System.Exception">Failed creating Documentum client</exception>
        public DqlConnection()
        {
            _clientx = new DfClientX();
            _client = _clientx.getLocalClient();
            if (_client == null)
                throw new Exception("Failed creating Documentum client");

            IDfDocbaseMap map = _client.getDocbaseMap();
            Catalogs = new List<string>();
            int count = map.getDocbaseCount();

            for (int i = 0; i < count; i++)
            {
                Catalogs.Add(map.getDocbaseName(i));
            }
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="DqlConnection"/> class.
        /// </summary>
        /// <param name="builder">The builder.</param>
        public DqlConnection(DqlConnectionStringBuilder builder)
            : this()
        {
            this.builder = builder;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DqlConnection"/> class.
        /// </summary>
        /// <param name="connectionString">The connection string.</param>
        /// <param name="credential">The credential.</param>
        public DqlConnection(string connectionString, DqlCredentials credential)
            : this(connectionString)
        {
            this.credential = credential;
            this.connectionString = connectionString;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DqlConnection"/> class.
        /// </summary>
        /// <param name="connectionString">The connection string.</param>
        public DqlConnection(string connectionString)
            : this()
        {
            this.connectionString = connectionString;
        }

        /// <summary>
        /// Starts a database transaction.
        /// </summary>
        /// <param name="isolationLevel">Specifies the isolation level for the transaction.</param>
        /// <returns>An object representing the new transaction.</returns>
        /// <exception cref="System.NotImplementedException"></exception>
        protected override DbTransaction BeginDbTransaction(IsolationLevel isolationLevel)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Changes the current database for an open connection.
        /// </summary>
        /// <param name="databaseName">Specifies the name of the database for the connection to use.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        public override void ChangeDatabase(string databaseName)
        {
            builder = new DqlConnectionStringBuilder(connectionString)
            {
                ["Repository"] = Database
            };
            connectionString = builder.ConnectionString;
        }

        /// <summary>
        /// Closes the connection to the database. This is the preferred method of closing any open connection.
        /// </summary>
        public override void Close()
        {
            _session = null;
            _loginInfoObj = null;
            _clientx = null;
            _client = null;
        }

        /// <summary>
        /// Gets or sets the size of the batch.
        /// </summary>
        /// <value>The size of the batch.</value>
        public int BatchSize
        {
            get { return batchSize; }
            set { batchSize = value; }
        }
        /// <summary>
        /// Gets or sets the string used to open the connection.
        /// </summary>
        /// <value>The connection string.</value>
        public override string ConnectionString
        {
            get
            {
                return connectionString;
            }
            set
            {
                connectionString = value;
                if (builder == null)
                    builder = new DqlConnectionStringBuilder(value);
                else
                    builder.ConnectionString = value;
            }
        }

        /// <summary>
        /// Creates and returns a <see cref="T:System.Data.Common.DbCommand" /> object associated with the current connection.
        /// </summary>
        /// <returns>A <see cref="T:System.Data.Common.DbCommand" /> object.</returns>
        protected override DbCommand CreateDbCommand()
        {
            return new DqlCommand();
        }
        /// <summary>
        /// Creates the DQL command.
        /// </summary>
        /// <returns>DqlCommand.</returns>
        public DqlCommand CreateDqlCommand()
        {
            return new DqlCommand();
            //DqlClientFactory.Instance.CreateCommand();
        }

        /// <summary>
        /// Gets the name of the database server to which to connect.
        /// </summary>
        /// <value>The data source.</value>
        /// <exception cref="System.NotImplementedException"></exception>
        public override string DataSource
        {
            get { throw new NotImplementedException(); }
        }

        /// <summary>
        /// Gets the name of the current database after a connection is opened, or the database name specified in the connection string before the connection is opened.
        /// </summary>
        /// <value>The database.</value>
        /// <exception cref="System.NotImplementedException"></exception>
        public override string Database
        {
            get { return builder["Repository"] as string; }
        }

        /// <summary>
        /// Opens a database connection with the settings specified by the <see cref="P:System.Data.Common.DbConnection.ConnectionString" />.
        /// </summary>
        /// <exception cref="System.Exception">Failed conecting to Documentum</exception>
        public override void Open()
        {
            if (state == ConnectionState.Open || state == ConnectionState.Connecting) return;

            state = ConnectionState.Connecting;

            if (builder == null)
            {
                builder = new DqlConnectionStringBuilder();
                builder.ConnectionString = this.ConnectionString;
            }

            _loginInfoObj = _clientx.getLoginInfo();            
            _loginInfoObj.setUser(builder.UserId);
            _loginInfoObj.setPassword(builder.Password);

            try
            {   
                // Create a new session to the requested DocBase
                _session = _client.newSession(builder.Repository, _loginInfoObj);
                if (_session == null && !_session.isConnected())
                {
                    throw new DqlSessionException("Failed conecting to Documentum");                    
                }
                state = ConnectionState.Open;
            }
            catch (Exception ex)
            {
                throw new DqlSessionException("Unable to create session. Make sure the repository is available.", ex);               
            }

        }

        /// <summary>
        /// Gets a string that represents the version of the server to which the object is connected.
        /// </summary>
        /// <value>The server version.</value>
        /// <exception cref="System.NotImplementedException"></exception>
        public override string ServerVersion
        {
            get { throw new NotImplementedException(); }
        }

        /// <summary>
        /// Gets a string that describes the state of the connection.
        /// </summary>
        /// <value>The state.</value>
        public override ConnectionState State
        {
            get { return state; }
        }

        /// <summary>
        /// Creates a new object that is a copy of the current instance.
        /// </summary>
        /// <returns>A new object that is a copy of this instance.</returns>
        public object Clone()
        {
            return this.Clone();
        }

        /// <summary>
        /// Gets the <see cref="T:System.Data.Common.DbProviderFactory" /> for this <see cref="T:System.Data.Common.DbConnection" />.
        /// </summary>
        /// <value>The database provider factory.</value>
        protected override DbProviderFactory DbProviderFactory
        {
            get
            {
                return DqlClientFactory.Instance;
            }
        }
        /// <summary>
        /// Gets the DQL database provider factory.
        /// </summary>
        /// <value>The DQL database provider factory.</value>
        public DqlClientFactory DqlDbProviderFactory
        {
            get { return DqlClientFactory.Instance; }
        }

      
        /// <summary>
        /// Releases the unmanaged resources used by the <see cref="T:System.ComponentModel.Component" /> and optionally releases the managed resources.
        /// </summary>
        /// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources.</param>
        protected override void Dispose(bool disposing)
        {            
            base.Dispose(disposing);

            if (disposing)
            {
                GC.Collect();
                GC.WaitForPendingFinalizers();
            }
        }

        /// <summary>
        /// Returns schema information for the data source of this <see cref="T:System.Data.Common.DbConnection" />.
        /// </summary>
        /// <returns>A <see cref="T:System.Data.DataTable" /> that contains schema information.</returns>
        /// <exception cref="System.NotImplementedException"></exception>
        /// <PermissionSet>
        ///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
        /// </PermissionSet>
        public override DataTable GetSchema()
        {
            DataSet ds = new DataSet("schema");
            //var select = "select distinct t.name, t.attr_name, t.attr_type, '0' as min_length,";
            //select += "t.attr_length, t.attr_repeating, a.not_null as mandatory from dm_type t,";
            //select += "dmi_dd_attr_info a where t.name = a.type_name and t.attr_name = a.attr_name  enable(row_based)";

            string select = "select distinct t.name, t.attr_name, t.attr_type, '0' as min_length, t.attr_length, t.attr_repeating, a.not_null as mandatory from dm_type t, dmi_dd_attr_info a where t.name = a.type_name and t.attr_name = a.attr_name enable(row_based)";

            using (DqlCommand cmd = new DqlCommand(select, this))
            {
                using (DqlDataAdapter adapter = new DqlProvider.DqlDataAdapter(cmd))
                {
                    adapter.Fill(ds);
                }
            }
            return ds.Tables[0];

        }

        /// <summary>
        /// Returns schema information for the data source of this <see cref="T:System.Data.Common.DbConnection" /> using the specified string for the schema name.
        /// </summary>
        /// <param name="collectionName">Specifies the name of the schema to return.</param>
        /// <returns>A <see cref="T:System.Data.DataTable" /> that contains schema information.</returns>
        /// <exception cref="System.NotImplementedException"></exception>
        /// <PermissionSet>
        ///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
        /// </PermissionSet>
        public override DataTable GetSchema(string collectionName)
        {
            var select = "select distinct t.name, t.attr_name, t.attr_type, '0' as min_length,";
            select += "t.attr_length, t.attr_repeating, a.not_null as mandatory from dm_type t,";
            select += string.Format("dmi_dd_attr_info a where t.name = a.type_name and t.attr_name = a.attr_name and t.name = '{0}' enable(row_based)", collectionName);

            DataSet ds = new DataSet(collectionName);

            using (DqlCommand cmd = new DqlCommand(select, this))
            {
                using (DqlDataAdapter adapter = new DqlProvider.DqlDataAdapter(cmd))
                {
                    adapter.Fill(ds);
                }
            }
            return ds.Tables[0];
        }

        /// <summary>
        /// This is the asynchronous version of <see cref="M:System.Data.Common.DbConnection.Open" />. Providers should override with an appropriate implementation. The cancellation token can optionally be honored.The default implementation invokes the synchronous <see cref="M:System.Data.Common.DbConnection.Open" /> call and returns a completed task. The default implementation will return a cancelled task if passed an already cancelled cancellationToken. Exceptions thrown by Open will be communicated via the returned Task Exception property.Do not invoke other methods and properties of the DbConnection object until the returned Task is complete.
        /// </summary>
        /// <param name="cancellationToken">The cancellation instruction.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        public override Task OpenAsync(System.Threading.CancellationToken cancellationToken)
        {
            var task = Task.Run(() => Open());
            task.Wait(1000 * 10, cancellationToken);
            return task;
        }

        /// <summary>
        /// Executes the query.
        /// </summary>
        /// <param name="command">The command.</param>
        /// <returns>IDfCollection.</returns>
        public IDfCollection ExecuteQuery(DqlCommand command)
        {
            if (state == ConnectionState.Closed)
                throw new System.InvalidOperationException("Invalid operation. The connection is closed.");            

            IDfQuery query = _clientx.getQuery();

            query.setDQL(command.CommandText);
            query.setBatchSize(10000);

            return query.execute(_session, (int)tagDfQueryTypes.IDfQuery_DF_READ_QUERY);
        }
    }


}
