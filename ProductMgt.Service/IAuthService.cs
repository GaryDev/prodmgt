using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace ProductMgt.Service
{
    public interface IAuthService
    {
        bool Authenticate(string username, string password);
        bool ValidateToken(string token);
        void SetSession(HttpContext context);
    }
}
