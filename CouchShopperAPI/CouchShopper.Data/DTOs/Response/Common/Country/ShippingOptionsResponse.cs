using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CouchShopper.Data.DTOs.Response.Common.Country
{
    public class ShippingOptionsResponse
    {
        public string ShippingMethodName { get; set; }

        public int MinNumberOfDays { get; set; }
        public int MaxNumberOfDays { get; set; }
        public double ShippingPrice { get; set; }
    }
}
