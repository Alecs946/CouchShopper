using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CouchShopper.Data.DTOs.Response.Order
{
    public class OrderUserOrdersListResponse
    {
        public int TotalEntities { get; set; }

        public int Offset { get; set; }

        public List<OrderUserOrdersResponse> Orders { get; set; }

    }
}
