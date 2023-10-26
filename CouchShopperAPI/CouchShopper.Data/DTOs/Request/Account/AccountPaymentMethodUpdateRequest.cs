using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CouchShopper.Data.DTOs.Request.Account
{
    public class AccountPaymentMethodUpdateRequest
    {
        public string UserId { get; set; }
        public string PaymentId { get; set; }
    }
}
