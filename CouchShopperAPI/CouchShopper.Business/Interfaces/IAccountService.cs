using CouchShopper.Data.DTOs.Request.Account;
using CouchShopper.Data.DTOs.Response.Account;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CouchShopper.Business.Interfaces
{
    public interface IAccountService
    {
        Task<AccountResponse> GetProfileDetails(string id);

        Task<string> GetUserCountry(string userId);

        Task UpdateProfileDetails(AccountUpdateRequest request);

        Task UpdatePassword(AccountPasswordUpdateRequest request);

        Task<AccountProfilePictureResponse> GetProfilePicture(string username);

        Task UploadProfilePicture(AccountProfilePictureUploadRequest request);

        Task<List<AccountPaymentMethodResponse>> GetPaymentMethods(string username);

        Task CreateCard(AccountAddPaymentCreditCardRequest request);

        Task CreatePaypal(AccountAddPayPal request);

        Task UpdatePrimaryPayment(AccountPaymentMethodUpdateRequest request);

        Task DeletePaymentMethod(AccountPaymentMethodDeleteRequest request);

    }
}
