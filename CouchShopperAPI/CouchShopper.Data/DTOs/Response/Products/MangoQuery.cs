using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CouchShopper.Data.DTOs.Response.Products
{
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class MangoQuery<T> where T : class
    {
        [JsonProperty(PropertyName = "docs")]
        public List<T> Documents { get; set; }
    }
}
