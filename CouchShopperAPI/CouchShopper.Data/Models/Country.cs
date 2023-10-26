using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CouchShopper.Data.Models
{
    public class Country
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public bool Deleted { get; set; }

        public double DestinationCharge { get; set; }

        public double SaverDestinationCharge { get; set; }

        public int BaseNumberOfDays { get; set; }

    }
}
