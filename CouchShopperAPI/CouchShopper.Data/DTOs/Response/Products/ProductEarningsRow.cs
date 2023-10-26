using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CouchShopper.Data.DTOs.Response.Products
{
    public class ProductEarningsRow
    {
        [JsonProperty("sales")]
        public int Sales { get; set; }

        [JsonProperty("earnings")]
        public double Earnings { get; set; }
    }
}
