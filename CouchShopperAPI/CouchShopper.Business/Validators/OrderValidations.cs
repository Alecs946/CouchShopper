using CouchShopper.Business.Exceptions;
using CouchShopper.Data.DTOs.Request.Common.Icon;
using CouchShopper.Data.DTOs.Request.Orders;
using CouchShopper.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CouchShopper.Business.Validators
{
    public static class OrderValidations
    {
        public static void Validate(this OrderAddRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.ShippingType))
            {
                throw new InvalidRequestException($"Shipping Type is required.");
            }
            if (request.Price <= 0)
            {
                throw new InvalidRequestException($"Price is required.");
            }
            if (string.IsNullOrWhiteSpace(request.BuyerUsername))
            {
                throw new InvalidRequestException($"Buyer is required.");
            }
            if (string.IsNullOrWhiteSpace(request.BuyerFullName))
            {
                throw new InvalidRequestException($"Name  is required.");
            }
            if (string.IsNullOrWhiteSpace(request.BuyerPhone))
            {
                throw new InvalidRequestException($"Phone is required.");
            }
            if (string.IsNullOrWhiteSpace(request.ShippingAddress))
            {
                throw new InvalidRequestException($"Shipping Address is required.");
            }
            if (request.ShippingPrice<0)
            {
                throw new InvalidRequestException($"Shipping Price is required.");
            }
            if (string.IsNullOrWhiteSpace(request.PaymentMethodId))
            {
                throw new InvalidRequestException($"Payment Method is required.");
            }
            if (!request.OrderItems.Any())
            {
                throw new InvalidRequestException($"Order does not contain any item.");
            }
            
        }
        public static void Validate(this OrderChangeStatusRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.OrderId))
            {
                throw new InvalidRequestException($"Order is required.");
            }
        }
        public static void Validate(this OrderRatingRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.UserId))
            {
                throw new InvalidRequestException($"User is required.");
            }
            if (string.IsNullOrWhiteSpace(request.ItemId))
            {
                throw new InvalidRequestException($"Item is required.");
            }
            if (string.IsNullOrWhiteSpace(request.OrderId))
            {
                throw new InvalidRequestException($"Order is required.");
            }
            if (string.IsNullOrWhiteSpace(request.SellerId))
            {
                throw new InvalidRequestException($"Seller is required.");
            }
            if (request.SellerRating<=0)
            {
                throw new InvalidRequestException($"Seller Rating is required.");
            }
            if (request.ProductRating <= 0)
            {
                throw new InvalidRequestException($"Product Rating is required.");
            }
        }
    }
}
