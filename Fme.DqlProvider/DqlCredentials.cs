using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace Fme.DqlProvider
{
    public sealed class DqlCredentials
    {
        private readonly SecureString password;
        private readonly string userId;
        
        public DqlCredentials(string userId, SecureString password)
        {
            this.password = password;
            this.userId = userId;
        }
    }
}
