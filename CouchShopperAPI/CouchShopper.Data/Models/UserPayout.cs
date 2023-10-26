using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CouchShopper.Data.Models
{
    public class UserPayout
    {
        public DateTime OnDate { get; set; }
        public string PayoutMethod { get; set; }
        public double Amount { get; set; }
    }
}
