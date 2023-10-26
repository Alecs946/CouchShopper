using CouchShopper.Business.Exceptions;
using CouchShopper.Data.DTOs.Request.Common.Country;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CouchShopper.Business.Validators
{
    public static class CountryValidations
    {
        public static void Validate(this CountryAddRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Name))
            {
                throw new InvalidRequestException($"Name is required.");
            }
            if (request.DestinationCharge<=0)
            {
                throw new InvalidRequestException($"Premium Shipping Charge is required.");
            }
            if (request.SaverDestinationCharge<=0)
            {
                throw new InvalidRequestException($"Shipping Charge is required.");
            }
        }

        public static void Validate(this CountryDeleteRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.CountryId))
            {
                throw new InvalidRequestException($"Country is required.");
            }
        }

        public static void Validate(this CountryUpdateRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Id))
            {
                throw new InvalidRequestException($"Country is required.");
            }
            if (string.IsNullOrWhiteSpace(request.Name))
            {
                throw new InvalidRequestException($"Name is required.");
            }
            if (request.DestinationCharge <= 0)
            {
                throw new InvalidRequestException($"Premium Shipping Charge is required.");
            }
            if (request.SaverDestinationCharge <= 0)
            {
                throw new InvalidRequestException($"Shipping Charge is required.");
            }
        }
    }
}
