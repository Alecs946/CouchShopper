using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CouchShopper.Data.DTOs.Request.Products
{
    public class ProductRatingRequest
    {
        public string UserId { get; set; }
        public string ProductId { get; set; }
        public string Comment { get; set; }
        public int Rating { get; set; }

    }
}
