using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fme.Library
{
    /// <summary>
    /// Class ValidationEventArgs.
    /// </summary>
    /// <seealso cref="System.EventArgs" />
    public class ValidationEventArgs : EventArgs
    {
        public string DocumentNumber { get; set; }
        public string Revision { get; set; }

        public string FieldName { get; set; }
        public string Method { get; set; }
        public string Value { get; set; }
        public bool Result { get; set; }
        public DataRow DataRow { get; set; }
        public int Count { get; set; }


        /// <summary>
        /// Initializes a new instance of the <see cref="ValidationEventArgs"/> class.
        /// </summary>
        /// <param name="fieldName">Name of the field.</param>
        /// <param name="method">The method.</param>
        /// <param name="result">if set to <c>true</c> [result].</param>
        /// <param name="row">The row.</param>
        public ValidationEventArgs(string fieldName, string method, bool result, DataRow row, string[] keys, int count, string value)
        {
            this.FieldName = fieldName;
            this.Method = method;
            this.Result = result;
            this.DataRow = DataRow;
            this.Count = count;
            this.Value = value;
            DocumentNumber = row[keys.First()].ToString();
            Revision = row[keys.Last()].ToString();
        }

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>A <see cref="System.String" /> that represents this instance.</returns>
        public override string ToString()
        {
            return string.Format("{0} {1} {2} {3} {4}", FieldName, Method, Value, Result, Count);
        }
    }
}
