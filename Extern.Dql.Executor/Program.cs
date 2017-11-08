// ***********************************************************************
// Assembly         : Extern.Dql.Executor
// Author           : mcarlucci
// Created          : 11-03-2017
//
// Last Modified By : mcarlucci
// Last Modified On : 11-03-2017
// ***********************************************************************
// <copyright file="Program.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
using Fme.DqlProvider;
using Fme.Library.Extensions;
using Fme.Library.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Extern.Dql.Executor
{
    /// <summary>
    /// Class Program.
    /// </summary>
    class Program
    {
        /// <summary>
        /// Defines the entry point of the application.
        /// </summary>
        /// <param name="args">The arguments.</param>
        static void Main(string[] args)
        {
            ExternalQueryModel model = new ExternalQueryModel(args[0]);
            try
            { 
                Console.Write("Executing External Query...\r\n");
            
                DqlConnectionStringBuilder builder = new DqlConnectionStringBuilder(model.GetConnectionString());
                builder.Password = new string('*', builder.Password.Length);

                Console.Title = builder.ConnectionString;
                Console.WriteLine(builder.ConnectionString + Environment.NewLine);            
                Console.WriteLine(model.GetQueryString());

                model.WriteQuery();
            }
            catch(Exception ex)
            {
                model.WriteError(ex);
            }

        }

    }
}
