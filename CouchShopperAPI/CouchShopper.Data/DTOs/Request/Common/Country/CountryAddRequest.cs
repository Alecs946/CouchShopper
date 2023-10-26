
namespace CouchShopper.Data.DTOs.Request.Common.Country
{
    public class CountryAddRequest
    {
        public string Name { get; set; }

        public double DestinationCharge { get; set; }

        public double SaverDestinationCharge { get; set; }

        public int BaseNumberOfDays { get; set; }
    }
}
