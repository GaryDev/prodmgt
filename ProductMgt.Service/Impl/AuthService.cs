using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductMgt.Service
{
    public class AuthService : IAuthService
    {
        public bool Authenticate(string username, string password)
        {
            return true;
        }
    }
}
