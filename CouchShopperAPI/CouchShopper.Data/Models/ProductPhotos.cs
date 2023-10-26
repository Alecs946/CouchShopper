using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CouchShopper.Data.Models
{
    public class ProductPhotos 
    {
        public string Id { get; set; }

        public byte[] Content { get; set; }
        public bool IsDefault { get; set; }
    }
}
