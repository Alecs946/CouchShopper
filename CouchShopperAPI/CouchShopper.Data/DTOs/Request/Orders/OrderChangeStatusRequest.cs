using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CouchShopper.Data.DTOs.Request.Orders
{
    public class OrderChangeStatusRequest
    {
        public string OrderId { get; set; }

        public string Reason { get; set; }
    }
}
