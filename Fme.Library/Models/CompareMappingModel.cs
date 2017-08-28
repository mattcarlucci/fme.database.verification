// ***********************************************************************
// Assembly         : Fme.Library
// Author           : mcarlucci
// Created          : 08-17-2017
//
// Last Modified By : mcarlucci
// Last Modified On : 08-19-2017
// ***********************************************************************
// <copyright file="CompareMappingModel.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Fme.Library.Enums;
using System.IO;

namespace Fme.Library.Models
{

    /// <summary>
    /// Class CompareMappingModel.
    /// </summary>
    [Serializable]
    public class CompareMappingModel
    {
        
        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="CompareMappingModel"/> is selected.
        /// </summary>
        /// <value><c>true</c> if selected; otherwise, <c>false</c>.</value>
        public bool Selected { get; set; }
        /// <summary>
        /// Gets or sets the left side.
        /// </summary>
        /// <value>The left side.</value>
        public string LeftSide { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [use left lookup].
        /// </summary>
        /// <value><c>true</c> if [use left lookup]; otherwise, <c>false</c>.</value>
        public bool UseLeftLookup { get; set; }
        /// <summary>
        /// Gets or sets the left lookup file.
        /// </summary>
        /// <value>The left lookup file.</value>
        public string LeftLookupFile { get; set; }

        /// <summary>
        /// Gets or sets the right side.
        /// </summary>
        /// <value>The right side.</value>
        public string RightSide { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether [use right lookup].
        /// </summary>
        /// <value><c>true</c> if [use right lookup]; otherwise, <c>false</c>.</value>
        public bool UseRightLookup { get; set; }
        /// <summary>
        /// Gets or sets the right lookup file.
        /// </summary>
        /// <value>The right lookup file.</value>
        public string RightLookupFile { get; set; }
        /// <summary>
        /// Gets or sets the type of the compare.
        /// </summary>
        /// <value>The type of the compare.</value>
        public ComparisonTypeEnum CompareType { get; set; }
        /// <summary>
        /// Gets or sets the ignore chars.
        /// </summary>
        /// <value>The ignore chars.</value>
        public string IgnoreChars { get; set; }

        /// <summary>
        /// Gets the errors.
        /// </summary>
        /// <value>The errors.</value>
        public string Errors { get; set; }
       
        /// <summary>
        /// Gets or sets the status.
        /// </summary>
        /// <value>The status.</value>
        public string Status { get; set; }

        /// <summary>
        /// Gets or sets the operator.
        /// </summary>
        /// <value>The operator.</value>
        public OperatorEnums Operator { get; set; }
        /// <summary>
        /// Gets or sets the selection.
        /// </summary>
        /// <value>The selection.</value>
        public string Selection { get; set; }
        /// <summary>
        /// Gets or sets the ordinal.
        /// </summary>
        /// <value>The ordinal.</value>
        public decimal Ordinal { get; set; }
        /// <summary>
        /// Gets or sets the start time.
        /// </summary>
        /// <value>The start time.</value>
        public DateTime? StartTime { get; set; }
        /// <summary>
        /// Gets or sets the compare results.
        /// </summary>
        /// <value>The compare results.</value>
        public List<CompareResultModel> CompareResults { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether this instance is calculated.
        /// </summary>
        /// <value><c>true</c> if this instance is calculated; otherwise, <c>false</c>.</value>
        public bool IsCalculated { get; set; }
        /// <summary>
        /// Gets or sets the left query.
        /// </summary>
        /// <value>The left query.</value>
        public string LeftQuery { get; set; }
        /// <summary>
        /// Gets or sets the right query.
        /// </summary>
        /// <value>The right query.</value>
        public string RightQuery { get;set; }

        /// <summary>
        /// Gets or sets the name of the column from the table. Requires a mapping after query exection
        /// </summary>
        /// <value>The name of the column.</value>
        public string ColumnName { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [time zone offset].
        /// </summary>
        /// <value><c>true</c> if [time zone offset]; otherwise, <c>false</c>.</value>
        public int LeftTimeZoneOffset { get; set; }
        public int RightTimeZoneOffset { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="CompareMappingModel"/> class.
        /// </summary>
        public CompareMappingModel()
        {
            CompareResults = new List<CompareResultModel>();
            Selected = true;
            CompareType = ComparisonTypeEnum.String;
            Operator = OperatorEnums.Equals;
            
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="CompareMappingModel"/> class.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        public CompareMappingModel(string left, string right)
            : this()
        {
            LeftSide = left;
            RightSide = right;
        }

        /// <summary>
        /// Gets the left alias.
        /// </summary>
        /// <value>The left alias.</value>
        public string LeftAlias { get { return "left_" + LeftSide; } }
        /// <summary>
        /// Gets the right alias.
        /// </summary>
        /// <value>The right alias.</value>
        public string RightAlias { get { return "right_" + RightSide; } }

        /// <summary>
        /// Gets the left SQL.
        /// </summary>
        /// <value>The left SQL.</value>
        public string LeftSql
        {
            get
            {
                if (string.IsNullOrEmpty(LeftSide)) return string.Empty;
                return string.Format("[{0}] as [{1}]", LeftSide, LeftAlias);
            }
        }
        /// <summary>
        /// Gets the right SQL.
        /// </summary>
        /// <value>The right SQL.</value>
        public string RightSql
        {
            get
            {
                if (string.IsNullOrEmpty(RightSide)) return string.Empty;
                return string.Format("[{0}] as [{1}]", RightSide, RightAlias);
            }
        }
                

        //public string LeftSql()
        //{
        //    List<string> fields = new List<string>();
        //    fields.Add(LeftSql);
        //    fields.Add(RightSql);
        //    return string.Join(",", fields.Where(w => string.IsNullOrEmpty(w) == false));
        //}
        //public string PairRightSql()
        //{
        //    List<string> fields = new List<string>();
        //    fields.Add(LeftSql);
        //    fields.Add(RightSql);
        //    return string.Join(",", fields.Where(w => string.IsNullOrEmpty(w) == false));
        //}


        /// <summary>
        /// Determines whether this instance is pair.
        /// </summary>
        /// <returns><c>true</c> if this instance is pair; otherwise, <c>false</c>.</returns>
        public bool IsPair()
        {
            return (!string.IsNullOrEmpty(LeftSide) && !string.IsNullOrEmpty(RightSide));
        }

        /// <summary>
        /// To the dictionary.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <returns>Dictionary&lt;System.String, System.String&gt;.</returns>
        public Dictionary<string, string> ToDictionary(string file)
        {
            if (string.IsNullOrEmpty(file)) return null;
            if (File.Exists(file) == false) return null;
            StreamReader reader = new StreamReader(file);

            Dictionary<string, string> items = new Dictionary<string, string>();

            while (reader.Peek() >= 0)
            {
                string line = reader.ReadLine();
                try
                {
                    items.Add(line.Split('\t')[0], line.Split('\t')[1]);
                }
                catch(Exception)
                {
                    continue;
                }
            }
            return items;
        }

    }
}
