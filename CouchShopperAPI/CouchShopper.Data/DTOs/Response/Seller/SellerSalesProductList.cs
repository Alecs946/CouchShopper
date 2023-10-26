using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CouchShopper.Data.DTOs.Response.Seller
{
    public class SellerSalesProductList
    {
        [JsonProperty(PropertyName = "total_rows")]
        public int TotalEntities { get; set; }

        [JsonProperty(PropertyName = "offset")]
        public int Offset { get; set; }

        [JsonProperty(PropertyName = "rows")]

        public List<SellerSalesProductsList> Rows { get; set; }
    }
    public class SellerSalesProductsList
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("key")]
        public string[] Key { get; set; }

        [JsonProperty("value")]
        public SellerSalesProductsDetailsRow Revenue { get; set; }
    }
    public class SellerSalesProductsDetailsRow
    {
        [JsonProperty("productName")]
        public string ProductName { get; set; }

        [JsonProperty("quantity")]
        public int Quantity { get; set; }

        [JsonProperty("revenue")]
        public double Revenue { get; set; }
    }

}
