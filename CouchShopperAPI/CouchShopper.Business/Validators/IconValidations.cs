using CouchShopper.Business.Exceptions;
using CouchShopper.Data.DTOs.Request.Common.Category;
using CouchShopper.Data.DTOs.Request.Common.Cities;
using CouchShopper.Data.DTOs.Request.Common.Country;
using CouchShopper.Data.DTOs.Request.Common.Icon;
using CouchShopper.Data.DTOs.Request.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CouchShopper.Business.Validators
{
    public static class IconValidations
    {
        
        public static void Validate(this IconAddRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Name))
            {
                throw new InvalidRequestException($"Name is required.");
            }
        }
        public static void Validate(this IconDeleteRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.IconId))
            {
                throw new InvalidRequestException($"Icon is required.");
            }
        }

        public static void Validate(this IconUpdateRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Name))
            {
                throw new InvalidRequestException($"Name is required.");
            }
        }
    }
}
