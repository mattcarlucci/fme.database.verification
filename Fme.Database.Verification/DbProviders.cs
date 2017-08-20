using System;
using System.Collections.Generic;
using Fme.DqlProvider;
using Fme.Library;
using System.Data.SqlClient;
using System.Data.Common;

namespace Fme.Database.Verification
{
    public class DbConnectionStringFactory : Dictionary<string, Func<string, DbConnectionStringBuilder>>
    {
        public DbConnectionStringFactory()
        {
            Add("OpenText Documentum", (cn) => new DqlConnectionStringBuilder(cn));
            Add("Microsoft Excel", (cn) => new ExcelDbConnectionStringBuilder(cn));
            Add("Microsoft Access", (cn) => new AccessDbConnectionStringBuilder(cn));
            Add("Microsoft Sql Server", (cn) => new SqlConnectionStringBuilder(cn));
        }

       
    }

    public class DbDataSourceFactory : Dictionary<string, Func<string, DataSourceBase>>
    {
        public DbDataSourceFactory()
        {
            Add("OpenText Documentum", (cn) => new DqlDataSource(cn));
            Add("Microsoft Excel", (cn) =>  new ExcelDataSource(cn));
            Add("Microsoft Access", (cn) => new AccessDataSource(cn));
            Add("Microsoft Sql Server", (cn) => new SqlDataSource(cn));        }
    }
}