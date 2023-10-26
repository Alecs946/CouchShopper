using CouchShopper.Data.DTOs.Response.Products;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace CouchShopper.Data.DTOs.Response.Seller
{
    public class SellerSalesResponseList
    {
        [JsonProperty(PropertyName = "total_rows")]
        public int TotalEntities { get; set; }

        [JsonProperty(PropertyName = "offset")]
        public int Offset { get; set; }

        [JsonProperty(PropertyName = "rows")]

        public List<SellerSalesList> Rows { get; set; }
    }
    public class SellerSalesList
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("key")]
        public string[] Key { get; set; }

        [JsonProperty("value")]
        public SellerSalesDetailsRow Revenue { get; set; }
    }
    public class SellerSalesDetailsRow
    {
        [JsonProperty("date")]
        public DateTime Date { get; set; }

        [JsonProperty("revenue")]
        public double Revenue { get; set; }
    }

    public class SellerSalesKey
    {
        public string user { get; set; }
        public DateTime date { get; set; }
    }
}
