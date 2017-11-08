// ***********************************************************************
// Assembly         : Fme.Library
// Author           : mcarlucci
// Created          : 11-03-2017
//
// Last Modified By : mcarlucci
// Last Modified On : 11-03-2017
// ***********************************************************************
// <copyright file="ExternalQueryException.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fme.Library.Exceptions
{
    /// <summary>
    /// Class ExternalQueryException.
    /// </summary>
    /// <seealso cref="System.Exception" />
    public class ExternalQueryException : Exception
    {
        /// <summary>
        /// The old stack trace
        /// </summary>
        private string oldStackTrace;

        /// <summary>
        /// Initializes a new instance of the <see cref="ExternalQueryException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="stackTrace">The stack trace.</param>
        public ExternalQueryException(string message, string stackTrace) 
            : base(message)
        {
            this.oldStackTrace = stackTrace;
        }

        /// <summary>
        /// Gets a string representation of the immediate frames on the call stack.
        /// </summary>
        /// <value>The stack trace.</value>
        /// <PermissionSet>
        ///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
        /// </PermissionSet>
        public override string StackTrace
        {
            get
            {
                return this.oldStackTrace;
            }
        }
    }
}
