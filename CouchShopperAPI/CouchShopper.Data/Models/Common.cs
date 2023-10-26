using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CouchShopper.Data.Models
{
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class Common: BaseEntity
    {
        public List<Category> Categories { get; set; }
        public List<Country> Countries { get; set; }
        public List<City> Cities { get; set; }
        public List<Icon> Icons { get; set; }
        public List<SpecialOffer> SpecialOffers { get; set; }

        public Dictionary<string, DocumentAttachment>? _attachments { get; set; }
    }
}
