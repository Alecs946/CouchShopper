using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CouchShopper.Data.Models
{
    public class ProductOptions
    {
        public string ProductOptionName { get; set; }
        public List<string> ProductOptionValues { get; set; }
    }
}
