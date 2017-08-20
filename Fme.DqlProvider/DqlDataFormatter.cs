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

namespace DqlProvider
{
/// <summary>
/// Class DqlFormatter.
/// </summary>
/// <seealso cref="System.Collections.Generic.Dictionary{Documentum.Interop.DFC.tagDfValueTypes,System.Func{Documentum.Interop.DFC.IDfAttr,Documentum.Interop.DFC.IDfCollection,System.Object}}" />
    public class DqlFormatter : Dictionary<tagDfValueTypes, Func<IDfAttr, IDfCollection, object>>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DocumentumFormatter" /> class.
        /// </summary>
        /// <returns>DocumentumFormatter.</returns>
        public DqlFormatter()
        {
            Add(tagDfValueTypes.DF_STRING, (attr, collection) => collection.getString(attr.getName()));
            Add(tagDfValueTypes.DF_ID, (attr, collection) => collection.getString(attr.getName()));
            Add(tagDfValueTypes.DF_TIME, (attr, collection) => collection.getTime(attr.getName()).toString());
            Add(tagDfValueTypes.DF_DOUBLE, (attr, collection) => collection.getDouble(attr.getName()));
            Add(tagDfValueTypes.DF_INTEGER, (attr, collection) => collection.getInt(attr.getName()));
            Add(tagDfValueTypes.DF_BOOLEAN, (attr, collection) => collection.getBoolean(attr.getName()));

        }

    }
}
