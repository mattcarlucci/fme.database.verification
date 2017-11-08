// ***********************************************************************
// Assembly         : DqlProvider
// Author           : Matt.Carlucci
// Created          : 08-12-2017
//
// Last Modified By : Matt.Carlucci
// Last Modified On : 08-12-2017
// ***********************************************************************
// <copyright file="DqlCommand.cs" company="">
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
    /// Class DqlCommand.
    /// </summary>
    /// <seealso cref="System.Data.Common.DbCommand" />
    /// <seealso cref="System.Data.IDbCommand" />
    public class DqlCommand : DbCommand, ICloneable, IDbCommand
    {
        /// <summary>
        /// The connection
        /// </summary>
        private DbConnection connection = null;

        /// <summary>
        /// The command text
        /// </summary>
        private string commandText = string.Empty;
        /// <summary>
        /// The command timeout
        /// </summary>
        private int commandTimeout = 30;
        /// <summary>
        /// The command type
        /// </summary>
        CommandType commandType = CommandType.Text;

        /// <summary>
        /// Initializes a new instance of the <see cref="DqlCommand"/> class.
        /// </summary>
        public DqlCommand()
        {

        }
        /// <summary>
        /// Initializes a new instance of the <see cref="DqlCommand"/> class.
        /// </summary>
        /// <param name="cmdText">The command text.</param>
        public DqlCommand(string cmdText)
        {
            this.commandText = cmdText;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DqlCommand"/> class.
        /// </summary>
        /// <param name="cmdText">The command text.</param>
        /// <param name="connection">The connection.</param>
        public DqlCommand(string cmdText, DbConnection connection)
            : this(cmdText)
        {
            this.connection = connection;
            
        }

        private DqlCommand(DqlCommand from)
      : this()
    {
            this.CommandText = from.CommandText;
            this.CommandTimeout = from.CommandTimeout;
            this.CommandType = from.CommandType;
            this.connection = from.Connection;
            this.DesignTimeVisible = from.DesignTimeVisible;
            this.Transaction = from.Transaction;
            this.UpdatedRowSource = from.UpdatedRowSource;
            //this._columnEncryptionSetting = from.ColumnEncryptionSetting;
            //DqlParameterCollection parameters = this.Parameters;
            //foreach (object obj in (DbParameterCollection)from.Parameters)
            //    parameters.Add(obj is ICloneable ? (obj as ICloneable).Clone() : obj);
        }

       
        /// <summary>
        /// Gets or sets the text command to run against the data source.
        /// </summary>
        /// <value>The command text.</value>
        public override string CommandText
        {
            get
            {
                return commandText;
            }
            set
            {
                this.commandText = value;
            }
        }

        /// <summary>
        /// Gets or sets the wait time before terminating the attempt to execute a command and generating an error.
        /// </summary>
        /// <value>The command timeout.</value>
        public override int CommandTimeout
        {
            get
            {
                return commandTimeout;
            }
            set
            {
                this.commandTimeout = value;
            }
        }

        /// <summary>
        /// Indicates or specifies how the <see cref="P:System.Data.Common.DbCommand.CommandText" /> property is interpreted.
        /// </summary>
        /// <value>The type of the command.</value>
        public override CommandType CommandType
        {
            get
            {
                return this.commandType;
            }
            set
            {
                this.commandType = value;
            }
        }
        /// <summary>
        /// Releases the unmanaged resources used by the <see cref="T:System.ComponentModel.Component" /> and optionally releases the managed resources.
        /// </summary>
        /// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources.</param>
        protected override void Dispose(bool disposing)
        {
            connection = null;
            base.Dispose(disposing);            
        }

        /// <summary>
        /// Creates a new instance of a <see cref="T:System.Data.Common.DbParameter" /> object.
        /// </summary>
        /// <returns>A <see cref="T:System.Data.Common.DbParameter" /> object.</returns>
        /// <exception cref="System.NotImplementedException"></exception>
        protected override DbParameter CreateDbParameter()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Gets or sets the <see cref="T:System.Data.Common.DbConnection" /> used by this <see cref="T:System.Data.Common.DbCommand" />.
        /// </summary>
        /// <value>The database connection.</value>
        protected override DbConnection DbConnection
        {
            get
            {
                return connection;
            }
            set
            {
                connection = (DqlConnection)value;
            }
        }
        public new DqlConnection Connection
        {
            get
            {
                return (DqlConnection)connection;
            }
        }

        /// <summary>
        /// Gets the collection of <see cref="T:System.Data.Common.DbParameter" /> objects.
        /// </summary>
        /// <value>The database parameter collection.</value>
        /// <exception cref="System.NotImplementedException"></exception>
        protected override DbParameterCollection DbParameterCollection
        {
            get { throw new NotImplementedException(); }
        }

        /// <summary>
        /// Gets or sets the <see cref="P:System.Data.Common.DbCommand.DbTransaction" /> within which this <see cref="T:System.Data.Common.DbCommand" /> object executes.
        /// </summary>
        /// <value>The database transaction.</value>
        /// <exception cref="System.NotImplementedException">
        /// </exception>
        protected override DbTransaction DbTransaction
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the command object should be visible in a customized interface control.
        /// </summary>
        /// <value><c>true</c> if [design time visible]; otherwise, <c>false</c>.</value>
        /// <exception cref="System.NotImplementedException">
        /// </exception>
        public override bool DesignTimeVisible
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        /// <summary>
        /// Executes the command text against the connection.
        /// </summary>
        /// <param name="behavior">An instance of <see cref="T:System.Data.CommandBehavior" />.</param>
        /// <returns>A task representing the operation.</returns>
        /// <exception cref="System.NotImplementedException"></exception>
        protected override DbDataReader ExecuteDbDataReader(CommandBehavior behavior)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Executes a SQL statement against a connection object.
        /// </summary>
        /// <returns>The number of rows affected.</returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public override int ExecuteNonQuery()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Executes the query and returns the first column of the first row in the result set returned by the query. All other columns and rows are ignored.
        /// </summary>
        /// <returns>The first column of the first row in the result set.</returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public override object ExecuteScalar()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Creates a prepared (or compiled) version of the command on the data source.
        /// </summary>
        /// <exception cref="System.NotImplementedException"></exception>
        public override void Prepare()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Gets or sets how command results are applied to the <see cref="T:System.Data.DataRow" /> when used by the Update method of a <see cref="T:System.Data.Common.DbDataAdapter" />.
        /// </summary>
        /// <value>The updated row source.</value>
        /// <exception cref="System.NotImplementedException">
        /// </exception>
        public override UpdateRowSource UpdatedRowSource
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        /// <summary>
        /// Creates a new object that is a copy of the current instance.
        /// </summary>
        /// <returns>A new object that is a copy of this instance.</returns>
        public object Clone()
        {            
            var dqlCommand = new DqlCommand(this);
            return dqlCommand;
                
          //  return this.Clone();
        }

        /// <summary>
        /// Attempts to cancels the execution of a <see cref="T:System.Data.Common.DbCommand" />.
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        public override void Cancel()
        {
            throw new NotImplementedException();
        }
    }
}
