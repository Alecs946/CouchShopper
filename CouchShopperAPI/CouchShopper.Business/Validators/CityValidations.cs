using CouchShopper.Business.Exceptions;
using CouchShopper.Data.DTOs.Request.Common.Cities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CouchShopper.Business.Validators
{
    public static class CityValidations
    {
        public static void Validate(this CityAddRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Name))
            {
                throw new InvalidRequestException($"Name is required.");
            }
            if (string.IsNullOrWhiteSpace(request.CountryId))
            {
                throw new InvalidRequestException($"Country is required.");
            }
        }
        public static void Validate(this CityDeleteRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.CityId))
            {
                throw new InvalidRequestException($"City is required.");
            }
        }
        public static void Validate(this CityUpdateRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Name))
            {
                throw new InvalidRequestException($"Name is required.");
            }
        }

    }
}
