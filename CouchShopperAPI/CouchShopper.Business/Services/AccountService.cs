using AutoMapper;
using CouchShopper.Business.Exceptions;
using CouchShopper.Business.Helpers;
using CouchShopper.Business.Interfaces;
using CouchShopper.Business.Validators;
using CouchShopper.Data.DTOs.Request.Account;
using CouchShopper.Data.DTOs.Response.Account;
using CouchShopper.Data.DTOs.Response.Users;
using CouchShopper.Data.Models;
using Microsoft.AspNet.Identity;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CouchShopper.Business.Services
{
    public class AccountService : BaseService<Users>, IAccountService
    {
        private readonly IMapper _mapper;
        public AccountService(IConfiguration configuration, IHttpClientFactory clientFactory, IMapper mapper)
            : base(configuration, clientFactory)
        {
            _mapper = mapper;
        }

        public async Task<AccountResponse> GetProfileDetails(string id)
        {
            var user = await GetByIdAsync(id);
            if (user == null)
            {
                throw new InvalidRequestException($"User does not exist.");
            }

            return _mapper.Map<AccountResponse>(user);
        }

        public async Task<string> GetUserCountry(string userId)
        {
            var user = await GetByIdAsync(userId);

            return user?.Country ?? null;
        }

        public async Task UpdateProfileDetails(AccountUpdateRequest request)
        {
            request.Validate();
            var userToUpdate = await GetByIdAsync(request.Username);
            if (userToUpdate == null)
            {
                throw new InvalidRequestException($"Your informaions could not be updated");
            }
            userToUpdate.FullName = request.FullName;
            userToUpdate.Email = request.Email;
            userToUpdate.Country = request.Country;
            userToUpdate.City = request.City;
            userToUpdate.Address = request.Address;
            userToUpdate.ZipCode = request.ZipCode;
            userToUpdate.Phone = request.Phone;
            await UpdateAsync(userToUpdate);
        }

        public async Task UpdatePassword(AccountPasswordUpdateRequest request)
        {
            request.Validate();
            var accountToUpdate = await GetByIdAsync(request.Username);
            if (accountToUpdate == null)
            {
                throw new InvalidRequestException($"Your informaions could not be updated");
            }
            if (!request.CurrentPassword.CheckPassword(accountToUpdate.PasswordSalt, accountToUpdate.PasswordHash))
            {
                throw new InvalidRequestException($"Current password does not match");
            }
            using var hmac = new HMACSHA512();
            accountToUpdate.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(request.NewPassword));
            accountToUpdate.PasswordSalt = hmac.Key;
            await UpdateAsync(accountToUpdate);
        }

        public async Task<AccountProfilePictureResponse> GetProfilePicture(string username)
        {

            var imageBytes = await GetAttachemntContent(username, "profilePicture");
            return new AccountProfilePictureResponse
            {
                ProfilePicture = imageBytes != null ? Convert.ToBase64String(imageBytes) : null
            };

        }

        public async Task UploadProfilePicture(AccountProfilePictureUploadRequest request)
        {
            request.Validate();
            if (request.ProfilePicture == null || request.ProfilePicture.Length == 0)
            {
                throw new Exception("No files were uploaded.");
            }
            var user = await GetByIdAsync(request.Username);
            var fileBytes = Convert.FromBase64String(request.ProfilePicture);
            await InsertAttachment(request.Username, "profilePicture", fileBytes, user.Rev);
        }

        public async Task<List<AccountPaymentMethodResponse>> GetPaymentMethods(string username)
        {
            var user = await GetByIdAsync(username);
            if (user == null)
            {
                throw new InvalidRequestException("User could not be found");
            }
            var paymentMethods = user.PaymentMethods;

            return _mapper.Map<List<AccountPaymentMethodResponse>>(paymentMethods); ;
        }

        public async Task CreateCard(AccountAddPaymentCreditCardRequest request)
        {
            request.Validate();
            var user = await GetByIdAsync(request.Username);
            if (user == null)
            {
                throw new InvalidRequestException("User could not be found");
            }
            var card = _mapper.Map<PaymentMethod>(request);
            if (user.PaymentMethods == null)
            {
                user.PaymentMethods = new List<PaymentMethod>();
                card.IsPrimary = true;
            }
            if (request.IsPrimary)
            {
                user.PaymentMethods.AsParallel().ForAll(x => { x.IsPrimary = false; });
            }

            user.PaymentMethods.Add(card);
            await UpdateAsync(user);
        }

        public async Task CreatePaypal(AccountAddPayPal request)
        {
            request.Validate();
            var user = await GetByIdAsync(request.Username);
            if (user == null)
            {
                throw new InvalidRequestException("User could not be found");
            }
            var paypal = _mapper.Map<PaymentMethod>(request);
            if (user.PaymentMethods == null)
            {
                user.PaymentMethods = new List<PaymentMethod>();
                paypal.IsPrimary = true;
            }
            if (request.IsPrimary)
            {
                user.PaymentMethods.AsParallel().ForAll(x => { x.IsPrimary = false; });
            }
            //Paypal ocnfirmation
            user.PaymentMethods.Add(paypal);
            await UpdateAsync(user);
        }

        public async Task UpdatePrimaryPayment(AccountPaymentMethodUpdateRequest request)
        {
            request.Validate();
            var user = await GetByIdAsync(request.UserId);
            user.PaymentMethods.ForEach(x => x.IsPrimary = false);
            var method = user.PaymentMethods.Where(x => x.Id == request.PaymentId).FirstOrDefault();
            method.IsPrimary = true;
            await UpdateAsync(user);
        }

        public async Task DeletePaymentMethod(AccountPaymentMethodDeleteRequest request)
        {
            request.Validate();
            var user = await GetByIdAsync(request.Username);
            if (user == null)
            {
                throw new InvalidRequestException("User could not be found");
            }
            user.PaymentMethods.RemoveAll(x => x.Id.Equals(request.CardId));
            await UpdateAsync(user);
        }
    }
}
