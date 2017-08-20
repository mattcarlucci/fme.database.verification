using System;
using System.Xml.Serialization;

namespace Fme.Library.Models
{
    [Serializable]
    public class ErrorMessageModel
    {
       

        public ErrorMessageModel(string Message, string StackTrace)
        {
            this.TimeStamp = DateTime.Now;
            this.Message = Message;
            this.StackTrace = StackTrace;
        }

        public DateTime TimeStamp { get; set; }
       // [XmlElement("CDataElement")]
        public string Message { get; set; }
       // [XmlElement("CDataElement")]
        public string StackTrace { get; set; }
    }
}
