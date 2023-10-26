using CouchShopper.Data.DTOs.Request.Account;
using CouchShopper.Data.DTOs.Request.Users;
using CouchShopper.Data.DTOs.Response.Users;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CouchShopper.Business.Interfaces
{
    public interface IUserService
    {
        Task<bool> IsActiveAccount(UserActivationCheckRequest request);

        Task<LoginResponse> Login(LoginRequest request);

        Task<UserResponse> Create(UserAddRequest request);

        Task<UserResponse> GetUser(string id);

        Task ActivateAccount(UserActivationRequest request);

    }
}
