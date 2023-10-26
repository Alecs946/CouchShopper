using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CouchShopper.Data.Models
{
    public class OrderItem
    {
        public string Id { get; set; }
        public string ProductId { get; set; }

        public string ProductName { get; set; }

        public List<SelectedOption> Options { get; set; }

        public string SellerId { get; set; }

        public int Quantity { get; set; }

        public double Price { get; set; }

        public string PhotoId { get; set; }

        public bool Rated { get; set; }

    }
}
