using CouchShopper.Business.Exceptions;
using CouchShopper.Data.DTOs.Request.Orders;
using CouchShopper.Data.DTOs.Request.Seller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CouchShopper.Business.Validators
{
    public static class SellerValidations
    {
        public static void Validate(this SellerRatingRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.UserId))
            {
                throw new InvalidRequestException($"User is required.");
            }
            if (string.IsNullOrWhiteSpace(request.SellerId))
            {
                throw new InvalidRequestException($"Seller is required.");
            }
            if (request.Rating<=0)
            {
                throw new InvalidRequestException($"Order is required.");
            }

        }
        public static void Validate(this SellerRevenue request)
        {
            if (string.IsNullOrWhiteSpace(request.UserId))
            {
                throw new InvalidRequestException($"User is required.");
            }
            if (request.ProductIds==null || (request.ProductIds!=null && !request.ProductIds.Any()))
            {
                throw new InvalidRequestException($"Products are required.");
            }
            
        }
        public static void Validate(this SellerWithdrawalRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.UserId))
            {
                throw new InvalidRequestException($"User is required.");
            }
        }

    }
}
