// ***********************************************************************
// Assembly         : DqlProvider
// Author           : Matt.Carlucci
// Created          : 08-12-2017
//
// Last Modified By : Matt.Carlucci
// Last Modified On : 08-12-2017
// ***********************************************************************
// <copyright file="DqlDataFormatter.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
using Documentum.Interop.DFC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Fme.DqlProvider
{
    public class DqlDataTypeMap : Dictionary<int, Type>
    {
        public DqlDataTypeMap()
        {
            Add((int)tagDfValueTypes.DF_STRING, typeof(string));
            Add((int)tagDfValueTypes.DF_ID, typeof(string));
            Add((int)tagDfValueTypes.DF_TIME, typeof(DateTime));
            Add((int)tagDfValueTypes.DF_DOUBLE, typeof(double));
            Add((int)tagDfValueTypes.DF_INTEGER, typeof(int));
            Add((int)tagDfValueTypes.DF_BOOLEAN, typeof(bool));
        }
    }

    /// <summary>
    /// Class DqlReader.
    /// </summary>
    /// <seealso cref="System.Collections.Generic.Dictionary{Documentum.Interop.DFC.tagDfValueTypes,System.Func{Documentum.Interop.DFC.IDfAttr,Documentum.Interop.DFC.IDfCollection,System.Object}}" />
    public class DqlReader : Dictionary<tagDfValueTypes, Func<IDfAttr, IDfCollection, object>>
    {
        public string RepeatingToken { get; set; }
        /// <summary>
        /// Initializes a new instance of the <see cref="DocumentumFormatter" /> class.
        /// </summary>
        /// <returns>DocumentumFormatter.</returns>
        public DqlReader(string repeatingToken = "|")
        {
            RepeatingToken = repeatingToken;

            Add(tagDfValueTypes.DF_STRING, (attr, collection) => GetString(attr, collection));
            Add(tagDfValueTypes.DF_ID, (attr, collection) => GetString(attr, collection));
            Add(tagDfValueTypes.DF_TIME, (attr, collection) => GetTime(attr, collection));
            Add(tagDfValueTypes.DF_DOUBLE, (attr, collection) => GetDouble(attr, collection));
            Add(tagDfValueTypes.DF_INTEGER, (attr, collection) => GetInteger(attr, collection));
            Add(tagDfValueTypes.DF_BOOLEAN, (attr, collection) => GetBoolean(attr, collection));

        }

        /// <summary>
        /// Gets the boolean.
        /// </summary>
        /// <param name="attr">The attribute.</param>
        /// <param name="collection">The collection.</param>
        /// <returns>System.Object.</returns>
        private object GetBoolean(IDfAttr attr, IDfCollection collection)
        {
            if (attr.isRepeating())
                return GetString(attr, collection);

            return collection.getBoolean(attr.getName());
        }

        /// <summary>
        /// Gets the integer.
        /// </summary>
        /// <param name="attr">The attribute.</param>
        /// <param name="collection">The collection.</param>
        /// <returns>System.Object.</returns>
        private object GetInteger(IDfAttr attr, IDfCollection collection)
        {
            if (attr.isRepeating())
                return GetString(attr, collection);

            return collection.getInt(attr.getName());
        }

        /// <summary>
        /// Gets the double.
        /// </summary>
        /// <param name="attr">The attribute.</param>
        /// <param name="collection">The collection.</param>
        /// <returns>System.Object.</returns>
        private object GetDouble(IDfAttr attr, IDfCollection collection)
        {
            if (attr.isRepeating())
                return GetString(attr, collection);

            return collection.getDouble(attr.getName());
        }
        
        /// <summary>
        /// Gets the time.
        /// </summary>
        /// <param name="attr">The attribute.</param>
        /// <param name="collection">The collection.</param>
        /// <returns>System.Object.</returns>
        private object GetTime(IDfAttr attr, IDfCollection collection)
        {
            if (attr.isRepeating())
                return GetString(attr, collection);

            return collection.getTime(attr.getName()).toString();
        
        }


        /// <summary>
        /// Gets the string.
        /// </summary>
        /// <param name="attr">The attribute.</param>
        /// <param name="collection">The collection.</param>
        /// <returns>System.Object.</returns>
        private object GetString(IDfAttr attr, IDfCollection collection)
        {      
            
            if (attr.isRepeating())
                return GetAllRepeatingStrings(attr.getName(), collection, RepeatingToken);
            
            return collection.getString(attr.getName());
        }

        /// <summary>
        /// Gets all repeating strings.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="attr">The attribute.</param>
        /// <param name="collection">The collection.</param>
        /// <returns>System.Object.</returns>
        public object GetAllRepeatingStrings(string name, IDfCollection collection, string token)
        {   
            List<string> items = new List<string>();
            for (int index = 0; index < collection.getValueCount(name); index++)
            {
                items.Add(collection.getRepeatingString(name,index).ToString());
            }            
            return string.Join(RepeatingToken, items.ToArray());
        }

        /// <summary>
        /// Merges the specified seperator.
        /// </summary>
        /// <param name="items">The items.</param>
        /// <param name="seperator">The seperator.</param>
        /// <returns>System.String.</returns>
        public static string Merge(object[] items, string seperator = "|")
        {
            return string.Join(seperator, items);
        }

    }
}
