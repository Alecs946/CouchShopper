using CouchShopper.Business.Exceptions;
using CouchShopper.Data.DTOs.Request.Account;
using CouchShopper.Data.DTOs.Request.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CouchShopper.Business.Validators
{
    public static class UserValidations
    {
        public static void Validate(this UserAddRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.FullName))
            {
                throw new InvalidRequestException($"Name is required.");
            }
            if (string.IsNullOrWhiteSpace(request.UserName))
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

        public static void Validate(this LoginRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.UserName))
            {
                throw new InvalidRequestException($"Username is required.");
            }
            if (string.IsNullOrWhiteSpace(request.Password))
            {
                throw new InvalidRequestException($"Password is required.");
            }
        }

        public static void Validate(this UserActivationRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Username))
            {
                throw new InvalidRequestException($"Username is required.");
            }
            if (string.IsNullOrWhiteSpace(request.Code))
            {
                throw new InvalidRequestException($"Code is required.");
            }
        }
    }
}
