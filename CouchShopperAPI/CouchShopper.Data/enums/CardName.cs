using CouchShopper.Data.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CouchShopper.Data.Enums
{
    public enum CardName
    {
        [EnumString("visa")]
        Visa = 1,
        [EnumString("mastercard")]
        MasterCard = 2,
        [EnumString("amex")]
        AmericanExpress = 3,
    }
}
