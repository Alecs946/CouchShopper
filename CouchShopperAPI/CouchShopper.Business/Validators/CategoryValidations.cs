using CouchShopper.Business.Exceptions;
using CouchShopper.Data.DTOs.Request.Common.Category;

namespace CouchShopper.Business.Validators
{
    public static class CategoryValidations
    {
        public static void Validate(this CategoryAddRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Name))
            {
                throw new InvalidRequestException($"Name is required.");
            }
            if (string.IsNullOrWhiteSpace(request.IconName))
            {
                throw new InvalidRequestException($"Icon is required.");
            }
        }
        public static void Validate(this CategoryDeleteRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.CategoryId))
            {
                throw new InvalidRequestException($"Category is required.");
            }
        }

        public static void Validate(this CategoryUpdateRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Name))
            {
                throw new InvalidRequestException($"Name is required.");
            }
            if (string.IsNullOrWhiteSpace(request.IconName))
            {
                throw new InvalidRequestException($"Icon  is required.");
            }
        }
    }
}
