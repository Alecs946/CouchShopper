using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CouchShopper.Data.Models
{
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class PaymentMethod
    {
        public string Id { get; set; }

        public string CardName { get; set; }

        public string CardNumber { get; set; }

        public string NameOnCard { get; set; }

        public string ExpiryDate { get; set; }

        public string Email { get; set; }

        public int PaymentMethodType { get; set; }

        public bool IsPrimary { get; set; }

    }
}
