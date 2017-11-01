// ***********************************************************************
// Assembly         : Fme.Library
// Author           : mcarlucci
// Created          : 08-18-2017
//
// Last Modified By : mcarlucci
// Last Modified On : 08-19-2017
// ***********************************************************************
// <copyright file="DqlQueryBuilder.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using Fme.Library.Models;

namespace Fme.Library
{
    public class DqlQueryBuilderExtended : QueryBuilder
    {
        public string PrimaryKey { get; set; }
        public string[] Fields { get; set; }
        public string Alias { get; set; }
        public string TableName { get; set; }
        public int MaxRows { get; set; }
        public string InField { get; set; }
        public Type InFieldType { get; set; }
        public string[] InValues { get; set; }
        public Dictionary<string, string> ExtendedProperties { get; set; }
        public void AddProperty(string key, string value)
        {
            //example would be "deleted" would tell us to add this to the query string after the table name
            //(return_top, 10)
            //(deleted, true);
            ExtendedProperties.Add(key, value);
        }
        public DqlQueryBuilderExtended()
        {
            ExtendedProperties = new Dictionary<string, string>();
        }
    }
    /// <summary>
    /// Class DqlQueryBuilder.
    /// </summary>
    /// <seealso cref="Fme.Library.QueryBuilder" />
    public class DqlQueryBuilder : QueryBuilder
    {

        /// <summary>
        /// Builds the field aliases.
        /// </summary>
        /// <param name="fields">The fields.</param>
        /// <param name="alias">The alias.</param>
        /// <returns>System.String.</returns>
        protected override string BuildFieldAliases(string[] fields, string alias)
        {
            if (string.IsNullOrEmpty(alias))
                return string.Join("\r\n,", fields);
            else
                return string.Join("\r\n,", fields.
                    Select(s => s + " as " + alias + "_" + s));
        }

        public override string BuildSql(DataSourceModel source, string[] fields, string[] strings, string filter)
        {
            return base.BuildSql(source, fields, strings, filter);
        }
        /// <summary>
        /// Builds the SQL in.
        /// </summary>
        /// <param name="primaryKey">The primary key.</param>
        /// <param name="fields">The fields.</param>
        /// <param name="tableName">Name of the table.</param>
        /// <param name="aliasPrefix">The alias prefix.</param>
        /// <param name="inField">The in field.</param>
        /// <param name="inValues">The in values.</param>
        /// <returns>System.String.</returns>
        public override string BuildSql(string primaryKey, string[] fields, string tableName, string aliasPrefix, string maxRows, string inField, string[] inValues, string filter)
        {
            //var _filter = string.IsNullOrEmpty(filter) ? "" : " AND " + filter;

            if (inValues == null)
                return BuildSql(primaryKey, fields.ToArray(), tableName, aliasPrefix, maxRows, filter);

            var aliases = BuildFieldAliases(fields, aliasPrefix);
            var inCaluse = BuildInValues(inField, inValues);

            string enabletop = "";
            var _tableName = IncludeVersion ? tableName + " (all) " : tableName;

            var clauses = GetInClauses(inField, inValues.Distinct().ToArray());
            List<string> sqls = new List<string>();
            foreach (var clause in clauses)
            {
                sqls.Add(string.Format("select r_object_id, {0} as primary_key, {1} from {2} where {3} {4}", 
                    primaryKey, aliases, _tableName, clause, enabletop));
            }
            
            if (sqls.Count() == 0)
                return string.Format("select r_object_id, {0} as primary_key, {1} from {2} where {3} {4}", 
                    primaryKey, aliases, _tableName, inCaluse, enabletop);

            return string.Join(";", sqls); 

         
        }
        /// <summary>
        /// Builds the SQL in.
        /// </summary>
        /// <param name="primaryKey">The primary key.</param>
        /// <param name="fields">The fields.</param>
        /// <param name="tableName">Name of the table.</param>
        /// <param name="aliasPrefix">The alias prefix.</param>
        /// <param name="inField">The in field.</param>
        /// <param name="inValues">The in values.</param>
        /// <returns>System.String.</returns>
        public override string BuildSql(string primaryKey, string[] fields, string tableName, string aliasPrefix, string maxRows, string inField, int[] inValues, string filter)
        {
            if (inValues == null)
                return BuildSql(primaryKey, fields.Distinct().ToArray(), tableName, aliasPrefix, maxRows, filter);

            var aliases = BuildFieldAliases(fields, aliasPrefix);
            var inCaluse = BuildInValues(inField, inValues);
          
            var enabletop = "";
            tableName = IncludeVersion ? tableName + " (all) " : tableName;

            return string.Format("select r_object_id, {0} as primary_key, {1} from {2} where {3} {4}", primaryKey, aliases, tableName, inCaluse, enabletop);
        }
        /// <summary>
        /// Builds the SQL.
        /// </summary>
        /// <param name="primaryKey">The primary key.</param>
        /// <param name="fields">The fields.</param>
        /// <param name="tableName">Name of the table.</param>
        /// <param name="aliasPrefix">The alias prefix.</param>
        /// <returns>System.String.</returns>
        public override string BuildSql(string primaryKey, string[] fields, string tableName, string aliasPrefix, string maxRows, string filter)
        {
            filter = string.IsNullOrEmpty(filter) ? "" : " WHERE " + filter;

            var aliases = BuildFieldAliases(fields.Distinct().ToArray(), aliasPrefix);
                       
            tableName = IncludeVersion ? tableName + " (all) " : tableName;

            string enabletop = string.Format("enable(return_top {0})", maxRows ?? "0");
            enabletop = !string.IsNullOrEmpty(maxRows) || int.Parse(maxRows ?? "0") > 0 ? enabletop : "";

            return string.Format("select r_object_id, {0} as primary_key, {1} from {2} {3} {4}", primaryKey, aliases, tableName, filter, enabletop);
        }
    }
}
