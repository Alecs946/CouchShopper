using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CouchShopper.Data.DTOs.Request.Common.Cities
{
    public class CityUpdateRequest
    {
        public string Id { get; set; }

        public string CountryId { get; set; }

        public string Name { get; set; }

    }
}
