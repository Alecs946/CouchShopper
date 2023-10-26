using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CouchShopper.Data.DTOs.Response.Order
{
    public class OrderOverviewListResponse
    {
        public int TotalEntities { get; set; }

        public int Offset { get; set; }

        public List<OrderOverviewResponse> Orders { get; set; }
    }
}
