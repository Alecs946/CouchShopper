using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CouchShopper.Data.DTOs.Request.Common.SpecialOffer
{
    public class SpecialOfferUpdateRequest
    {
        public string Id { get; set; }

        public string Title { get; set; }

        public string ImageBase64 { get; set; }

        public string BackgroundColor { get; set; }

        public string Description { get; set; }

        public string TextColor { get; set; }
    }
}
