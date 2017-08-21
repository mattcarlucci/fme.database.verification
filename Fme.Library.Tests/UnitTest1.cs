// ***********************************************************************
// Assembly         : Fme.Library.Tests
// Author           : mcarlucci
// Created          : 08-15-2017
//
// Last Modified By : mcarlucci
// Last Modified On : 08-18-2017
// ***********************************************************************
// <copyright file="UnitTest1.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using System.Data.OleDb;
using System.Threading;

using Fme.Library;
using Fme.Library.Models;
using Fme.Library.Extensions;
using Fme.DqlProvider;

namespace Fme.Library.Tests
{
    /// <summary>
    /// Class UnitTest1.
    /// </summary>
    [TestClass]
    public class UnitTest1
    {
        /// <summary>
        /// Tests the DQL query builder with cancel token.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(System.OperationCanceledException))]
        public void Test_DqlQueryBuilder_WithCancelToken()
        {
            CancellationTokenSource sourceToken = new CancellationTokenSource();            
            CancellationToken token = sourceToken.Token;
            
            sourceToken.Cancel();
            
            DqlConnectionStringBuilder builder = new DqlConnectionStringBuilder("dmadmin", "@vmware99", "ls_repos");
            DqlDataSource dql = new DqlDataSource(builder.ConnectionString);

            QueryBuilder query = dql.GetQueryBuilder();
            var select = query.BuildSql("r_object_id", new string[] { "keywords", "authors" }, "dm_cabinet", "left", "0", "r_object_id", new string[] { "0c00301880000104" } );
            var table = dql.ExecuteQuery(select, token);
            

            Assert.AreEqual(table.Tables.Count, 1);
            Assert.AreEqual(table.Tables[0].Rows.Count, 1);
        }
        /// <summary>
        /// Tests the DQL connection builder keys.
        /// </summary>
        [TestMethod]
        public void Test_DqlConnectionBuilderKeys()
        {
            DqlConnectionStringBuilder builder = new DqlConnectionStringBuilder("dmadmin", "@vmware99", "ls_repos");
            DqlDataSource dql = new DqlDataSource(builder.ConnectionString);
            var Keys = dql.GetConnectionStringBuilder();

            //TODO need to repalce repositry with data source
            Assert.AreEqual((string)Keys["Data Source"], "ls_repos");
            Assert.AreEqual((string)Keys["User ID"], "dmadmin");
            Assert.AreEqual((string)Keys["Password"], "@vmware99");
        }

        /// <summary>
        /// Tests the DQL query builder.
        /// </summary>
        [TestMethod]
        public void Test_DqlQueryBuilder()
        {
            DqlConnectionStringBuilder builder = new DqlConnectionStringBuilder("dmadmin", "@vmware99", "ls_repos");
            DqlDataSource dql = new DqlDataSource(builder.ConnectionString);
            var x = dql.GetConnectionStringBuilder();

            //TODO need to repalce repositry with data source
            var y = x["Data Source"];


            var query = dql.GetQueryBuilder();
            var select = query.BuildSql("r_object_id", new string[] { "keywords", "authors" }, "dm_cabinet", "left", "0", "r_object_id", new string[] { "0c00301880000104" } );
            var table = dql.ExecuteQuery(select);
            Assert.AreEqual(table.Tables.Count, 1);
            Assert.AreEqual(table.Tables[0].Rows.Count, 1);
        }

        [TestMethod]
        public void Test_DqlQuery_AltKeys()
        {
            DqlConnectionStringBuilder builder = new DqlConnectionStringBuilder("dmadmin", "@vmware99", "ls_repos");
            DqlDataSource dql = new DqlDataSource(builder.ConnectionString);
            var x = dql.GetConnectionStringBuilder();

            //TODO need to repalce repositry with data source
            var y = x["Data Source"];


            var query = dql.GetQueryBuilder();
            var select = query.BuildSql("keywords", new string[] {"r_object_id", "keywords", "authors" }, "dm_cabinet", "left", "0", "r_object_id", new string[] { "0c00301880000104" });
            var table = dql.ExecuteQuery(select);
            Assert.AreEqual(table.Tables.Count, 1);
            Assert.AreEqual(table.Tables[0].Rows.Count, 1);
        }
        [TestMethod]
        public void Test_DqlQuery_EmtpyRecordset()
        {
            DqlConnectionStringBuilder builder = new DqlConnectionStringBuilder("dmadmin", "@vmware99", "ls_repos");
            DqlDataSource dql = new DqlDataSource(builder.ConnectionString);
            var x = dql.GetConnectionStringBuilder();       

            var query = dql.GetQueryBuilder();
            var select = query.BuildSql("keywords", new string[] { "r_object_id", "keywords", "authors" }, "dm_cabinet", "left", "0", "r_object_id", new string[] { "0c0030188000010X" });
            var table = dql.ExecuteQuery(select);
            Assert.AreEqual(table.Tables.Count,0);
          //  Assert.AreEqual(table.Tables[0].Rows.Count, 1);
        }
        /// <summary>
        /// Tests the excel connection.
        /// </summary>
        [TestMethod]
        public void Test_ExcelConnection()
        {
            string file = @"C:\Users\mcarlucci\Documents\Visual Studio 2017\Projects\Fme.Database.Verification\Fme.Library.Tests\bin\Debug\dm_document1.xlsx";
            ExcelDbConnectionStringBuilder builder = new ExcelDbConnectionStringBuilder(file);
                     
            OleDbDataSource dql = new OleDbDataSource(builder.ConnectionString);
            var x = dql.GetConnectionStringBuilder();
            var y = x["Data Source"];
            OleDbConnection cn = new OleDbConnection(x.ConnectionString);
            cn.Open();

        }
        /// <summary>
        /// Tests the excel query builder.
        /// </summary>
        [TestMethod]
        public void Test_ExcelQueryBuilder()
        {
            ExcelDbConnectionStringBuilder builder = new ExcelDbConnectionStringBuilder(@".\cd_clinical.xlsx");
            OleDbDataSource dql = new OleDbDataSource(builder.ConnectionString);

            QueryBuilder query = dql.GetQueryBuilder();
            var select = query.BuildSql("r_object_id", new string[] { "object_name", "r_object_type" }, "Sheet1$", "left", "0", "r_object_id", new string[] { "090200f1800de14c" } );
            var table = dql.ExecuteQuery(select);
            Assert.AreEqual(table.Tables.Count, 1);
            Assert.AreEqual(table.Tables[0].Rows.Count, 1);
        }

        /// <summary>
        /// Tests the access query builder.
        /// </summary>
        [TestMethod]
        public void Test_AccessQueryBuilder()
        {
            AccessDbConnectionStringBuilder builder = new AccessDbConnectionStringBuilder(@".\Northwinds.accdb");
            OleDbDataSource dql = new OleDbDataSource(builder.ConnectionString);

            QueryBuilder query = dql.GetQueryBuilder();
            var select = query.BuildSql("ID", new string[] { "City", "Company" }, "Customers", "left", "0", "ID", new int[] { 1,2 });
            var table = dql.ExecuteQuery(select);
            Assert.AreEqual(table.Tables.Count, 1);
            Assert.AreEqual(table.Tables[0].Rows.Count, 2);
        }

        /// <summary>
        /// Tests the excel schemas.
        /// </summary>
        [TestMethod]
        public void Test_ExcelSchemas()
        {
            ExcelDbConnectionStringBuilder builder = new ExcelDbConnectionStringBuilder(@".\cd_clinical.xlsx");
            OleDbDataSource dql = new OleDbDataSource(builder.ConnectionString);
            var model = dql.GetSchemaModel();
            Assert.AreEqual(1, model.Where(w => w.TableName == "Sheet1$").Count());
        }

        /// <summary>
        /// Tests the excel schema.
        /// </summary>
        [TestMethod]
        public void Test_ExcelSchema()
        {
            
            ExcelDbConnectionStringBuilder builder = new ExcelDbConnectionStringBuilder(@".\cd_clinical.xlsx");
            OleDbDataSource dql = new OleDbDataSource(builder.ConnectionString);

            var model = dql.GetSchemaModel("Sheet1$");
            Assert.AreEqual(model.TableName, "Sheet1$");
        }

        /// <summary>
        /// Tests the accesschemas.
        /// </summary>
        [TestMethod]
        public void Test_Accesschemas()
        {
            AccessDbConnectionStringBuilder builder = new AccessDbConnectionStringBuilder(@".\Northwinds.accdb");
            OleDbDataSource dql = new OleDbDataSource(builder.ConnectionString);

            var model = dql.GetSchemaModel();
            Assert.AreEqual(1, model.Where(w => w.TableName == "Customers").Count());

        }

        /// <summary>
        /// Tests the access schema.
        /// </summary>
        [TestMethod]
        public void Test_AccessSchema()
        {

            AccessDbConnectionStringBuilder builder = new AccessDbConnectionStringBuilder(@".\Northwinds.accdb");
            OleDbDataSource dql = new OleDbDataSource(builder.ConnectionString);

            var model = dql.GetSchemaModel("Customers");
            Assert.AreEqual(model.TableName, "Customers");
        }

        /// <summary>
        /// Tests the DQL schemas.
        /// </summary>
        [TestMethod]
        public void Test_DqlSchemas()
        {
            DqlConnectionStringBuilder builder = new DqlConnectionStringBuilder("dmadmin", "@vmware99", "ls_repos");
            DqlDataSource dql = new DqlDataSource(builder.ConnectionString);
          
            var model = dql.GetSchemaModel();
            var fields = model.Where(w=> w.TableName == "dm_cabinet").
                FirstOrDefault().Fields.Select(s => s.Name);

        }

        /// <summary>
        /// Tests the DQL schema.
        /// </summary>
        [TestMethod]
        public void Test_DqlSchema()
        {
            DqlConnectionStringBuilder builder = new DqlConnectionStringBuilder("dmadmin", "@vmware99", "ls_repos");
            DqlDataSource dql = new DqlDataSource(builder.ConnectionString);

            var model = dql.GetSchemaModel("dm_cabinet");
            var fields = model.Fields.Select(s => s.Name);
        }

        //[TestMethod]
        /// <summary>
        /// Tests the SQL schema.
        /// </summary>
        public void Test_SqlSchema()
        {
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
            SqlDataSource dql = new SqlDataSource(builder.ConnectionString);

           
            var model = dql.GetSchemaModel("dm_cabinet");
            var fields = model.Fields.Select(s => s.Name);
        }

        /// <summary>
        /// Tests the setup comparison.
        /// </summary>
        [TestMethod]
        public void Test_SetupComparison()
        {
            ExcelDbConnectionStringBuilder builder1 = new ExcelDbConnectionStringBuilder(@".\dm_document1.xlsx");
            ExcelDataSource dql1 = new ExcelDataSource(builder1.ConnectionString);
            var source = dql1.GetSchemaModel("Sheet1$");

            ExcelDbConnectionStringBuilder builder2 = new ExcelDbConnectionStringBuilder(@".\dm_document3.xlsx");
            ExcelDataSource dql2 = new ExcelDataSource(builder2.ConnectionString);
            var target = dql2.GetSchemaModel("Sheet1$");

            var model = new CompareModel();
            //model.Source.DataSource = dql1;
            //model.Source.TableSchemas = source;
            //model.Source.SelectedTable = "Sheet1$";
            //model.Target = dsl2;
            //model.Target.TableSchemas = target;
            //model.Target.SelectedTable = "Sheet1$";
                      
                        
            var pairs = source.Fields.
                Join(target.Fields,
                    s => new { s.Name },
                    t => new { t.Name },
                    (s, t) => new CompareMappingModel(s.Name, t.Name)
                ).ToList();

          //  model.ColumnCompare = pairs;

            
            //QueryBuilder query = new QueryBuilder();
            //var select1 = query.BuildSql("r_object_id", pairs.Select(s=> s.LeftSide.Field).ToArray(), "Sheet1$", "left");
            //var select2 = query.BuildSql("r_object_id", pairs.Select(s => s.RightSide.Field).ToArray(), "Sheet1$", "right");

            //var t1 = dql1.ExecuteQuery(select1).Tables[0];
            //var t2 = dql2.ExecuteQuery(select2).Tables[0];
            //t1.SetPrimaryKey("primary_key", t2);
            //t1.Merge(t2);
            //TODO: order columns
            //TODO Now we should do the compare. 
            //Next Dump to Excel? or do we do the compare within excel


        }
        /// <summary>
        /// Tests the merge.
        /// </summary>
        [TestMethod]
        public void Test_Merge()
        {
            //var b = new ExcelDbConnectionStringBuilder(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=.\dm_document1.xlsx;Extended Properties='Excel 12.0;IMEX=1;ImportMixedTypes=Text;READONLY=TRUE");

            ExcelDbConnectionStringBuilder builder1 = new ExcelDbConnectionStringBuilder(@".\dm_document1.xlsx");
            ExcelDataSource dql1 = new ExcelDataSource(builder1.ConnectionString);

            Serializer.Serialize(@".\dql.xml", dql1);

            var qb1 = dql1.GetQueryBuilder();
            var cb2 = dql1.GetConnectionStringBuilder();

            qb1.BuildSql("r_object_id2", new string[] { "r_object_id2", "r_object_type" }, "Sheet1$", "right", "0");


            QueryBuilder query = new QueryBuilder();
            var select1 = query.BuildSql("r_object_id", new string[] { "object_name", "r_object_type" }, "Sheet1$", "left", "0");
            var table1 = dql1.ExecuteQuery(select1);
            var schema1 = dql1.GetSchemaModel("Sheet1$");

            ExcelDbConnectionStringBuilder builder2 = new ExcelDbConnectionStringBuilder(@".\dm_document3.xlsx");
            OleDbDataSource dql2 = new OleDbDataSource(builder2.ConnectionString);
            var schema2 = dql2.GetSchemaModel("Sheet1$");

            
            var select2 = query.BuildSql("r_object_id", new string[] { "r_object_id2", "r_object_type" }, "Sheet1$", "right", "0");
            var table2 = dql2.ExecuteQuery(select2);
            
            var source = table1.Tables[0];
            var target = table2.Tables[0];

         
            source.SetPrimaryKey("primary_key", target);           
            source.Merge(target);
            
            
        }
        /// <summary>
        /// Populates the model.
        /// </summary>
        public void PopulateModel()
        {

            //var model = new CompModel();
            //ExcelDbConnectionStringBuilder xlsBuilder = new ExcelDbConnectionStringBuilder(@".\dm_document1.xlsx");
            //model.Left.DataSource = new ExcelDataSource(xlsBuilder);

            //DqlConnectionStringBuilder builder = new DqlConnectionStringBuilder("dmadmin", "@vmware99", "ls_repos");
            //DqlDataSource dql = new DqlDataSource(builder.ConnectionString);
            //model.Right.DataSource = dql;
            //model.CompareC
        }


    }
}
