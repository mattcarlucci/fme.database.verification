#define NO_PARA
using System;

namespace Fme.Library.Models
{
    [Serializable]
    public class CompareResultModel 
    {
        public string PrimaryKey { get; set; }
        
        public string LeftSide { get; set; }
        public string RightSide { get; set; }
        public string LeftValue { get; set; }
        public string RightValue { get; set; }
        public int Row { get; set; }
        public CompareResultModel(string primaryKey, string leftSide, string rightSide, string left, string right, int row)
        {
            PrimaryKey = primaryKey;             
            LeftSide = leftSide;
            RightSide = rightSide;
            LeftValue = left;
            RightValue = right;
            Row = row;
        }
        public CompareResultModel()
        {

        }
                   
        public override int GetHashCode()
        {
            return (LeftValue + RightValue).GetHashCode();
        }
    }

}
