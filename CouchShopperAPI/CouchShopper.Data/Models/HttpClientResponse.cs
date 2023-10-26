using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CouchShopper.Data.Models
{
    public class HttpClientResponse
    {
        public bool IsSuccess { get; set; }
        public string SuccessContentObject { get; set; }
        public string FailedReason { get; set; }
    }
}
