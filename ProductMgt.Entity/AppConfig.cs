using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductMgt.Config
{
    public class AppConfig
    {
        private static AppConfig _instance = new AppConfig();

        private AppConfig()
        { }

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

        public string CraServiceUrl
        {
            get
            {
                if (ConfigurationManager.AppSettings.AllKeys.Contains("CraServiceUrl"))
                    return ConfigurationManager.AppSettings["CraServiceUrl"];

                return null;
            }
        }

        public string SessionTimeout
        {
            get
            {
                if (ConfigurationManager.AppSettings.AllKeys.Contains("SessionTimeout"))
                    return ConfigurationManager.AppSettings["SessionTimeout"];

                return Constants.SESSION_TIMEOUT.ToString();
            }
        }

        public string PageSize
        {
            get
            {
                if (ConfigurationManager.AppSettings.AllKeys.Contains("PageSize"))
                    return ConfigurationManager.AppSettings["PageSize"];

                return Constants.PAGE_SIZE.ToString();
            }
        }
    }
}
