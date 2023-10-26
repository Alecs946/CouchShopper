namespace CouchShopper.Data.DTOs.Request.Common.SpecialOffer
{
    public class SpecialOfferAddRequest
    {
        public string Title { get; set; }

        public string ImageBase64 { get; set; }

        public string BackgroundColor { get; set; }

        public string Description { get; set; }

        public string TextColor { get; set; }
    }
}
