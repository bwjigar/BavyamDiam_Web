using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib.Models
{
    public class OAuthErrorMsg
    {
        public string error { get; set; }
        public string error_description { get; set; }
        public string error_uri { get; set; }
    }
}
