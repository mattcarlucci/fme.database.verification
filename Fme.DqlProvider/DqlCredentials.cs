// ***********************************************************************
// Assembly         : Fme.DqlProvider
// Author           : mcarlucci
// Created          : 08-20-2017
//
// Last Modified By : mcarlucci
// Last Modified On : 08-20-2017
// ***********************************************************************
// <copyright file="DqlCredentials.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace Fme.DqlProvider
{
    /// <summary>
    /// Class DqlCredentials. This class cannot be inherited.
    /// </summary>
    public sealed class DqlCredentials
    {
        /// <summary>
        /// The password
        /// </summary>
        private readonly SecureString password;
        /// <summary>
        /// The user identifier
        /// </summary>
        private readonly string userId;

        /// <summary>
        /// Initializes a new instance of the <see cref="DqlCredentials"/> class.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="password">The password.</param>
        public DqlCredentials(string userId, SecureString password)
        {
            this.password = password;
            this.userId = userId;
        }
    }
}
