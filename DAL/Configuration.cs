using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace DAL
{
    public abstract class Configuration
    {
        public static String ConnectionString
        {
            get
            {
                string ConnStr = String.Format(System.Configuration.ConfigurationManager.ConnectionStrings["SqlConnection"].ConnectionString);
                return ConnStr;
            }
        }
    }
}
