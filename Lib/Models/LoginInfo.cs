using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib.Models
{
    public class LoginInfoResponse
    {
        public long iTotalRec { get; set; }
        public string LoginDate { get; set; }
        public string LoginTime { get; set; }
        public string sUsername { get; set; }
        public string BrowserName { get; set; }
        public string Platform { get; set; }
        public string sCompName { get; set; }
        public string sCompCountry { get; set; }
        public string CustomerName { get; set; }
        public string LoginType { get; set; }
    }
}
