using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Fme.Library.Models
{
    [Serializable]
    public class ValidationMacro
    {
        public bool Selected { get; set; }
        public string Functions { get; set; }
        public ValidationMacro()
        {

        }
        public ValidationMacro(string name)
        {
            this.Functions = name;
        }
    }

    [Serializable]
    public class FieldSchemaModel
    {        
        public string Name { get; set; }
        public Int64 Ordinal { get; set; }

        public string TableName { get; set; }

        public string Type { get; set; }       
        public Int64? MaxLength { get; set; }
        public bool PrimaryKey { get; set; }
        public bool IsRepeating { get; set; }
        //public List<ValidationMacro> Validators { get; set; } = new List<ValidationMacro>();
        //private string ValidationMacros  { get;set; }

        //private string validationMacros = string.Empty;        
        //public string ValidationMacros
        //{
        //    get {       return validationMacros.Replace(Environment.NewLine, "&crlf;");      }
        //    set {       validationMacros = value.Replace(Environment.NewLine, "&crlf");      }
        //}

        /// <summary>
        /// Gets or sets the validation macros.
        /// </summary>
        /// <value>The validation macros.</value>
        public string ValidationMacros { get; set; }

        /// <summary>
        /// Gets the macros.
        /// </summary>
        /// <returns>System.String.</returns>
        public string GetMacros()
        {
            if (string.IsNullOrEmpty(ValidationMacros)) return null;

            if (ValidationMacros.Contains(Environment.NewLine))
                return ValidationMacros;

            return ValidationMacros.Replace("\n", Environment.NewLine);
        }
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
