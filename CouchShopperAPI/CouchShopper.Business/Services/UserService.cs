using AutoMapper;
using AutoMapper.Execution;
using CouchShopper.Business.Exceptions;
using CouchShopper.Business.Helpers;
using CouchShopper.Business.Interfaces;
using CouchShopper.Business.Validators;
using CouchShopper.Data.Constants;
using CouchShopper.Data.DTOs.Request.Account;
using CouchShopper.Data.DTOs.Request.Users;
using CouchShopper.Data.DTOs.Response.Products;
using CouchShopper.Data.DTOs.Response.Users;
using CouchShopper.Data.enums;
using CouchShopper.Data.Models;
using Microsoft.AspNet.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.Serialization;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CouchShopper.Business.Services
{
    public class UserService : BaseService<Users>, IUserService
    {
        private readonly ITokenService _tokenService;
        private readonly IProductService _productService;
        private readonly IMapper _mapper;
        public UserService(ITokenService tokenService, IConfiguration configuration, IHttpClientFactory clientFactory, IMapper mapper, IProductService productService)
             : base(configuration, clientFactory)
        {
            _tokenService = tokenService;
            _productService = productService;
            _mapper = mapper;
        }

        public async Task<UserResponse> GetUser(string id)
        {
            var user = await GetByIdAsync(id);
            if (user == null)
            {
                throw new InvalidRequestException($"User does not exist.");
            }
            return _mapper.Map<UserResponse>(user);
        }

        public async Task<UserResponse> Create(UserAddRequest request)
        {
            request.Validate();
            using var hmac = new HMACSHA512();
            var user = _mapper.Map<Users>(request);
            user.UserName = request.UserName;
            user.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(request.Password));
            user.PasswordSalt = hmac.Key;
            var dbResult = await GetByIdAsync(user.Id);
            if (dbResult != null)
            {
                throw new InvalidRequestException($"Username already taken");
            }
            var response = await InsertAsync(user);
            if (!response)
            {
                throw new Exception($"User could not be created");
            }

            return _mapper.Map<UserResponse>(user);
        }

        public async Task<bool> IsActiveAccount(UserActivationCheckRequest request)
        {
            var user = await GetByIdAsync(request.Username);
            if (user == null)
            {
                throw new InvalidRequestException($"Username or password do not match");
            }
            if (!request.Password.CheckPassword(user.PasswordSalt, user.PasswordHash))
            {
                throw new InvalidRequestException($"Username or password do not match");
            }

            return user.Active;
        }

        public async Task<LoginResponse> Login(LoginRequest request)
        {
            request.Validate();
            var dbResult = await client.GetAsync($"{typeof(Users).Name.ToLower()}" +
                            $"/{Views.byUsername}?key=\"{request.UserName}\"&include_docs=true&limit=1");
            var response = dbResult.IsSuccessStatusCode
                ? JsonConvert.DeserializeObject<BaseViewEntity<Users>>(await dbResult.Content.ReadAsStringAsync())
                : throw new InvalidRequestException($"User could not be found");
            var user = (response != null && response.Rows.Any())
                ? (Users)response.Rows.FirstOrDefault().Doc
                : throw new InvalidRequestException($"User could not be found");

            return request.Password.CheckPassword(user.PasswordSalt, user.PasswordHash)
                ? new LoginResponse()
                {
                    Username = user.UserName,
                    Token = _tokenService.CreateToken(user),
                }
                : throw new InvalidRequestException($"Credentials do not match");
        }

        public async Task ActivateAccount(UserActivationRequest request)
        {
            request.Validate();
            var user = await GetByIdAsync(request.Username);
            if (!user.ActivationCode.Equals(request.Code))
            {
                throw new InvalidRequestException($"Provided code is incorrect");
            }
            user.Active = true;
            if (!await UpdateAsync(user))
            {
                throw new Exception("Something went wrong!");
            }
        }

    }
}
