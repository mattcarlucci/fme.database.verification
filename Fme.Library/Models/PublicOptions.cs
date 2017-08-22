// ***********************************************************************
// Assembly         : Fme.Library
// Author           : mcarlucci
// Created          : 08-17-2017
//
// Last Modified By : mcarlucci
// Last Modified On : 08-22-2017
// ***********************************************************************
// <copyright file="CompareModel.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;

namespace Fme.Library.Models
{
    /// <summary>
    /// Class PublicOptions.
    /// </summary>
    [Serializable]
    public class PublicOptions
    {
        /// <summary>
        /// Gets or sets a value indicating whether [hide empty columns].
        /// </summary>
        /// <value><c>true</c> if [hide empty columns]; otherwise, <c>false</c>.</value>
        public bool HideEmptyColumns { get; set; }
        /// <summary>
        /// Gets or sets the exclude mapping fields.
        /// </summary>
        /// <value>The exclude mapping fields.</value>
        public string ExcludeMappingFields { get; set; }
       
        
        /// <summary>
        /// Initializes a new instance of the <see cref="PublicOptions"/> class.
        /// </summary>
        public PublicOptions()
        {
            HideEmptyColumns = false;
          

        }
        /// <summary>
        /// Loads this instance.
        /// </summary>
        /// <returns>PublicOptions.</returns>
        public static PublicOptions Load()
        {
            return Serializer.DeSerialize <PublicOptions>(@".\options.xml");
        }
        /// <summary>
        /// Saves this instance.
        /// </summary>
        public void Save()
        {
            Serializer.Serialize(".\\options.xml", this);
        }
    }
}
