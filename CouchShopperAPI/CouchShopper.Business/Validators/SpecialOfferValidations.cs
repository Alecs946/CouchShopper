using CouchShopper.Business.Exceptions;
using CouchShopper.Data.DTOs.Request.Common.SpecialOffer;

namespace CouchShopper.Business.Validators
{
    public static class SpecialOfferValidations
    {
        public static void Validate(this SpecialOfferAddRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Title))
            {
                throw new InvalidRequestException($"Title is required.");
            }
            if (string.IsNullOrWhiteSpace(request.ImageBase64))
            {
                throw new InvalidRequestException($"Image is required.");
            }
            if (string.IsNullOrWhiteSpace(request.BackgroundColor))
            {
                throw new InvalidRequestException($"Background Color is required.");
            }
            if (string.IsNullOrWhiteSpace(request.Description))
            {
                throw new InvalidRequestException($"Description is required.");
            }
            if (string.IsNullOrWhiteSpace(request.TextColor))
            {
                throw new InvalidRequestException($"Text Color is required.");
            }
        }

        public static void Validate(this SpecialOfferUpdateRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Id))
            {
                throw new InvalidRequestException($"Offer is required.");
            }
            if (string.IsNullOrWhiteSpace(request.Title))
            {
                throw new InvalidRequestException($"Title is required.");
            }
            if (string.IsNullOrWhiteSpace(request.ImageBase64))
            {
                throw new InvalidRequestException($"Image is required.");
            }
            if (string.IsNullOrWhiteSpace(request.BackgroundColor))
            {
                throw new InvalidRequestException($"Background Color is required.");
            }
            if (string.IsNullOrWhiteSpace(request.Description))
            {
                throw new InvalidRequestException($"Description is required.");
            }
            if (string.IsNullOrWhiteSpace(request.TextColor))
            {
                throw new InvalidRequestException($"Text Color is required.");
            }
        }
        public static void Validate(this SpecialOfferDeleteRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.SpecialOfferId))
            {
                throw new InvalidRequestException($"Offer is required.");
            }
            
        }
    }
}
