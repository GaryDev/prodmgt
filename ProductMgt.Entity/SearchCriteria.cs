using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductMgt.Entity
{
    public class SearchCriteria
    {
        public static readonly string KEY = "SearchCriteria";

        public string ProductName { get; set; }
        public string ProductSku { get; set; }
    }
}
