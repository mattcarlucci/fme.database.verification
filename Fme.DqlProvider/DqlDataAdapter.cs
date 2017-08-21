// ***********************************************************************
// Assembly         : DqlProvider
// Author           : Matt.Carlucci
// Created          : 08-12-2017
//
// Last Modified By : Matt.Carlucci
// Last Modified On : 08-12-2017
// ***********************************************************************
// <copyright file="DqlDataAdapter.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************

using Documentum.Interop.DFC;

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Fme.DqlProvider
{
    /// <summary>
    /// Class DqlDataAdapter.
    /// </summary>
    /// <seealso cref="System.Data.Common.DbDataAdapter" />
    /// <seealso cref="System.Data.IDbDataAdapter" />
    /// <seealso cref="System.ICloneable" />
    /// <seealso cref="System.Data.IDataAdapter" />
    public class DqlDataAdapter : DbDataAdapter, IDbDataAdapter, IDataAdapter, ICloneable                                
    {
        /// <summary>
        /// The command
        /// </summary>
        DqlCommand command;
       

        /// <summary>
        /// Initializes a new instance of the <see cref="DqlDataAdapter" /> class.
        /// </summary>
        public DqlDataAdapter()
        {
                    }

        /// <summary>
        /// Initializes a new instance of the <see cref="DqlDataAdapter" /> class.
        /// </summary>
        /// <param name="selectCommand">The select command.</param>
        public DqlDataAdapter(DqlCommand selectCommand)
        {
            this.command = selectCommand;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DqlDataAdapter" /> class.
        /// </summary>
        /// <param name="selectCommandText">The select command text.</param>
        /// <param name="selectConnection">The select connection.</param>
        public DqlDataAdapter(string selectCommandText, DqlConnection selectConnection)
        {
            this.command = new DqlCommand(selectCommandText, selectConnection)
            {
                CommandText = selectCommandText,
                CommandType = CommandType.Text
            };
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DqlDataAdapter" /> class.
        /// </summary>
        /// <param name="selectCommandText">The select command text.</param>
        /// <param name="selectConnectionString">The select connection string.</param>
        public DqlDataAdapter(string selectCommandText, string selectConnectionString)
        {
            this.command = new DqlCommand(selectCommandText)
            {
                CommandText = selectCommandText,
                CommandType = CommandType.Text
            };
        }

        /// <summary>
        /// Fills the specified data set.
        /// </summary>
        /// <param name="dataSet">The data set.</param>
        /// <param name="token">The token.</param>
        /// <returns>System.Int32.</returns>
        public int Fill(DataSet dataSet, CancellationToken token)
        {
            DqlReader formatter = new DqlReader();

            List<DqlCommand> commands = new List<DqlCommand>();
            command.CommandText.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries)
                .ToList().ForEach(commandText => commands.Add(new DqlCommand(commandText, (DqlConnection)command.Connection)));

            foreach (var cmd in commands)
            {
                if (token.IsCancellationRequested)
                    throw new OperationCanceledException("A cancellation token associated with this operation was canceled");

                DqlConnection cn = cmd.Connection;
                IDfCollection records = cn.ExecuteQuery(cmd);
                DataTable table = null;

                #region Row Iterator
                while (records.next())
                {
                    if (token.IsCancellationRequested)
                        throw new OperationCanceledException("A cancellation token associated with this operation was canceled");

                    CreateTable(ref table, records);
                    IDfTypedObject typedObj = records.getTypedObject();

                    DataRow row = table.NewRow();
                    table.Rows.Add(row);

                    #region Column Iterator
                    int count = records.getAttrCount();
                    for (int i = 0; i < count; i++)
                    {
                        var collection = records;
                        IDfAttr attr = collection.getAttr(i);
                        string field = attr.getName();

                        if (formatter.ContainsKey((tagDfValueTypes)attr.getDataType()))
                            row[field] = formatter[(tagDfValueTypes)attr.getDataType()](attr, collection);
                    }
                    #endregion
                }
                #endregion

                records.close();

                if (table != null)
                    dataSet.Tables.Add(table);
            }

            commands.ForEach(item => item.Dispose());
            return 1;
        }
        /// <summary>
        /// Adds or refreshes rows in the <see cref="T:System.Data.DataSet" />.
        /// </summary>
        /// <param name="dataSet">A <see cref="T:System.Data.DataSet" /> to fill with records and, if necessary, schema.</param>
        /// <returns>The number of rows successfully added to or refreshed in the <see cref="T:System.Data.DataSet" />. This does not include rows affected by statements that do not return rows.</returns>
        /// <PermissionSet>
        ///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
        ///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
        ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
        /// </PermissionSet>
        public override int Fill(DataSet dataSet)
        {
            return Fill(dataSet, new CancellationToken());
        }

        /// <summary>
        /// Creates the table.
        /// </summary>
        /// <param name="table">The table.</param>
        /// <param name="collection">The collection.</param>
        private void CreateTable(ref DataTable table, IDfCollection collection)
        {
            if (table != null) return;
            table = new DataTable();

            DqlDataType converter = new DqlDataType();
            IDfTypedObject typedObj = collection.getTypedObject();

            int count = collection.getAttrCount();
            for (int i = 0; i < count; i++)
            {
                IDfAttr attr = collection.getAttr(i);
                string field = attr.getName();

                tagDfValueTypes dataType = attr.isRepeating() 
                    ? tagDfValueTypes.DF_STRING 
                    : (tagDfValueTypes)attr.getDataType();

                if (converter.ContainsKey(dataType))
                    table.Columns.Add(converter[dataType](field));
            }
        }

        /// <summary>
        /// Fills the schema.
        /// </summary>
        /// <param name="dataSet">The data set.</param>
        /// <returns>DataTable.</returns>
        public DataSet FillSchema(DataSet dataSet)
        {
            Fill(dataSet);
            return dataSet;
        }

        
        #region Not Implemented
        /// <summary>
        /// Adds or refreshes rows in a specified range in the <see cref="T:System.Data.DataSet" /> to match those in the data source using the <see cref="T:System.Data.DataSet" /> and <see cref="T:System.Data.DataTable" /> names.
        /// </summary>
        /// <param name="dataSet">A <see cref="T:System.Data.DataSet" /> to fill with records.</param>
        /// <param name="srcTable">A string indicating the name of the source table.</param>
        /// <param name="dataReader">An instance of <see cref="T:System.Data.IDataReader" />.</param>
        /// <param name="startRecord">The zero-based index of the starting record.</param>
        /// <param name="maxRecords">An integer indicating the maximum number of records.</param>
        /// <returns>The number of rows successfully added to or refreshed in the <see cref="T:System.Data.DataSet" />. This does not include rows affected by statements that do not return rows.</returns>
        /// <exception cref="System.NotImplementedException"></exception>
        protected override int Fill(DataSet dataSet, string srcTable, IDataReader dataReader, int startRecord, int maxRecords)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// Adds or refreshes rows in a specified range in the <see cref="T:System.Data.DataSet" /> to match those in the data source using the <see cref="T:System.Data.DataSet" /> and source table names, command string, and command behavior.
        /// </summary>
        /// <param name="dataSet">A <see cref="T:System.Data.DataSet" /> to fill with records and, if necessary, schema.</param>
        /// <param name="startRecord">The zero-based record number to start with.</param>
        /// <param name="maxRecords">The maximum number of records to retrieve.</param>
        /// <param name="srcTable">The name of the source table to use for table mapping.</param>
        /// <param name="command">The SQL SELECT statement used to retrieve rows from the data source.</param>
        /// <param name="behavior">One of the <see cref="T:System.Data.CommandBehavior" /> values.</param>
        /// <returns>The number of rows successfully added to or refreshed in the <see cref="T:System.Data.DataSet" />. This does not include rows affected by statements that do not return rows.</returns>
        /// <exception cref="System.NotImplementedException"></exception>
        protected override int Fill(DataSet dataSet, int startRecord, int maxRecords, string srcTable, IDbCommand command, CommandBehavior behavior)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// Adds or refreshes rows in the <see cref="T:System.Data.DataTable" /> to match those in the data source using the <see cref="T:System.Data.DataTable" /> name and the specified <see cref="T:System.Data.IDataReader" />.
        /// </summary>
        /// <param name="dataTable">A <see cref="T:System.Data.DataTable" /> to fill with records.</param>
        /// <param name="dataReader">An instance of <see cref="T:System.Data.IDataReader" />.</param>
        /// <returns>The number of rows successfully added to or refreshed in the <see cref="T:System.Data.DataTable" />. This does not include rows affected by statements that do not return rows.</returns>
        /// <exception cref="System.NotImplementedException"></exception>
        protected override int Fill(DataTable dataTable, IDataReader dataReader)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// Adds or refreshes rows in a <see cref="T:System.Data.DataTable" /> to match those in the data source using the specified <see cref="T:System.Data.DataTable" />, <see cref="T:System.Data.IDbCommand" /> and <see cref="T:System.Data.CommandBehavior" />.
        /// </summary>
        /// <param name="dataTable">A <see cref="T:System.Data.DataTable" /> to fill with records and, if necessary, schema.</param>
        /// <param name="command">The SQL SELECT statement used to retrieve rows from the data source.</param>
        /// <param name="behavior">One of the <see cref="T:System.Data.CommandBehavior" /> values.</param>
        /// <returns>The number of rows successfully added to or refreshed in the <see cref="T:System.Data.DataTable" />. This does not include rows affected by statements that do not return rows.</returns>
        /// <exception cref="System.NotImplementedException"></exception>
        protected override int Fill(DataTable dataTable, IDbCommand command, CommandBehavior behavior)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// Adds or refreshes rows in a specified range in the collection of <see cref="T:System.Data.DataTable" /> objects to match those in the data source.
        /// </summary>
        /// <param name="dataTables">A collection of <see cref="T:System.Data.DataTable" /> objects to fill with records.</param>
        /// <param name="dataReader">An instance of <see cref="T:System.Data.IDataReader" />.</param>
        /// <param name="startRecord">The zero-based index of the starting record.</param>
        /// <param name="maxRecords">An integer indicating the maximum number of records.</param>
        /// <returns>The number of rows successfully added to or refreshed in the <see cref="T:System.Data.DataTable" />. This does not include rows affected by statements that do not return rows.</returns>
        /// <exception cref="System.NotImplementedException"></exception>
        protected override int Fill(DataTable[] dataTables, IDataReader dataReader, int startRecord, int maxRecords)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// Adds or refreshes rows in a specified range in the <see cref="T:System.Data.DataSet" /> to match those in the data source using the <see cref="T:System.Data.DataSet" /> and <see cref="T:System.Data.DataTable" /> names.
        /// </summary>
        /// <param name="dataTables">The <see cref="T:System.Data.DataTable" /> objects to fill from the data source.</param>
        /// <param name="startRecord">The zero-based record number to start with.</param>
        /// <param name="maxRecords">The maximum number of records to retrieve.</param>
        /// <param name="command">The <see cref="T:System.Data.IDbCommand" /> executed to fill the <see cref="T:System.Data.DataTable" /> objects.</param>
        /// <param name="behavior">One of the <see cref="T:System.Data.CommandBehavior" /> values.</param>
        /// <returns>The number of rows added to or refreshed in the data tables.</returns>
        /// <exception cref="System.NotImplementedException"></exception>
        protected override int Fill(DataTable[] dataTables, int startRecord, int maxRecords, IDbCommand command, CommandBehavior behavior)
        {
            throw new NotImplementedException();
        }


        /// <summary>
        /// Adds a <see cref="T:System.Data.DataTable" /> named "Table" to the specified <see cref="T:System.Data.DataSet" /> and configures the schema to match that in the data source based on the specified <see cref="T:System.Data.SchemaType" />.
        /// </summary>
        /// <param name="dataSet">A <see cref="T:System.Data.DataSet" /> to insert the schema in.</param>
        /// <param name="schemaType">One of the <see cref="T:System.Data.SchemaType" /> values that specify how to insert the schema.</param>
        /// <returns>A reference to a collection of <see cref="T:System.Data.DataTable" /> objects that were added to the <see cref="T:System.Data.DataSet" />.</returns>
        /// <exception cref="System.NotImplementedException"></exception>
        /// <PermissionSet>
        ///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
        ///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
        ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
        /// </PermissionSet>
        public override DataTable[] FillSchema(DataSet dataSet, SchemaType schemaType)
        {
            Fill(dataSet);

            List<DataTable> tables = new List<DataTable>();
            foreach(DataTable table in dataSet.Tables)
            {
                tables.Add(table);
            }
            return tables.ToArray();
        }
        /// <summary>
        /// Adds a <see cref="T:System.Data.DataTable" /> to the specified <see cref="T:System.Data.DataSet" /> and configures the schema to match that in the data source based on the specified <see cref="T:System.Data.SchemaType" />.
        /// </summary>
        /// <param name="dataSet">The <see cref="T:System.Data.DataSet" /> to be filled with the schema from the data source.</param>
        /// <param name="schemaType">One of the <see cref="T:System.Data.SchemaType" /> values.</param>
        /// <param name="command">The SQL SELECT statement used to retrieve rows from the data source.</param>
        /// <param name="srcTable">The name of the source table to use for table mapping.</param>
        /// <param name="behavior">One of the <see cref="T:System.Data.CommandBehavior" /> values.</param>
        /// <returns>An array of <see cref="T:System.Data.DataTable" /> objects that contain schema information returned from the data source.</returns>
        /// <exception cref="System.NotImplementedException"></exception>
        protected override DataTable[] FillSchema(DataSet dataSet, SchemaType schemaType, IDbCommand command, string srcTable, CommandBehavior behavior)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// Adds a <see cref="T:System.Data.DataTable" /> to the specified <see cref="T:System.Data.DataSet" />.
        /// </summary>
        /// <param name="dataSet">The <see cref="T:System.Data.DataTable" /> to be filled from the <see cref="T:System.Data.IDataReader" />.</param>
        /// <param name="schemaType">One of the <see cref="T:System.Data.SchemaType" /> values.</param>
        /// <param name="srcTable">The name of the source table to use for table mapping.</param>
        /// <param name="dataReader">The <see cref="T:System.Data.IDataReader" /> to be used as the data source when filling the <see cref="T:System.Data.DataTable" />.</param>
        /// <returns>A reference to a collection of <see cref="T:System.Data.DataTable" /> objects that were added to the <see cref="T:System.Data.DataSet" />.</returns>
        /// <exception cref="System.NotImplementedException"></exception>
        protected override DataTable[] FillSchema(DataSet dataSet, SchemaType schemaType, string srcTable, IDataReader dataReader)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// Adds a <see cref="T:System.Data.DataTable" /> to the specified <see cref="T:System.Data.DataSet" />.
        /// </summary>
        /// <param name="dataTable">The <see cref="T:System.Data.DataTable" /> to be filled from the <see cref="T:System.Data.IDataReader" />.</param>
        /// <param name="schemaType">One of the <see cref="T:System.Data.SchemaType" /> values.</param>
        /// <param name="dataReader">The <see cref="T:System.Data.IDataReader" /> to be used as the data source when filling the <see cref="T:System.Data.DataTable" />.</param>
        /// <returns>A <see cref="T:System.Data.DataTable" /> object that contains schema information returned from the data source.</returns>
        /// <exception cref="System.NotImplementedException"></exception>
        protected override DataTable FillSchema(DataTable dataTable, SchemaType schemaType, IDataReader dataReader)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// Configures the schema of the specified <see cref="T:System.Data.DataTable" /> based on the specified <see cref="T:System.Data.SchemaType" />, command string, and <see cref="T:System.Data.CommandBehavior" /> values.
        /// </summary>
        /// <param name="dataTable">The <see cref="T:System.Data.DataTable" /> to be filled with the schema from the data source.</param>
        /// <param name="schemaType">One of the <see cref="T:System.Data.SchemaType" /> values.</param>
        /// <param name="command">The SQL SELECT statement used to retrieve rows from the data source.</param>
        /// <param name="behavior">One of the <see cref="T:System.Data.CommandBehavior" /> values.</param>
        /// <returns>A of <see cref="T:System.Data.DataTable" /> object that contains schema information returned from the data source.</returns>
        protected override DataTable FillSchema(DataTable dataTable, SchemaType schemaType, IDbCommand command, CommandBehavior behavior)
        {
            return base.FillSchema(dataTable, schemaType, command, behavior);
        }
        /// <summary>
        /// Creates a new <see cref="T:System.Data.Common.DataTableMappingCollection" />.
        /// </summary>
        /// <returns>A new table mapping collection.</returns>
        /// <exception cref="System.NotImplementedException"></exception>
        protected override DataTableMappingCollection CreateTableMappings()
        {
            throw new NotImplementedException();
        }

        #endregion

        /// <summary>
        /// Releases the unmanaged resources used by the <see cref="T:System.Data.Common.DbDataAdapter" /> and optionally releases the managed resources.
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

    }
}
