using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CouchShopper.Data.Models
{
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class SpecialOffer
    {
        public string Id { get; set; }

        public string Title { get; set; }

        public string BackgroundColor { get; set; }

        public string Description { get; set; }

        public bool Deleted { get; set; }
        
        public bool Published { get; set; }

        public DateTime CreatedOn { get; set; }

        public string PhotoId { get; set; }

        public string TextColor { get; set; }

        public Dictionary<string, DocumentAttachment>? _attachments { get; set; }

    }
}
