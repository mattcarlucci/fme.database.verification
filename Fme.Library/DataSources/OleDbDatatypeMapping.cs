using Fme.Library.Enums;
using System;
using System.Collections.Generic;

namespace Fme.Library
{
    /// <summary>
    /// Class OleDbDataTypeMapping.
    /// </summary>
    /// <seealso cref="System.Collections.Generic.Dictionary{Fme.Library.Enums.DataTypeEnums, System.Type}" />
    public class OleDbDataTypeMapping : Dictionary<DataTypeEnums, Type>
    {
        /// <summary>
        /// The singleton
        /// </summary>
        private static readonly OleDbDataTypeMapping _singleton = new OleDbDataTypeMapping();

        /// <summary>
        /// Gets the instance.
        /// </summary>
        /// <value>The instance.</value>
        public static OleDbDataTypeMapping Instance
        {
            get { return _singleton; }
        }
        private OleDbDataTypeMapping()
        {
            #region Types
            Add(DataTypeEnums.adBinary, typeof(object));
            Add(DataTypeEnums.adBoolean, typeof(bool));
            Add(DataTypeEnums.adBSTR, typeof(string));
            Add(DataTypeEnums.adChapter, typeof(string));
            Add(DataTypeEnums.adChar, typeof(string));
            Add(DataTypeEnums.adCurrency, typeof(decimal));
            Add(DataTypeEnums.adDate, typeof(DateTime));
            Add(DataTypeEnums.adDBDate, typeof(DateTime));
            Add(DataTypeEnums.adDBFileTime, typeof(DateTime));
            Add(DataTypeEnums.adDBTime, typeof(DateTime));
            Add(DataTypeEnums.adDBTimeStamp, typeof(DateTime));
            Add(DataTypeEnums.adDecimal, typeof(decimal));
            Add(DataTypeEnums.adDouble, typeof(double));
            Add(DataTypeEnums.adEmpty, typeof(object));
            Add(DataTypeEnums.adError, typeof(object));
            Add(DataTypeEnums.adFileTime, typeof(DateTime));
            Add(DataTypeEnums.adGUID, typeof(string));
            Add(DataTypeEnums.adIDispatch, typeof(object));
            Add(DataTypeEnums.adInteger, typeof(int));
            Add(DataTypeEnums.adIUnknown, typeof(object));
            Add(DataTypeEnums.adLongVarBinary, typeof(byte[]));
            Add(DataTypeEnums.adLongVarChar, typeof(byte[]));
            Add(DataTypeEnums.adLongVarWChar, typeof(byte[]));
            Add(DataTypeEnums.adNumeric, typeof(int));
            Add(DataTypeEnums.adPropVariant, typeof(object));
            Add(DataTypeEnums.adSingle, typeof(short));
            Add(DataTypeEnums.adSmallInt, typeof(int));
            Add(DataTypeEnums.adTinyInt, typeof(short));
            Add(DataTypeEnums.adUnsignedBigInt, typeof(int));
            Add(DataTypeEnums.adUnsignedInt, typeof(int));
            Add(DataTypeEnums.adUnsignedSmallInt, typeof(int));
            Add(DataTypeEnums.adUnsignedTinyInt, typeof(int));
            Add(DataTypeEnums.adUserDefined, typeof(object));
            Add(DataTypeEnums.adVarBinary, typeof(object));
            Add(DataTypeEnums.adVarChar, typeof(string));
            Add(DataTypeEnums.adVariant, typeof(object));
            Add(DataTypeEnums.adVarNumeric, typeof(double));
            Add(DataTypeEnums.adVarWChar, typeof(string));
            Add(DataTypeEnums.adWChar, typeof(string));
            #endregion


        }

        public string GetDataType(object dataType)
        {
            DataTypeEnums type = (DataTypeEnums)dataType;

            if (Instance.ContainsKey(type))
                return Instance[type].FullName;
            else
                return typeof(string).FullName;
        }
        /// <summary>
        /// Gets the map.
        /// </summary>
        /// <returns>Dictionary&lt;DataTypeEnums, Type&gt;.</returns>
        //public static Dictionary<DataTypeEnums, Type> GetMap()
        //{
        //    #region 
        //    Dictionary<DataTypeEnums, Type> DataTypeMappings = new Dictionary<DataTypeEnums, Type>
        //    {
        //        {DataTypeEnums.adBinary,typeof(object)},
        //        {DataTypeEnums.adBoolean,typeof(bool)},
        //        {DataTypeEnums.adBSTR,typeof(string)},
        //        {DataTypeEnums.adChapter,typeof(string)},
        //        {DataTypeEnums.adChar,typeof(string)},
        //        {DataTypeEnums.adCurrency,typeof(decimal)},
        //        {DataTypeEnums.adDate,typeof(DateTime)},
        //        {DataTypeEnums.adDBDate,typeof(DateTime)},
        //        {DataTypeEnums.adDBFileTime,typeof(DateTime)},
        //        {DataTypeEnums.adDBTime,typeof(DateTime)},
        //        {DataTypeEnums.adDBTimeStamp,typeof(DateTime)},
        //        {DataTypeEnums.adDecimal,typeof(decimal)},
        //        {DataTypeEnums.adDouble,typeof(double)},
        //        {DataTypeEnums.adEmpty,typeof(object)},
        //        {DataTypeEnums.adError,typeof(object)},
        //        {DataTypeEnums.adFileTime,typeof(DateTime)},
        //        {DataTypeEnums.adGUID,typeof(string)},
        //        {DataTypeEnums.adIDispatch,typeof(object)},
        //        {DataTypeEnums.adInteger,typeof(int)},
        //        {DataTypeEnums.adIUnknown,typeof(object)},
        //        {DataTypeEnums.adLongVarBinary,typeof(byte[])},
        //        {DataTypeEnums.adLongVarChar,typeof(byte[])},
        //        {DataTypeEnums.adLongVarWChar,typeof(byte[])},
        //        {DataTypeEnums.adNumeric,typeof(int)},
        //        {DataTypeEnums.adPropVariant,typeof(object)},
        //        {DataTypeEnums.adSingle,typeof(short)},
        //        {DataTypeEnums.adSmallInt,typeof(int)},
        //        {DataTypeEnums.adTinyInt,typeof(short)},
        //        {DataTypeEnums.adUnsignedBigInt,typeof(int)},
        //        {DataTypeEnums.adUnsignedInt,typeof(int)},
        //        {DataTypeEnums.adUnsignedSmallInt,typeof(int)},
        //        {DataTypeEnums.adUnsignedTinyInt,typeof(int)},
        //        {DataTypeEnums.adUserDefined,typeof(object)},
        //        {DataTypeEnums.adVarBinary,typeof(object)},
        //        {DataTypeEnums.adVarChar,typeof(string)},
        //        {DataTypeEnums.adVariant,typeof(object)},
        //        {DataTypeEnums.adVarNumeric,typeof(double)},
        //        {DataTypeEnums.adVarWChar,typeof(string)},
        //        {DataTypeEnums.adWChar,typeof(string)}
        //    };
        //    #endregion
        //    return DataTypeMappings;
        //}

    }
}
