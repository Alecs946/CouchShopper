using CouchShopper.Data.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CouchShopper.Data.Enums
{
    public enum PaymentMethods
    {
        [EnumString("Card")]
        Card =1,
        [EnumString("Paypal")]
        Paypal=2
    }
}
