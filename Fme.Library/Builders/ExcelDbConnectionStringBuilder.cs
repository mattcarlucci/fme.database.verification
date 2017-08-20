// ***********************************************************************
// Assembly         : Fme.Library
// Author           : mcarlucci
// Created          : 08-16-2017
//
// Last Modified By : mcarlucci
// Last Modified On : 08-16-2017
// ***********************************************************************
// <copyright file="ExcelDbConnectionStringBuilder.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Data.Common;
using System.Data.OleDb;
using System.IO;

namespace Fme.Library
{
    /// <summary>
    /// Class ExcelDbConnectionStringBuilder.
    /// </summary>
    /// <seealso cref="System.Data.Common.DbConnectionStringBuilder" />
    [Serializable]
    public class ExcelDbConnectionStringBuilder : DbConnectionStringBuilder
    {

        //  string constr = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" +
        //          openFileDialog1.FileName + ";Extended Properties='Excel 12.0;IMEX=1;'";

        //public ExcelDbConnectionStringBuilder(string file)
        //    : base(false)
        //{

        //    //Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties='Excel 8.0;HDR=YES 
        //    //Provider=Microsoft.Jet.OLEDB.4.0;Data Source=c:\\testexcel.xls;Extended Properties=\"Excel 8.0;HDR=YES;IMEX=1;MAXSCANROWS=15;READONLY=FALSE\"" 
        //    FileInfo info = new FileInfo(file);

        public ExcelDbConnectionStringBuilder() : base(false)
        {
            this["Provider"] = "Microsoft.ACE.OLEDB.12.0";
            this["Extended Properties"] = "'Excel 12.0;IMEX=1;ImportMixedTypes=Text;READONLY=TRUE'";           
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ExcelDbConnectionStringBuilder"/> class.
        /// </summary>
        /// <param name="file">The file.</param>
        public ExcelDbConnectionStringBuilder(string file)
            : this()
        {
            //KernelExtensions ke = new KernelExtensions();     
            //if (file.Contains(" 2 "))
            //    this["Data Source"] = ke.GetShortPath(file);
            //else
            if (!string.IsNullOrEmpty(file) && File.Exists(file) == false)
                throw new FileNotFoundException(file);

            this["Data Source"] =  file;


        }

        /// <summary>
        /// Gets or sets the connection string associated with the <see cref="T:System.Data.Common.DbConnectionStringBuilder" />.
        /// </summary>
        /// <value>The connection string.</value>
        public new string ConnectionString
        {
            get { return base.ConnectionString.Replace("\"", ""); }
        }
    }
}
