// ***********************************************************************
// Assembly         : DqlProvider
// Author           : Matt.Carlucci
// Created          : 08-12-2017
//
// Last Modified By : Matt.Carlucci
// Last Modified On : 08-12-2017
// ***********************************************************************
// <copyright file="DqlDataType.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
using Documentum.Interop.DFC;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fme.DqlProvider
{
/// <summary>
/// Class DqlDataType.
/// </summary>
/// <seealso cref="System.Collections.Generic.Dictionary{Documentum.Interop.DFC.tagDfValueTypes,System.Func{System.String,DataColumn}}" />
    public class DqlDataType : Dictionary<tagDfValueTypes, Func<string, DataColumn>>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DocumentumDataType" /> class.
        /// </summary>
        /// <returns>DocumentumDataType.</returns>
        public DqlDataType()
        {
            Add(tagDfValueTypes.DF_STRING, (field) => new DataColumn(field, typeof(string)));
            Add(tagDfValueTypes.DF_ID, (field) => new DataColumn(field, typeof(string)));
            Add(tagDfValueTypes.DF_TIME, (field) => new DataColumn(field, typeof(string)));
            Add(tagDfValueTypes.DF_DOUBLE, (field) => new DataColumn(field, typeof(double)));
            Add(tagDfValueTypes.DF_INTEGER, (field) => new DataColumn(field, typeof(int)));
            Add(tagDfValueTypes.DF_BOOLEAN, (field) => new DataColumn(field, typeof(bool)));
        }

    }
}
