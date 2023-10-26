using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CouchShopper.Data.DTOs.Request.Products
{
    public class ProductOptionRequest
    {
        public string ProductOptionName { get; set; }

        public List<string> ProductOptionValues { get; set; }
    }
}
