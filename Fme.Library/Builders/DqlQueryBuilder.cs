﻿// ***********************************************************************
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
namespace Fme.Library
{
    /// <summary>
    /// Class DqlQueryBuilder.
    /// </summary>
    /// <seealso cref="Fme.Library.QueryBuilder" />
    public class DqlQueryBuilder : QueryBuilder
    {
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
        public override string BuildSql(string primaryKey, string[] fields, string tableName, string aliasPrefix, string maxRows, string inField, string[] inValues)
        {
            if (inValues == null)
                return BuildSql(primaryKey, fields, tableName, aliasPrefix, maxRows);

            var aliases = BuildFieldAliases(fields, aliasPrefix);
            var inCaluse = BuildInValues(inField, inValues);

            //string enabletop = string.Format("enable(return_top {0})", maxRows);
            //enabletop = !string.IsNullOrEmpty(maxRows) || int.Parse(maxRows) > 0 ? maxRows : "";
            var enabletop = "";

            return string.Format("select r_object_id, {0} as primary_key, {1} from {2} where {3} {4}", primaryKey, aliases, tableName, inCaluse, enabletop);

           /// var key = primaryKey == "r_object_id" ? "," : ", " + primaryKey + " as primary_key,";

            //hack to get dql to return the correct records.
           // return string.Format("select r_object_id{0} {1} from {2} where {3}", key,  aliases, tableName, inCaluse);
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
        public override string BuildSql(string primaryKey, string[] fields, string tableName, string aliasPrefix, string maxRows, string inField, int[] inValues)
        {
            if (inValues == null)
                return BuildSql(primaryKey, fields, tableName, aliasPrefix, maxRows);

            var aliases = BuildFieldAliases(fields, aliasPrefix);
            var inCaluse = BuildInValues(inField, inValues);

            //hack to get dql to return the correct records.
            /// string key = primaryKey == "r_object_id" ? " as primary_key," : ", " + primaryKey + " as primary_key,";
            // return string.Format("select r_object_id{0} {1} from {2} where {3}", key, aliases, tableName, inCaluse);

            //string enabletop = string.Format("enable(return_top {0})", maxRows);
            //enabletop = !string.IsNullOrEmpty(maxRows) || int.Parse(maxRows) > 0 ? maxRows : "";
            var enabletop = "";

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
        public override string BuildSql(string primaryKey, string[] fields, string tableName, string aliasPrefix, string maxRows)
        {
            var aliases = BuildFieldAliases(fields, aliasPrefix);

            // string key = primaryKey == "r_object_id" ? " as primary_key," : ", " + primaryKey + " as primary_key,";
            // return string.Format("select r_object_id{0} {1} from {2}", key, aliases, tableName);

            //string enabletop = string.Format("enable(return_top {0})", maxRows);
            //enabletop = !string.IsNullOrEmpty(maxRows) || int.Parse(maxRows) > 0 ? maxRows : "";
            var enabletop = "";
            return string.Format("select r_object_id, {0} as primary_key, {1} from {2} {3}", primaryKey, aliases, tableName, enabletop);
        }
    }
}