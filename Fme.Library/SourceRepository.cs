using Fme.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fme.Library
{
    //public class DataSourceRepository
    //{        
    //    public DataSource DataSource { get; set; }
    //    public List<TableSchemaModel> Schema { get; set; }

    //}
    //public class ComparisoneModel
    //{       
    //   public string Description { get; set; }
    //   // CompareModel
    //}
    //public class CompareSide
    //{
    //    public string Side { get; set; }
    //    public string Field { get; set; }
    //    public bool UseLookup { get; set; }
    //    public string LookupFile { get; set; }

    //    public bool UseQuery { get; set; }
    //    public string Query { get; set; }

    //    public string Alias { get { return Side + "_" + Field; } }

    //    public string SqlField
    //    {
    //        get
    //        {
    //            if (string.IsNullOrEmpty(Field)) return string.Empty;
    //            return string.Format("[{0}] as [{1}]", Field, Alias);
    //        }
    //    }
    //}

    //[Serializable]
    //public class CompareMappingModel
    //{

    //    public bool Selected { get; set; }

    //    public CompareSide LeftSide { get; set; }
    //    public CompareSide RightSide { get; set; }               

    //    public Common.Enums.ComparisonTypeEnum CompareType { get; set; }
    //    public string IgnoreChars { get; set; }
    //    public string Errors { get; set; }
    //    public string Status { get; set; }
        
    //    public Common.Enums.OperatorEnums Operator { get; set; }
    //    public string Selection { get; set; }
    //    public decimal Ordinal { get; set; }
    //    public DateTime? StartTime { get; set; }

    //    public List<Common.CompareResults> CompareResults { get; set; }
    //    public bool IsCalculated { get; set; }
       
                
    //    public CompareMappingModel()
    //    {
    //        CompareResults = new List<Common.CompareResults>();
    //        Selected = true;
    //        CompareType = Common.Enums.ComparisonTypeEnum.String;
    //        Operator = Common.Enums.OperatorEnums.Equals;
    //        LeftSide = new CompareSide() { Side = "left" };
    //        RightSide = new CompareSide() { Side = "right" };
    //    }
    //    public CompareMappingModel(string left, string right)
    //        : this()
    //    {
    //        LeftSide.Field = left;
    //        RightSide.Field = right;
    //    }
              

    //    public bool IsPair()
    //    {
    //        return (!string.IsNullOrEmpty(LeftSide.Field) && !string.IsNullOrEmpty(RightSide.Field));
    //    }

    //}
}
