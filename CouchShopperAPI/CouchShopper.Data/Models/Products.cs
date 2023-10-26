using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CouchShopper.Data.Models
{
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class Products : BaseEntity
    {
        public string UserId { get; set; }

        public string DefaultPhotoId { get; set; }

        public string Name { get; set; }

        public string Brand { get; set; }

        public string Model { get; set; }

        public double Price { get; set; }

        public double? SalePrice { get; set; }

        public bool FeatureProduct { get; set; }

        public int Quantity { get; set; }

        public string Category { get; set; }

        public string ProductDescription { get; set; }

        public int NumberOfReviews { get; set; }

        public int NumberOfSales { get; set; }

        public double Earnings { get; set; }

        public DateTime CreatedOn { get; set; }

        public List<ProductOptions> Options { get; set; }

        public List<ProductReview> Reviews { get; set; }

        public Dictionary<string, DocumentAttachment>? _attachments { get; set; }

    }
}
