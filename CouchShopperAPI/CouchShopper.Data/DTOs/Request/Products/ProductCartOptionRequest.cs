using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CouchShopper.Data.DTOs.Request.Products
{
    public class ProductCartOptionRequest
    {
        public string OptionName { get; set; }

        public string OptionValue { get; set; }
    }
}
