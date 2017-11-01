// ***********************************************************************
// Assembly         : Fme.Library
// Author           : mcarlucci
// Created          : 08-19-2017
//
// Last Modified By : mcarlucci
// Last Modified On : 08-23-2017
// ***********************************************************************
// <copyright file="ErrorMessageModel.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Xml.Serialization;

namespace Fme.Library.Models
{
    /// <summary>
    /// Class ErrorMessageModel.
    /// </summary>
    [Serializable]
    public class ErrorMessageModel
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="ErrorMessageModel"/> class.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="message">The message.</param>
        /// <param name="stackTrace">The stack trace.</param>
        public ErrorMessageModel(string source, string message, string stackTrace)
        {
            this.Source = source;
            this.TimeStamp = DateTime.Now;
            this.Message = message;
            this.StackTrace = stackTrace;
        }

        /// <summary>
        /// Gets or sets the time stamp.
        /// </summary>
        /// <value>The time stamp.</value>
        public DateTime TimeStamp { get; set; }
        /// <summary>
        /// Gets or sets the source.
        /// </summary>
        /// <value>The source.</value>
        public string Source { get; set; }
        /// <summary>
        /// Gets or sets the message.
        /// </summary>
        /// <value>The message.</value>
        public string Message { get; set; }
        /// <summary>
        /// Gets or sets the stack trace.
        /// </summary>
        /// <value>The stack trace.</value>
        public string StackTrace { get; set; }
    }
}
