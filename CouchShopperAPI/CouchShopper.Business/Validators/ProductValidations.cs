using CouchShopper.Business.Exceptions;
using CouchShopper.Data.DTOs.Request.Orders;
using CouchShopper.Data.DTOs.Request.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CouchShopper.Business.Validators
{
    public static class ProductValidations
    {
        public static void Validate(this ProductAddRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.UserId))
            {
                throw new InvalidRequestException($"User is required.");
            }
            if (string.IsNullOrWhiteSpace(request.Name))
            {
                throw new InvalidRequestException($"Product Name is required.");
            }
            if (string.IsNullOrWhiteSpace(request.Brand))
            {
                throw new InvalidRequestException($"Brand is required.");
            }
            if (request.Price <= 0)
            {
                throw new InvalidRequestException($"Price is required.");
            }
            if (request.Quantity <= 0)
            {
                throw new InvalidRequestException($"Quantity is required.");
            }
            if (string.IsNullOrWhiteSpace(request.ProductDescription))
            {
                throw new InvalidRequestException($"Product Description is required.");
            }
            if (!request.Options.Any())
            {
                throw new InvalidRequestException($"At least one product option is required.");
            }

        }

        public static void Validate(this ProductDeleteRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.ProductId))
            {
                throw new InvalidRequestException($"Product is required.");
            }

        }

        public static void Validate(this ProductUpdateRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Id))
            {
                throw new InvalidRequestException($"Product is required.");
            }
            if (string.IsNullOrWhiteSpace(request.UserId))
            {
                throw new InvalidRequestException($"User is required.");
            }
            if (string.IsNullOrWhiteSpace(request.Name))
            {
                throw new InvalidRequestException($"Product Name is required.");
            }
            if (string.IsNullOrWhiteSpace(request.Brand))
            {
                throw new InvalidRequestException($"Brand is required.");
            }
            if (request.Price <= 0)
            {
                throw new InvalidRequestException($"Price is required.");
            }
            if (request.Quantity <= 0)
            {
                throw new InvalidRequestException($"Quantity is required.");
            }
            if (string.IsNullOrWhiteSpace(request.ProductDescription))
            {
                throw new InvalidRequestException($"Product Description is required.");
            }
            if (!request.Options.Any())
            {
                throw new InvalidRequestException($"At least one product option is required.");
            }
        }

        public static void Validate(this ProductRatingRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.UserId))
            {
                throw new InvalidRequestException($"User is required.");
            }
            if (string.IsNullOrWhiteSpace(request.ProductId))
            {
                throw new InvalidRequestException($"Product is required.");
            }
            if (request.Rating <= 0)
            {
                throw new InvalidRequestException($"Rating is required.");
            }

        }
    }
}
