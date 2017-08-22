using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fme.Library.Enums;

namespace Fme.Library.Models
{
    [Serializable]
    public class DataSourceModel
    {
        public string Name { get; set; }
        public string Key { get; set; }
        
        public List<TableSchemaModel> TableSchemas {get;set;}

        public bool IsRandom { get; set; }
        public string MaxRows { get; set; }        
        public string IdList { get; set; }
        public string IdListFile { get; set; }
        public string SelectedTable { get; set; }

        public bool IncludeAllVersion { get; set; }
        public bool IncludeDeletedVersions { get; set; }
        public int TimeZoneOffset { get; set; }

        public DataSourceBase DataSource { get; set; }

        public DataSourceModel(DataSourceBase dataSource)
        {
            DataSource = DataSource;            
            TableSchemas = dataSource.GetSchemaModel();
        }
        public DataSourceModel()
        {
            TableSchemas = new List<TableSchemaModel>();
        }
        public void LoadSchema()
        {
            TableSchemas = DataSource.GetSchemaModel();
        }
        public TableSchemaModel SelectedSchema()
        {
            return TableSchemas.Where(w => w.TableName == SelectedTable).FirstOrDefault();
        }

      
    }
}
