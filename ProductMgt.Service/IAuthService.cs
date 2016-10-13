using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductMgt.Service
{
    public interface IAuthService
    {
        bool Authenticate(string username, string password);
    }
}
