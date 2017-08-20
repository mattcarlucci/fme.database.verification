using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fme.Library.Models
{
    [Serializable]
    public class TableSchemaModel
    {
        public string TableName { get;set;}
        public List<FieldSchemaModel> Fields { get; set; }

        public string GetSqlFields(string prefix)
        {
            return string.Join(",", Fields.Select(s => s.GetSqlFields(prefix)));
        }

        public TableSchemaModel()
        {
            Fields = new List<FieldSchemaModel>();
        }
      
       
    }
}
