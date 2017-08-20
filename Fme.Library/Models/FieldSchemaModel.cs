using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Fme.Library.Models
{
    [Serializable]
    public class FieldSchemaModel
    {
        public string TableName { get; set; }
        public Int64 Ordinal { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }       
        public Int64? MaxLength { get; set; }
        public bool PrimaryKey { get; set; }
        public bool IsRepeating { get; set; }
        //public ColumnCompare CompareType {get;set;}
        //public List<CompareResults> CompareResults { get; set; }
        public FieldSchemaModel()
        {
           // CompareResults = new List<CompareResults>();
        }
        public FieldSchemaModel(string tableName, string columnName, int ordinal)
        {
            TableName = tableName.Replace("$", "").Replace("'", "");
            Name = columnName;
            Ordinal = ordinal;
           // CompareResults = new List<CompareResults>();
            // RITM0612139
        }
       
        public string GetSqlFields(string prefix)
        {
            return string.Format("[{1}] as [{0}_{1}]", prefix, Name);
        }

        public FieldSchemaModel(DataColumn col, string tableName)
        {
            Name = col.ColumnName;
            Ordinal = col.Ordinal;
            Type = col.DataType.Name;
            MaxLength = col.MaxLength;
            TableName = tableName;
        }
        
    }
}
