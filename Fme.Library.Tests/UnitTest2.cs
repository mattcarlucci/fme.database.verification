// ***********************************************************************
// Assembly         : Fme.Library.Tests
// Author           : mcarlucci
// Created          : 11-15-2017
//
// Last Modified By : mcarlucci
// Last Modified On : 11-16-2017
// ***********************************************************************
// <copyright file="UnitTest2.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Fme.Library.Extensions;
using System.Linq;
using System.Reflection;
using System.Collections.Generic;
using System.Threading.Tasks;
using Fme.Library.Comparison;
using System.Globalization;
using System.IO;
using Fme.Library.Repositories;
using Fme.Library.Models;
using System.Data;

namespace Fme.Library.Tests
{

    /// <summary>
    /// Class ValidationTests.
    /// </summary>
    [TestClass]
    public class ValidationTests
    {
        /// <summary>
        /// Validations the test string is numeric.
        /// </summary>
        [TestMethod]
        public void ValidationTest_StringIsNumeric()
        {
            var string1 = "123";
            Assert.AreEqual(true, string1.IsNumeric());
        }

        /// <summary>
        /// Validations the test string is not numeric.
        /// </summary>
        [TestMethod]
        public void ValidationTest_StringIsNotNumeric()
        {
            var string1 = "12b3";
            Assert.AreEqual(false, string1.IsNumeric());
        }
        /// <summary>
        /// Validations the test string is alpha.
        /// </summary>
        [TestMethod]
        public void ValidationTest_StringIsAlpha()
        {
            var string1 = "aE@";
            Assert.AreEqual(false, string1.IsAlpha());
        }
        /// <summary>
        /// Validations the test string is not alpha.
        /// </summary>
        [TestMethod]
        public void ValidationTest_StringIsNotAlpha()
        {
            var string1 = "1aE@";
            Assert.AreEqual(false, string1.IsAlpha());
        }

        /// <summary>
        /// Validations the test string is alpha numeric.
        /// </summary>
        [TestMethod]
        public void ValidationTest_StringIsAlphaNumeric()
        {
            var string1 = "aE1";
            Assert.AreEqual(true, string1.IsAlphaNumeric());
        }

        /// <summary>
        /// Validations the test string is printable.
        /// </summary>
        [TestMethod]
        public void ValidationTest_StringIsPrintable()
        {
            //https://stackoverflow.com/questions/3253247/how-do-i-detect-non-printable-characters-in-net

            var string1 = string.Format("aE1{0} ", (char)127);
            Assert.AreEqual(false, string1.IsPrintable());
        }

        /// <summary>
        /// Validations the test split sub string test.
        /// </summary>
        [TestMethod]
        public void ValidationTest_SplitSubStringTest()
        {
            string testValue = "abc-1234";
            var par1 = testValue.Substring(0, 3).IsAlpha();
            var par2 = testValue.Substring(3, 1).IsChar("-");
            var par3 = testValue.Substring(4, 3).IsNumeric();

            Assert.IsTrue(par1 && par2 && par3);
        }
        [TestMethod]
        public void ValidationTest_Datatable()
        {
            string path = @"C:\Users\mcarlucci\Desktop\Tim\";
            string modelFile = "Validation Test Model.xml";
            string xlsFile = "SmartSolve Extract.xlsx";
            var temp = Serializer.DeSerialize<CompareModel>(path + modelFile);

            ExcelDbConnectionStringBuilder builder = new ExcelDbConnectionStringBuilder(path + xlsFile);
            OleDbDataSource dql = new OleDbDataSource(builder.ConnectionString);
            var ds = dql.ExecuteQuery(string.Format("select * from [{0}]", temp.Source.SelectedTable));

            ValidatorRepository repo = new ValidatorRepository();
            repo.Execute(ds.Table(), temp.Source.SelectedSchema().Fields);

        }
        /// <summary>
        /// Validations the test multi function paralell.
        /// </summary>
       public void ValidationTest_MultiFunctionParalell()
        {
            string[] testValues = { "abc-1234 10/16/2017" }; //, "def-4567 10/16/2017" };
            string methods = " !IsAlpha(0,3) ; IsChar(3, 1, -) ; IsNumeric(5,3) ;";
            methods += "IsInteger(5,3) ; IsMultiByteString() ; GreaterThanEqualInteger(5,1,6);";
            methods += "GreaterThanEqualDate(9,10, 11/1/2017) ; VerifyDateFormat(9,10,MM/dd/yyyy);";
            methods += @"LookInList(0,3, .\lookup.csv);IsBlank();";

            ValidatorRepository validator = new ValidatorRepository();

            validator.LoadLists(methods);

         //   Parallel.ForEach(testValues, testvalue =>
        ///    { 
                foreach (var testValue in testValues)
                {
                    List<bool> results = new List<bool>();
                    foreach (var method in ValidatorRepository.ParseFunctions(methods))
                    {
                        var items = ValidatorRepository.ParseParameters(method);
                        results.Add(validator[items.First()](testValue, method));
                    }
                Assert.AreEqual(true, results.Take(4).All(a => a == true));
                Assert.AreEqual(true, results.Skip(4).Take(3).All(a => a == false));
                Assert.AreEqual(true, results.Skip(7).Take(1).All(a => a == true));
                Assert.AreEqual(true, results.Skip(8).Take(1).All(a => a == true));
                Assert.AreEqual(true, results.Skip(9).Take(1).All(a => a == false));
            }
        //    });
            
        }
      
    }
}
