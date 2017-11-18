using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Globalization;
using System.Threading.Tasks;
using System.Collections.Generic;
using Fme.Library.Comparison;
using Fme.Library.Extensions;
using Fme.Library.Models;
using System.Data;
using System.Diagnostics;

namespace Fme.Library.Repositories
{
    #region Depricated
    public class DataTypeConverter : Dictionary<string, Func<string, IGenericConverter>>
    {
        public DataTypeConverter()
        {
            Add("Date", (converter) => new GenericConverter<DateTime>());
        }
    }
   
    public class DynamicFunction
    {
        public string Name { get; set; }
        public ReturnTypeEnums ReturnType { get; set; }
        public string[] Parameters { get; set; }
    }
    public class DynamicFunctions : List<DynamicFunction> { }
    #endregion

    /// <summary>
    /// Class ValidatorRepository.
    /// </summary>
    /// <seealso cref="System.Collections.Generic.Dictionary{System.String, System.Func{System.String, System.String, System.Boolean}}" />
    public class ValidatorRepository : Dictionary<string, Func<string, string, bool>>
    {
        public event EventHandler<ValidationEventArgs> Validate;

        protected virtual void OnValidate(object sender, ValidationEventArgs e)
        {
            if (Validate != null)
                Validate(sender, e);
        }
        /// <summary>
        /// The lookup lists
        /// </summary>
        Dictionary<string, string[]> lookupLists = new Dictionary<string, string[]>();

        /// <summary>
        /// Initializes a new instance of the <see cref="ValidatorRepository"/> class.
        /// </summary>
        public ValidatorRepository()
        {         
           // Add("IsNullOrEmpty", (value, parms) => string.IsNullOrEmpty(GetString(value, parms)));
          //  Add("IsNullOrWhiteSpace", (value, parms) => string.IsNullOrWhiteSpace(GetString(value, parms)));
            Add("IsBlank", (value, parms) => GetString(value, parms).IsEmpty());
            Add("IsAlpha", (value, parms) => GetString(value, parms).IsAlpha());
            Add("IsNumeric", (value, parms) => GetString(value, parms).IsNumeric());
            Add("IsAlphaNumeric", (value, parms) => GetString(value, parms).IsAlphaNumeric());
         //   Add("IsPrintable", (value, parms) => GetString(value, parms).IsPrintable());
            Add("NonPrintableChars", (value, parms) => GetString(value, parms).NotPrintable());
            Add("IsMultiByteString", (value, parms) => GetString(value, parms).IsMultiByteString());
            Add("IsInteger", (value, parms) => GetString(value, parms).IsInteger());
            Add("IsDateTime", (value, parms) => GetString(value, parms).IsDateTime());
            Add("IsChar", (value, parms) => GetString(value, parms).IsChar(ParseParameters(parms).Last()));
            Add("GreaterThanEqualInteger", (value, parms) => GreaterThanEqualInteger(value, parms));
            Add("GreaterThanEqualDate", (value, parms) => GreaterThanEqualDate(value, parms));
            Add("VerifyDateFormat", (value, parms) => VerifyDateFormat(value, parms));
            Add("LookInList", (value, parms) => LookInList(value, parms));

            var tmp = new Dictionary<string, Func<string, string, bool>>();           
            foreach (var kvp in this)
            {                
                tmp.Add("+" + kvp.Key, kvp.Value);
                tmp.Add("-" + kvp.Key, kvp.Value);
            }
            this.AddRange(tmp);
        }
        /// <summary>
        /// Gets or sets the <see cref="Dictionary{System.String, System.String[]}"/> with the specified key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns>Dictionary&lt;System.String, System.String[]&gt;.</returns>
        //public new Dictionary<string, Func<string, string, bool>> this[string key]
        //{
        //    get
        //    {
        //        if (ContainsKey(key) == false)
        //            return this["IsBlank"];

        //        return this[key];
        //    }
        //    set
        //    {
        //        if (ContainsKey(key) == false)
        //            this.Add(key, null);

        //        this[key] = value;
        //    }
        //}
        
        /// <summary>
        /// Creates the matrix.
        /// </summary>
        /// <returns>DataTable.</returns>
        public static DataTable CreateFunctionMatrix()
        {
            ValidatorRepository repo = new ValidatorRepository();

            DataTable table = new DataTable("Matrix");
            table.Columns.Add("Attribute Name");
            foreach (var item in repo)
            {
                table.Columns.Add(item.Key);
            }
            return table;
        }

        /// <summary>
        /// Gets the return side.
        /// </summary>
        /// <param name="method">The method.</param>
        /// <param name="value">The value.</param>
        /// <param name="result">if set to <c>true</c> [result].</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        private static bool ShouldReturn(string method, bool result)
        {
                      
            if (method.First() == '+' && result == false)
                return false;
            if (method.First() == '-' && result == true)
                return false;

            return true;
        }
        /// <summary>
        /// Gets the function list.
        /// </summary>
        /// <returns>System.String[].</returns>
        public static string[] GetFunctionList()
        {
            ValidatorRepository repo = new ValidatorRepository();
            return repo.Where(w=> w.Key.First() != '+' && w.Key.First() != '-' ).Select(s => s.Key).ToArray();
        }
        
        /// <summary>
        /// Gets the functions.
        /// </summary>
        /// <returns>List&lt;ValidationMacro&gt;.</returns>
        public static List<ValidationMacro> GetMacros()
        {
            List<ValidationMacro> macros = new List<ValidationMacro>();            
            GetFunctionList().ToList().ForEach(item => macros.Add(new ValidationMacro(item)));
            return macros;
        }

        /// <summary>
        /// Gets the function summary.
        /// </summary>
        /// <param name="fields">The fields.</param>
        /// <returns>DataTable.</returns>
        public static DataTable GetFunctionSummary(List<FieldSchemaModel> fields, List<ValidationEventArgs> items)
        {
            var cols = fields.Where(w => string.IsNullOrEmpty(w.ValidationMacros) == false).ToList();

            DataTable table = new DataTable("Summary");
            table.Columns.Add("Descrption");
            table.Columns.Add("True (Count)", typeof(int));
            table.Columns.Add("False (Count)", typeof(int));         

            foreach (var col in cols)
            {
                var dataRow = table.NewRow();
                dataRow[0] = col.Name;          
                table.Rows.Add(dataRow);
                foreach (var method in ParseFunctions(col.ValidationMacros))
                {
                    dataRow = table.NewRow();                    
                    dataRow[0] = "\t" + method.Trim();            
                    
                    dataRow["True (Count)"] = items.Where(w => w.FieldName == col.Name && w.Method == method).
                        Select(s => s.Result).Where(w1 => w1 == true).Count(); 

                    dataRow["False (Count)"] = items.Where(w => w.FieldName == col.Name && w.Method == method).
                        Select(s => s.Result).Where(w1 => w1 == false).Count(); 
                    table.Rows.Add(dataRow);
                }
            }
            return table;
        }
        /// <summary>
        /// Executes the specified table.
        /// </summary>
        /// <param name="table">The table.</param>
        /// <param name="fields">The fields.</param>
        public void Execute(DataTable table, List<FieldSchemaModel> fields)
        {
            ValidatorRepository validator = new ValidatorRepository();
            var cols = fields.Where(w => string.IsNullOrEmpty(w.ValidationMacros) == false).ToList();
            validator.LoadLists(string.Join("; ", cols.Select(s => s.ValidationMacros).ToList()));

            string[] keyFields = { "DOC_DOCUMENT_NO", "DOC_DOCUMENT_REV"};
            foreach (DataRow row in table.Rows)
            {                   
                foreach (var col in cols)
                {   
                    string value = row[col.Name]?.ToString();
                    foreach (var method in ParseFunctions(col.ValidationMacros))
                    {       
                        var rtype = method.First() == '+' || method.First() == '-' ? true : false;

                        var items = ParseParameters(method);
                        var result = validator[items.First()](value, method);
                        if (ShouldReturn(method, result))
                            OnValidate(this, new ValidationEventArgs(col.Name, method, result, row, keyFields, table.Rows.Count, value));
                        //Debug.Print("{0} {1} {2} {3}", col.Name, method, value, result);
                    }
                }
            }
        }
        /// <summary>
        /// Looks the in list.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="parms">The parms.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public bool LookInList(string value, string parms)
        {
            value = GetString(value, parms);
            var file = ParseParameters(parms).Last();

            return this.lookupLists[file].
                SingleOrDefault(s => s == value)?.Count() > 0;

        }
       
        /// <summary>
        /// Verifies the date format.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="parms">The parms.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        private static bool VerifyDateFormat(string value, string parms)
        {
            var format = ParseParameters(parms).Last();
            var dateValue = GetString(value, parms);
            try
            {
                DateTime.ParseExact(dateValue, format, CultureInfo.InvariantCulture);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        
        /// <summary>
        /// Greaters the than integer.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="parms">The parms.</param>
        /// <returns>System.Boolean.</returns>
        private static bool GreaterThanEqualInteger(string value, string parms)
        {
            var filter = ParseParameters(parms).Last().ToInteger();
            var intvalue = GetString(value, parms).ToInteger();

            return intvalue.CompareTo(filter) >= 0;
        }

        /// <summary>
        /// Greaters the than equal date.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="parms">The parms.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        private static bool GreaterThanEqualDate(string value, string parms)
        {
            var filter = ParseParameters(parms).Last().ToDateTime();
            var dateValue = GetString(value, parms).ToDateTime();

            return dateValue.CompareTo(filter) >= 0;
        }
        
        /// <summary>
        /// Parses the function.
        /// </summary>
        /// <param name="parms">The parms.</param>
        /// <returns>System.String[].</returns>
        public static string[] ParseParameters(string parms)
        {
            return parms.Split(new char[] { '(', ')', ',', ' ', '\"' }, StringSplitOptions.RemoveEmptyEntries);
        }
        
        /// <summary>
        /// Loads the lists.
        /// </summary>
        /// <param name="functions">The functions.</param>
        public void LoadLists(string functions)
        {
            var methods = ParseFunctions(functions).Where(w => w.StartsWith("LookInList")).ToList();
            foreach (var method in methods)
            {
                var file = ParseParameters(method).Last();
                this.lookupLists.Add(file, File.ReadAllLines(file));
            }

        }
        
        /// <summary>
        /// Parses the functions.
        /// </summary>
        /// <param name="parms">The parms.</param>
        /// <returns>System.String[].</returns>
        public static string[] ParseFunctions(string parms)
        {
            return parms.Split(new string[] { "AND", ";", "\r", "\n", "\t"  }, StringSplitOptions.RemoveEmptyEntries);
        }
        
        /// <summary>
        /// Gets the string.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="parms">The parms.</param>
        /// <returns>System.String.</returns>
        public static string GetString(string value, string parms)
        {
            var items = ParseParameters(parms);
            if (items.Count() == 1)
                return value;
            return value.Substring(items[1].ToInteger(), items[2].ToInteger());
        }
    }
}
