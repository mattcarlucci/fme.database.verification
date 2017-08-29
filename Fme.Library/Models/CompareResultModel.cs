// ***********************************************************************
// Assembly         : Fme.Library
// Author           : mcarlucci
// Created          : 08-17-2017
//
// Last Modified By : mcarlucci
// Last Modified On : 08-19-2017
// ***********************************************************************
// <copyright file="CompareResultModel.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
#define NO_PARA
using System;

namespace Fme.Library.Models
{
    /// <summary>
    /// Class CompareResultModel.
    /// </summary>
    [Serializable]
    public class CompareResultModel 
    {
        /// <summary>
        /// Gets or sets the primary key.
        /// </summary>
        /// <value>The primary key.</value>
        public string PrimaryKey { get; set; }

        /// <summary>
        /// Gets or sets the left key.
        /// </summary>
        /// <value>The left key.</value>
        public string LeftKey { get; set; }
        /// <summary>
        /// Gets or sets the left side.
        /// </summary>
        /// <value>The left side.</value>
        public string LeftSide { get; set; }

        /// <summary>
        /// Gets or sets the right key.
        /// </summary>
        /// <value>The right key.</value>
        public string RightKey { get; set; }
        /// <summary>
        /// Gets or sets the right side.
        /// </summary>
        /// <value>The right side.</value>
        public string RightSide { get; set; }
        /// <summary>
        /// Gets or sets the left value.
        /// </summary>
        /// <value>The left value.</value>
        public string LeftValue { get; set; }
        /// <summary>
        /// Gets or sets the right value.
        /// </summary>
        /// <value>The right value.</value>
        public string RightValue { get; set; }
       
       

        /// <summary>
        /// Gets or sets the row.
        /// </summary>
        /// <value>The row.</value>
        public int Row { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="CompareResultModel"/> class.
        /// </summary>
        /// <param name="primaryKey">The primary key.</param>
        /// <param name="leftSide">The left side.</param>
        /// <param name="rightSide">The right side.</param>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <param name="row">The row.</param>
        public CompareResultModel(string primaryKey, string leftSide, string rightSide, string left, string right, int row)
        {
            PrimaryKey = primaryKey;             
            LeftSide = leftSide;
            RightSide = rightSide;
            LeftValue = left;
            RightValue = right;
            Row = row;
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="CompareResultModel"/> class.
        /// </summary>
        /// <param name="primaryKey">The primary key.</param>
        /// <param name="leftSide">The left side.</param>
        /// <param name="rightSide">The right side.</param>
        /// <param name="leftKey">The left key.</param>
        /// <param name="rightKey">The right key.</param>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <param name="row">The row.</param>
        public CompareResultModel(string primaryKey, string leftSide, string rightSide, string leftKey, string rightKey, string left, string right, int row)
        {
            PrimaryKey = primaryKey;
            LeftSide = leftSide;
            RightSide = rightSide;
            LeftValue = left;
            RightValue = right;
            LeftKey = leftKey;
            RightKey = rightKey;
            Row = row;
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="CompareResultModel"/> class.
        /// </summary>
        /// <param name="primaryKey">The primary key.</param>
        /// <param name="model">The model.</param>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <param name="row">The row.</param>
        public CompareResultModel(string primaryKey, CompareMappingModel model, string left, string right, int row) :
            this(primaryKey,model.LeftSide, model.RightSide, model.LeftKey, model.RightKey, left, right, row)
        {

        }
        /// <summary>
        /// Initializes a new instance of the <see cref="CompareResultModel"/> class.
        /// </summary>
        public CompareResultModel()
        {

        }

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table.</returns>
        public override int GetHashCode()
        {
            return (LeftValue + RightValue).GetHashCode();
        }
    }

}
