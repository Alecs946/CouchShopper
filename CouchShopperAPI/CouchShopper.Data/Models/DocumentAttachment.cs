using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CouchShopper.Data.Models
{

    public class DocumentAttachment
    {
        public string content_type { get; set; }
        public int revpos { get; set; }
        public string digest { get; set; }
        public int length { get; set; }
        public bool stub { get; set; }
    }
}
