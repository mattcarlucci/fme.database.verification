using System;
using System.Xml.Serialization;

namespace Fme.Library.Models
{
    [Serializable]
    public class ErrorMessageModel
    {

        public ErrorMessageModel(string source, string message, string stackTrace)
        {
            this.Source = source;
            this.TimeStamp = DateTime.Now;
            this.Message = message;
            this.StackTrace = stackTrace;
        }

        public DateTime TimeStamp { get; set; }  
        public string Source { get; set; }
        public string Message { get; set; }       
        public string StackTrace { get; set; }
    }
}
