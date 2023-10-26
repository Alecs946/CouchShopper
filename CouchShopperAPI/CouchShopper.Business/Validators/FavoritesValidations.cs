using CouchShopper.Business.Exceptions;
using CouchShopper.Data.DTOs.Request.Account;
using CouchShopper.Data.DTOs.Request.Favorites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CouchShopper.Business.Validators
{
    public static class FavoritesValidations
    {
        public static void Validate(this FavoritesAddRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.UserId))
            {
                throw new InvalidRequestException($"Username is required.");
            }
            if (string.IsNullOrWhiteSpace(request.ProductId))
            {
                throw new InvalidRequestException($"Product is required.");
            }
        }
        public static void Validate(this FavoritesDeleteRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.UserId))
            {
                throw new InvalidRequestException($"Username is required.");
            }
            if (string.IsNullOrWhiteSpace(request.ProductId))
            {
                throw new InvalidRequestException($"Product is required.");
            }
        }
    }
}
