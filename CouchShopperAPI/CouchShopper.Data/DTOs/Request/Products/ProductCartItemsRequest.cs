using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CouchShopper.Data.DTOs.Request.Products
{
    public class ProductCartItemsRequest
    {
        public string ProductId { get; set; }

        public int Quantity { get; set; }

        public List<ProductCartOptionRequest> SelectedOption { get; set; }
    }
}
