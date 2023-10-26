using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CouchShopper.Data.Models
{
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class BaseViewEntity<T> where T : class
    {
        [JsonProperty(PropertyName = "total_rows")]
        public int TotalEntities { get; set; }

        [JsonProperty(PropertyName = "offset")]
        public int Offset { get; set; }

        [JsonProperty(PropertyName = "rows")]
        public List<ViewRow<T>> Rows { get; set; }
    }
}
