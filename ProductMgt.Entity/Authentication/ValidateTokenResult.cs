using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductMgt.Entity
{
    public class ValidateTokenResult
    {
        public bool IsTokenValid { get; set; }
        public string AppSecUserName { get; set; }
        public string StaffCode { get; set; }
        public DateTime? LastActiveTime { get; set; }
    }
}
