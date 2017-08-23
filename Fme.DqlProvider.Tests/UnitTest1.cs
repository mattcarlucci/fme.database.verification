// ***********************************************************************
// Assembly         : DqlProvider.Tests
// Author           : Matt.Carlucci
// Created          : 08-12-2017
//
// Last Modified By : Matt.Carlucci
// Last Modified On : 08-12-2017
// ***********************************************************************
// <copyright file="UnitTest1.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Data;
using System.Data.Common;
using System.Data.OleDb;
using System.Data.SqlClient;

namespace Fme.DqlProvider.Tests
{
    /// <summary>
    /// Class UnitTest1.
    /// </summary>
    [TestClass]
    public class UnitTest1
    {

        /// <summary>
        /// Tests the DQL fill schema.
        /// </summary>
        [TestMethod]
        public void Test_DqlFill_Schema()
        {
            string select = "Select * from dm_cabinet enable(return_top 1)";
            string cnstr = "User=dmadmin;Password=@vmware99;Repository=ls_repos";
            DataSet dataset = new DataSet();
                      
            using (var cn = new DqlConnection(cnstr))
            {
                cn.Open();
                using (var cmd = new DqlCommand(select, cn))
                {
                    using(var adapter = new DqlDataAdapter(cmd))
                    {
                        adapter.FillSchema(dataset);
                    }
                }
            }
        }


        /// <summary>
        /// Tests the DQL fill repeating values.
        /// </summary>
        [TestMethod]
        public void Test_DqlFill_RepeatingValues()
        {
            string select = "select r_object_id, object_name, keywords, authors from dm_cabinet where not object_name like 'dm%' and object_name = 'ls_repos'";
            DataSet dataset = new DataSet();
            string cnstr = "User=dmadmin;Password=@vmware99;Repository=ls_repos";
            using (DqlConnection cn = new DqlConnection(cnstr))
            {
                cn.Open();
                using (DqlCommand cmd = new DqlCommand(select, cn))
                {
                    using (DqlDataAdapter adapter = new DqlDataAdapter(cmd))
                    {
                        var schemas = adapter.FillSchema(dataset, SchemaType.Source);
                        Assert.AreEqual(dataset.Tables[0].Rows[0]["keywords"].ToString(), "test2|test3|test1");
                        Assert.AreEqual(dataset.Tables[0].Rows[0]["authors"].ToString(), "author2|author3|author1");
                    }
                }
            }
        }

        /// <summary>
        /// Tests the DQL fill.
        /// </summary>
        [TestMethod]
        public void Test_DqlFill()
        {
            string select = "Select r_object_id, keywords from dm_cabinet enable(return_top 1); select * from dm_document enable(return_top 1)";
            DataSet dataset = new DataSet();
            string cnstr = "User=dmadmin;Password=@vmware99;Repository=ls_repos";
            using (DqlConnection cn = new DqlConnection(cnstr))
            {
                cn.Open();
                using (DqlCommand cmd = new DqlCommand(select, cn))
                {
                    using (DqlDataAdapter adapter = new DqlDataAdapter(cmd))
                    {                        
                        var schemas =  adapter.Fill(dataset);
                        Assert.AreEqual(dataset.Tables.Count, 2);
                        Assert.AreEqual(dataset.Tables[0].Rows.Count, 1);
                        Assert.AreEqual(dataset.Tables[1].Rows.Count, 1);
                    }
                }
            }

        }

        /// <summary>
        /// Tests the DQL connection states.
        /// </summary>
        [TestMethod]
        public void Test_DqlConnection_States()
        {
            string cnstr = "User=dmadmin;Password=@vmware99;Repository=ls_repos";
            using (DqlConnection cn = new DqlConnection(cnstr))
            {
                Assert.IsTrue(cn.State == ConnectionState.Closed, "State is Closed");
                cn.Open();
                Assert.IsTrue(cn.State == ConnectionState.Open, "State is Open");
            }
        }
        /// <summary>
        /// Fills the access.
        /// </summary>
        [TestMethod]
        public void FillAccess()
        {
            string select = "Select * from Customers";
            string cnstr = string.Format("Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};User Id=Admin;Password=", ".\\Northwinds.accdb");
            DataSet dataset = new DataSet();

            using (var cn = new OleDbConnection(cnstr))
            {
                cn.Open();
                using (var cmd = new OleDbCommand(select, cn))
                {
                    using (var adapter = new OleDbDataAdapter(cmd))
                    {
                        var schemas = adapter.Fill(dataset);
                    }
                }
            }
        }

        /// <summary>
        /// Fills the excel.
        /// </summary>
        [TestMethod]
        public void FillExcel()
        {
            //doesn't support multiple requests in a single call
            string select = "Select * from [Customers$]";
            string cnstr = string.Format("Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};", ".\\Northwinds.xlsx");
            cnstr += "Extended Properties=\"Excel 8.0;HDR=YES;IMEX=1;MAXSCANROWS=15;READONLY=FALSE\"";
            DataSet dataset = new DataSet();

            using (var cn = new OleDbConnection(cnstr))
            {
                cn.Open();
                using (var cmd = new OleDbCommand(select, cn))
                {
                    using (var adapter = new OleDbDataAdapter(cmd))
                    {
                        var schemas = adapter.Fill(dataset);
                    }
                }
            }
        }

        /// <summary>
        /// Fills the SQL.
        /// </summary>
        //[TestMethod]
        public void FillSql()
        {
            string select = "Select * from Customers;Select * from Employees";
            string cnstr = string.Format("Data Source=(local);Initial Catalog=Northwind;Integrated Security=True");
            DataSet dataset = new DataSet();

            using (var cn = new SqlConnection(cnstr))
            {
                cn.Open();
                using (var cmd = new SqlCommand(select, cn))
                {
                    using (var adapter = new SqlDataAdapter((SqlCommand)cmd))
                    {
                       
                        adapter.Fill(dataset);
                    }
                }
            }
        }
       
      
    }
}
