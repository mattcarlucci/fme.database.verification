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
            return string.Join(" or ", GetInClauses(field, inValues));
        }
        /// <summary>
        /// Creates the in clause.
        /// </summary>
        /// <param name="field">The field.</param>
        /// <param name="inValues">The in values.</param>
        /// <returns>System.String.</returns>
        public string CreateInClause(string field, string[] inValues)
        {
            if (inValues[0].Contains("where"))
                return inValues[0];

            return string.Join(" or ", GetInClauses(field, inValues));
        }

        /// <summary>
        /// Gets the in clauses.
        /// </summary>
        /// <param name="field">The field.</param>
        /// <param name="inValues">The in values.</param>
        /// <returns>System.String[].</returns>
        public string[] GetInClauses(string field, int[] inValues)
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
            return @in2.ToArray();
        }

        /// <summary>
        /// Gets the default SQL.
        /// </summary>
        /// <param name="tableName">Name of the table.</param>
        /// <returns>System.String.</returns>
        public virtual string GetDefaultSql(string tableName)
        {
            return string.Format("Select * from [{0}]", tableName);
        }

        /// <summary>
        /// Gets the in clauses.
        /// </summary>
        /// <param name="field">The field.</param>
        /// <param name="inValues">The in values.</param>
        /// <returns>System.String[].</returns>
        public string[] GetInClauses(string field, string[] inValues)
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
            return @in2.ToArray();
        }
     

        
        /// <summary>
        /// Builds the field aliases.
        /// </summary>
        /// <param name="fields">The fields.</param>
        /// <param name="alias">The alias.</param>
        /// <returns>System.String.</returns>
        protected virtual string BuildFieldAliasesX(string[] fields, string alias)
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
        /// Builds the field aliases.
        /// </summary>
        /// <param name="fields">The fields.</param>
        /// <param name="alias">The alias.</param>
        /// <returns>System.String.</returns>
        protected virtual string BuildFieldAliases(string[] fields, string alias)
        {
            List<string> formatted = new List<string>();
            fields.ToList().ForEach(item =>
            {
                var x = item.Contains("[") == false
                    ? "[" + item + "]"
                    : item;

                var y = item.Contains("[") == false
                    ? "[" + item + "] as [" + alias + "_" + item + "]"
                    : item;
                if (string.IsNullOrEmpty(alias))
                    formatted.Add(x);
                else formatted.Add(y);
            });            
             return Environment.NewLine + " " + string.Join("\r\n,", formatted) + Environment.NewLine;
           
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
        /// Builds the SQL.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="fields">The fields.</param>
        /// <param name="strings">The strings.</param>
        /// <returns>System.String.</returns>
        public virtual string BuildSql(DataSourceModel source, string[] fields, string[] inValues, string filter)
        {
            if (inValues == null)
                return BuildSql(source.Key, fields, source.SelectedTable, string.Empty, source.MaxRows, filter);

            if (source.KeyType == typeof(string) || source.KeyType == typeof(DateTime) || source.KeyType == null)
                return BuildSql(source.Key, fields, source.SelectedTable, string.Empty, source.MaxRows, source.Key, inValues, filter);

            var ints = Array.ConvertAll(inValues, int.Parse);
            return BuildSql(source.Key, fields, source.SelectedTable, string.Empty, source.MaxRows, source.Key, ints, filter);
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
        public virtual string BuildSql(string primaryKey, string[] fields, string tableName, string aliasPrefix, string maxRows, string inField, string[] inValues, string filter )
        {           
            if (inValues == null)            
                return BuildSql(primaryKey, fields, tableName, aliasPrefix, maxRows, filter);
                            
            var aliases = BuildFieldAliases(fields, aliasPrefix);
            var inClause = BuildInValues(inField, inValues.Distinct().ToArray());

            //var clauses = GetInClauses(inField, inValues.Distinct().ToArray());
            //List<string> sqls = new List<string>();
            //foreach(var clause in clauses)
            //{
            //    sqls.Add(string.Format("select {0} as primary_key, {1} from [{2}] where {3}", primaryKey, aliases, tableName, clause));
            //}

            //if (clauses.Count() > 0 )
            //    return string.Join(";", sqls);

            return string.Format("select {0} as primary_key, {1} from [{2}] where {3}", primaryKey, aliases, tableName, inClause);

        
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
        public virtual string BuildSql(string primaryKey, string[] fields, string tableName, string aliasPrefix, string maxRows, string inField, int[] inValues, string filter)
        {
            if (inValues == null)
                return BuildSql(primaryKey, fields, tableName, aliasPrefix, maxRows, filter);

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
        public virtual string BuildSql(string primaryKey, string[] fields, string tableName, string aliasPrefix, string maxRows, string filter)
        {           
            var aliases = BuildFieldAliases(fields, aliasPrefix);
            filter = string.IsNullOrEmpty(filter) ? "" : " AND " + filter;
            return string.Format("select {0} as primary_key, {1} from [{2}] where {0} IS NOT NULL {3}", primaryKey, aliases, tableName, filter);
        }
        
        /// <summary>
        /// Builds the SQL having.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="target">The target.</param>
        public virtual void BuildSqlHaving(DataTable source, DataTable target)
        {

        }

    }
}
