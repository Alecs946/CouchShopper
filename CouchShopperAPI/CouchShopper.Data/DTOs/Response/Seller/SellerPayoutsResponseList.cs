using CouchShopper.Data.DTOs.Response.Account;
using CouchShopper.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CouchShopper.Data.DTOs.Response.Seller
{
    public class SellerPayoutsResponseList
    {
        public double Balance { get; set; }

        public int TotalEntities { get; set; }

        public int Offset { get; set; }

        public SellerPaymentMethodResponse PaymentMethod { get; set; }

        public List<SellerPayoutResponse> Payouts { get; set; }
    }
}
