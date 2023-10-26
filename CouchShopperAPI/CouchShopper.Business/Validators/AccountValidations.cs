using CouchShopper.Business.Exceptions;
using CouchShopper.Data.DTOs.Request.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CouchShopper.Business.Validators
{
    public static class AccountValidations
    {
        public static void Validate(this AccountUpdateRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Username))
            {
                throw new InvalidRequestException($"Username is required.");
            }
            if (string.IsNullOrWhiteSpace(request.FullName))
            {
                throw new InvalidRequestException($"Name is required.");
            }
            if (string.IsNullOrWhiteSpace(request.Email))
            {
                throw new InvalidRequestException($"Email is required.");
            }
            if (string.IsNullOrWhiteSpace(request.Country))
            {
                throw new InvalidRequestException($"Country is required.");
            }
            if (string.IsNullOrWhiteSpace(request.City))
            {
                throw new InvalidRequestException($"City is required.");
            }
            if (string.IsNullOrWhiteSpace(request.Address))
            {
                throw new InvalidRequestException($"Address is required.");
            }
            if (string.IsNullOrWhiteSpace(request.ZipCode))
            {
                throw new InvalidRequestException($"ZIP Code is required.");
            }
        }
        
        public static void Validate(this AccountPasswordUpdateRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Username))
            {
                throw new InvalidRequestException($"Username is required.");
            }
            if (string.IsNullOrWhiteSpace(request.CurrentPassword))
            {
                throw new InvalidRequestException($"Current Password is required.");
            }
            if (string.IsNullOrWhiteSpace(request.NewPassword))
            {
                throw new InvalidRequestException($"New Password is required.");
            }
        }
        
        public static void Validate(this AccountProfilePictureUploadRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Username))
            {
                throw new InvalidRequestException($"Username is required.");
            }
            if (string.IsNullOrWhiteSpace(request.ProfilePicture))
            {
                throw new InvalidRequestException($"Picture is required.");
            }
        }
        
        public static void Validate(this AccountAddPaymentCreditCardRequest request)
        {
            if (request.CardName ==0)
            {
                throw new InvalidRequestException($"Card Name is required.");
            }
            if (string.IsNullOrWhiteSpace(request.Username))
            {
                throw new InvalidRequestException($"Username is required.");
            }
            if (string.IsNullOrWhiteSpace(request.CardNumber))
            {
                throw new InvalidRequestException($"Card Number is required.");
            }
            if (string.IsNullOrWhiteSpace(request.NameOnCard))
            {
                throw new InvalidRequestException($"Name On Card is required.");
            }
            if (string.IsNullOrWhiteSpace(request.ExpiryDate))
            {
                throw new InvalidRequestException($"Card Expiry Date is required.");
            }

        }
        public static void Validate(this AccountAddPayPal request)
        {
            if (string.IsNullOrWhiteSpace(request.Username))
            {
                throw new InvalidRequestException($"Username is required.");
            }
            if (string.IsNullOrWhiteSpace(request.Email))
            {
                throw new InvalidRequestException($"Email is required.");
            }
            if (string.IsNullOrWhiteSpace(request.Password))
            {
                throw new InvalidRequestException($"Password is required.");
            }
        }
        
        public static void Validate(this AccountPaymentMethodUpdateRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.UserId))
            {
                throw new InvalidRequestException($"Username is required.");
            }
            if (string.IsNullOrWhiteSpace(request.PaymentId))
            {
                throw new InvalidRequestException($"Payment Method is required.");
            }
        }
        public static void Validate(this AccountPaymentMethodDeleteRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Username))
            {
                throw new InvalidRequestException($"Username is required.");
            }
            if (string.IsNullOrWhiteSpace(request.CardId))
            {
                throw new InvalidRequestException($"Payment Method is required.");
            }
        }

    }
}
