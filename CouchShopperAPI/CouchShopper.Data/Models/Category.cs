using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CouchShopper.Data.Models
{
    public class Category
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string IconName { get; set; }

        public bool Deleted { get; set; }
    }
}
