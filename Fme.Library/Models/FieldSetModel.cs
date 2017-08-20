using Fme.Common.Enums;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fme.Library.Models
{
 
    [Serializable]
    public class FieldSetModel
    {
        public bool Selected { get; set; }
        public string LeftSide { get; set; }
        public string RightSide { get; set; }
        public ComparisonTypeEnum CompareType { get; set; }
        public OperatorEnums Operator { get; set; }
        public string IgnoreChars { get; set; }

        #region Execution Status
        public string Errors { get; set; }
        public string Status { get; set; }
        public string Compare { get { return "Compare"; } }      
        public string Selection { get; set; }
        public DateTime StartTime { get; set; }
        #endregion

        public FieldSetModel()
        {
            Selected = true;
            CompareType = ComparisonTypeEnum.String;
            Operator = OperatorEnums.Equals;
        }

        
    }  
   
}
