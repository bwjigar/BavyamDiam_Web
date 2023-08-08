using System.Collections.Generic;

namespace Lib.Models
{
    public class CommonResponse
    {
        public string Message { get; set; }
        public string Status { get; set;}
        public string Error { get; set; }
    }

    public class ServiceResponse<T>
    {
        public List<T> Data { get; set; }
        public string Message { get; set; }
        public string Status { get; set; }
    }
}
