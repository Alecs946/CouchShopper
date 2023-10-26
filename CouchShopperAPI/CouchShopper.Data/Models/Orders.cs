using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CouchShopper.Data.Models
{
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class Orders : BaseEntity
    {
        public List<OrderItem> OrderItems { get; set; }

        public string ShippingType { get; set; }

        public double Price { get; set; }

        public string BuyerUsername { get; set; }

        public string BuyerFullName { get; set; }

        public string BuyerPhone { get; set; }

        public string ShippingAddress { get; set; }

        public string PaymentMethodId { get; set;}

        public int OrderStatus { get; set; }

        public DateTime CreatedOn { get; set; }

        public string DeclineReason { get; set; }

        public Dictionary<string, DocumentAttachment>? _attachments { get; set; }
    }
}
