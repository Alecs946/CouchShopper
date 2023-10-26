using CouchShopper.Data.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CouchShopper.Data.DTOs.Response.Products
{
    public class ProductEarningsResponse
    {
        [JsonProperty(PropertyName = "total_rows")]
        public int TotalEntities { get; set; }

        [JsonProperty(PropertyName = "offset")]
        public int Offset { get; set; }

        [JsonProperty(PropertyName = "rows")]
        public List<ProductEarningsList> Rows { get; set; }
    }
}
