using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CouchShopper.Data.DTOs.Response.Account
{
    public class AccountPaymentMethodResponse
    {
        public string Id { get; set; }
        public string CardNumber { get; set; }

        public string CardName { get; set; }

        public string NameOnCard { get; set; }

        public string ExpiryDate { get; set; }

        public string CVV { get; set; }

        public string Email { get; set; }

        public string PaymentMethodType { get; set; }

        public bool IsCard { get; set; }

        public bool IsPrimary { get; set; }
    }
}
