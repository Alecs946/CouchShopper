using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CouchShopper.Data.DTOs.Request.Account
{
    public class AccountAddPaymentCreditCardRequest
    {
        public string Username { get; set; }

        public int CardName { get; set; }

        public string CardNumber { get; set; }

        public string NameOnCard { get; set; }

        public string ExpiryDate { get; set; }

        public bool IsPrimary { get; set; }

    }
}
