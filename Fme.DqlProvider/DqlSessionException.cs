// ***********************************************************************
// Assembly         : DqlProvider
// Author           : mcarlucci
// Created          : 08-12-2017
//
// Last Modified By : mcarlucci
// Last Modified On : 08-14-2017
// ***********************************************************************
// <copyright file="DqlSessionException.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Fme.DqlProvider
{
    /// <summary>
    /// Class DqlSessionException.
    /// </summary>
    /// <seealso cref="System.Exception" />
    public class DqlSessionException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DqlSessionException"/> class.
        /// </summary>
        public DqlSessionException()
        {
            
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DqlSessionException"/> class.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public DqlSessionException(string message) : base(message)
        {
            
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DqlSessionException"/> class.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="innerException">The exception that is the cause of the current exception, or a null reference (Nothing in Visual Basic) if no inner exception is specified.</param>
        public DqlSessionException(string message, Exception innerException) : 
            base(message, innerException)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DqlSessionException"/> class.
        /// </summary>
        /// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> that holds the serialized object data about the exception being thrown.</param>
        /// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext" /> that contains contextual information about the source or destination.</param>
        protected DqlSessionException(SerializationInfo info, StreamingContext context) : 
            base(info, context)
        {
        }
    }
}
