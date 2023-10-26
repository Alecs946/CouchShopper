using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CouchShopper.Data.DTOs.Request.Favorites
{
    public  class FavoritesDeleteRequest
    {
        public string UserId { get; set; }

        public string ProductId { get; set; }
    }
}
