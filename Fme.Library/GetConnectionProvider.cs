using Fme.DqlProvider;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fme.Library
{
    public class GetConnectionProvider
    {
        /// <summary>
        /// Gets the provider.
        /// </summary>
        /// <param name="provider">The provider.</param>
        /// <returns>DbConnectionStringBuilder.</returns>
        public static DbConnectionStringBuilder GetProvider(string provider)
        {
            if (provider == "Documentum")
                return new DqlConnectionStringBuilder();
            else if (provider == "Microsoft Excel")
                return new ExcelDbConnectionStringBuilder(string.Empty);
            else if (provider == "Microsoft Access")
                return new AccessDbConnectionStringBuilder(string.Empty);
            else if (provider == "Microsoft Sql Server")
                return new SqlConnectionStringBuilder();
            else
                return new DbConnectionStringBuilder();             
        }

        /// <summary>
        /// Gets the provider.
        /// </summary>
        /// <param name="provider">The provider.</param>
        /// <returns>DbConnectionStringBuilder.</returns>
        public static DbConnectionStringBuilder GetProvider(Type provider)
        {
            return (DbConnectionStringBuilder)Activator.CreateInstance(provider);            
        }

       
    }
}
