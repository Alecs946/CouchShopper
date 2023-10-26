using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace CouchShopper.Data.Models
{
    public class BaseEntity
    {
        [JsonProperty(PropertyName = "_id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "_rev")]
        public string Rev { get; set; }

        public bool Deleted { get; set; }

        //[JsonProperty(PropertyName = "_attachments")]
        //public Dictionary<string, DocumentAttachment> Attachments { get; set; }
    }
}
