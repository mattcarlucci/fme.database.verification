// ***********************************************************************
// Assembly         : Fme.Library
// Author           : mcarlucci
// Created          : 08-18-2017
//
// Last Modified By : mcarlucci
// Last Modified On : 08-19-2017
// ***********************************************************************
// <copyright file="QueryBuilder.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Fme.Library.Models;
using Fme.Library.Extensions;
using Fme.Library.Repositories;

namespace Fme.Library
{
    /// <summary>
    /// Class QueryBuilder.
    /// </summary>
    public class QueryBuilder
    {
        /// <summary>
        /// Gets or sets a value indicating whether [include version].
        /// </summary>
        /// <value><c>true</c> if [include version]; otherwise, <c>false</c>.</value>
        public virtual bool IncludeVersion { get; set; }

        /// <summary>
        /// Creates the in clause.
        /// </summary>
        /// <param name="field">The field.</param>
        /// <param name="inValues">The in values.</param>
        /// <returns>System.String.</returns>
        public string CreateInClause(string field, int[] inValues)
        {
            string @in = string.Join(",", inValues.Select(s => s.ToString()));

            List<string> @in2 = new List<string>();

            string[] fields = @in.Split(new string[] { "'", "," }, StringSplitOptions.RemoveEmptyEntries);
            var blocks = fields.Split(490).ToList();

            foreach (var block in blocks)
            {
                string buffer = field + " in (" + string.Join(",", block.Select(s => s)) + ")";
                @in2.Add(buffer);
            }
            string stack = string.Join(" or ", @in2.Select(s => s));

            return stack;
        }

        /// <summary>
        /// Creates the in clause.
        /// </summary>
        /// <param name="field">The field.</param>
        /// <param name="inValues">The in values.</param>
        /// <returns>System.String.</returns>
        public string CreateInClause(string field, string[] inValues)
        {
            string @in = string.Join(",", inValues.Select(s => s));

            List<string> @in2 = new List<string>();

            string[] fields = @in.Split(new string[] { "'", "," }, StringSplitOptions.RemoveEmptyEntries);
            var blocks = fields.Split(490).ToList();

            foreach (var block in blocks)
            {
                string buffer = field + " in ('" + string.Join("','", block.Select(s => s)) + "')";
                @in2.Add(buffer);
            }
            string stack = string.Join(" or ", @in2.Select(s => s));

            return stack;
        }

        
        /// <summary>
        /// Builds the field aliases.
        /// </summary>
        /// <param name="fields">The fields.</param>
        /// <param name="alias">The alias.</param>
        /// <returns>System.String.</returns>
        protected string BuildFieldAliases(string[] fields, string alias)
        {
            List<string> formatted = new List<string>();
            fields.Where(w=> string.IsNullOrEmpty(w) == false).ToList().ForEach(item =>
            {
                var x = item.Contains(" ") && item.Contains("[") == false 
                    ? "[" + item + "]" 
                    : item;

                formatted.Add(x);              
            });

            if (formatted.Count == 0)
                return string.Empty;

            if (string.IsNullOrEmpty(alias))
                return string.Join("\r\n,", formatted);
            else
                return string.Join("\r\n,", formatted.
                    Select(s => s + " as " + alias + "_" + s));
                
        }

      

        /// <summary>
        /// Builds the in values.
        /// </summary>
        /// <param name="inField">The in field.</param>
        /// <param name="inValues">The in values.</param>
        /// <returns>System.String.</returns>
        protected string BuildInValues(string inField, string[] inValues)
        {
            return CreateInClause(inField, inValues);
        }

        /// <summary>
        /// Builds the in values.
        /// </summary>
        /// <param name="inField">The in field.</param>
        /// <param name="inValues">The in values.</param>
        /// <returns>System.String.</returns>
        protected string BuildInValues(string inField, int[] inValues)
        {
            return CreateInClause(inField, inValues);
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
        public virtual string BuildSql(string primaryKey, string[] fields, string tableName, string aliasPrefix, string maxRows, string inField, string[] inValues )
        {           
            if (inValues == null)            
                return BuildSql(primaryKey, fields, tableName, aliasPrefix, maxRows);
                
            
            var aliases = BuildFieldAliases(fields, aliasPrefix);
            var inCaluse = BuildInValues(inField, inValues.Distinct().ToArray());

            return string.Format("select {0} as primary_key, {1} from [{2}] where {3}", primaryKey, aliases, tableName, inCaluse);
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
        public virtual string BuildSql(string primaryKey, string[] fields, string tableName, string aliasPrefix, string maxRows, string inField, int[] inValues)
        {
            if (inValues == null)
                return BuildSql(primaryKey, fields, tableName, aliasPrefix, maxRows);

            var aliases = BuildFieldAliases(fields, aliasPrefix);
            var inCaluse = BuildInValues(inField, inValues);

            return string.Format("select {0} as primary_key, {1} from [{2}] where {3}", primaryKey, aliases, tableName, inCaluse);
        }

        /// <summary>
        /// Builds the SQL.
        /// </summary>
        /// <param name="primaryKey">The primary key.</param>
        /// <param name="fields">The fields.</param>
        /// <param name="tableName">Name of the table.</param>
        /// <param name="aliasPrefix">The alias prefix.</param>
        /// <returns>System.String.</returns>
        public virtual string BuildSql(string primaryKey, string[] fields, string tableName, string aliasPrefix, string maxRows)
        {           
            var aliases = BuildFieldAliases(fields, aliasPrefix);
            return string.Format("select {0} as primary_key, {1} from [{2}] where {0} IS NOT NULL", primaryKey, aliases, tableName);
        }

      

        /// <summary>
        /// Builds the SQL having.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="target">The target.</param>
        public virtual void BuildSqlHaving(DataTable source, DataTable target)
        {

        }


        public virtual string BuildSql(string primaryKey, DataField[] fields, string tableName, string aliasPrefix, string maxRows, string inField, string[] inValues)
        {
            if (inValues == null)
                return BuildSql(primaryKey, fields, tableName, aliasPrefix, maxRows);


            var aliases = string.Join(",", fields.Select(s => s.GetQualifiedName() + " as C" + s.Ordinal));
            var inCaluse = BuildInValues(inField, inValues.Distinct().ToArray());

            return string.Format("select {0} as primary_key, {1} from [{2}] where {3}", primaryKey, aliases, tableName, inCaluse);
        }
        
        public virtual string BuildSql(string primaryKey, DataField[] fields, string tableName, string aliasPrefix, string maxRows, string inField, int[] inValues)
        {
            if (inValues == null)
                return BuildSql(primaryKey, fields, tableName, aliasPrefix, maxRows);


            var aliases = string.Join(",", fields.Select(s => s.GetQualifiedName() + " as C" + s.Ordinal));
            var inCaluse = BuildInValues(inField, inValues.Distinct().ToArray());

            return string.Format("select {0} as primary_key, {1} from [{2}] where {3}", primaryKey, aliases, tableName, inCaluse);
        }

        public virtual string BuildSql(string primaryKey, DataField[] fields, string tableName, string aliasPrefix, string maxRows)
        {
            var aliases = string.Join(",", fields.Select(s => s.GetQualifiedName() + " as C" + s.Ordinal));
            return string.Format("select {0} as primary_key, {1} from [{2}] where {0} IS NOT NULL", primaryKey, aliases, tableName);
        }
    }
}
