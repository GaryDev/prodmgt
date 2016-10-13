using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductMgt.DataModel
{
    internal class AppConfig
    {
        private static AppConfig _instance = new AppConfig();


        public static AppConfig Instance
        {
            get
            {
                return _instance;
            }
        }

        public string DBConnection
        {
            get
            {
                if (ConfigurationManager.ConnectionStrings["DBConnStr"] != null)
                    return ConfigurationManager.ConnectionStrings["DBConnStr"].ConnectionString;
                else
                    return null;
            }
        }
    }
}
