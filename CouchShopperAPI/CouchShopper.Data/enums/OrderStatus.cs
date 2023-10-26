using CouchShopper.Data.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CouchShopper.Data.enums
{
    public enum OrderStatus
    {
        [EnumString("Created")]
        Created =1,
        [EnumString("Approved")]
        Approved=2,
        [EnumString("Declined")]
        Declined=3,
        [EnumString("Sent")]
        Sent=4,
        [EnumString("Delivered")]
        Delivered=5
    }
}
