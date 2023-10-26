using CouchShopper.Business.Exceptions;
using CouchShopper.Business.Interfaces;
using CouchShopper.Data.Constants;
using CouchShopper.Data.DTOs.Request.Common.Country;
using CouchShopper.Data.DTOs.Response.Common.Country;
using CouchShopper.Data.DTOs.Response;
using CouchShopper.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using System.Net.Http;
using CouchShopper.Business.Validators;

namespace CouchShopper.Business.Services
{
    public class CountryService : BaseService<Common>, ICountryService
    {
        private readonly IMapper _mapper;
        private readonly IAccountService _accountService;

        public CountryService(IConfiguration configuration, IHttpClientFactory clientFactory, IMapper mapper, IAccountService accountService)
            : base(configuration, clientFactory)
        {
            _mapper = mapper;
            _accountService = accountService;
        }
        public async Task<CountryResponse> GetCountry(string id)
        {
            var commonCountries = await GetByIdAsync("Countries");
            var country = commonCountries.Countries.Where(x => x.Id == id && !x.Deleted).FirstOrDefault();
            if (country == null)
            {
                throw new InvalidRequestException($"Country does not exist.");
            }
            return _mapper.Map<CountryResponse>(country);
        }

        public async Task<CountryListResponse> GetActiveCountries(int page)
        {
            page = page == 0 ? 1 : page;
            var activeCountries = (await GetByIdAsync("Countries")).Countries.OrderBy(x => x.Name).ToList().FindAll(x => !x.Deleted);
            return activeCountries != null ? new CountryListResponse
            {
                TotalEntities = activeCountries.Count,
                Offset = (page),
                Countries = _mapper.Map<List<CountryResponse>>(activeCountries.Skip((page - 1) * Numeric.ItemsPerPage).Take(Numeric.ItemsPerPage))
            }
            : new CountryListResponse
            {
                TotalEntities = 0,
                Offset = (page),
                Countries = new List<CountryResponse>()
            };
        }

        public async Task CreateCountry(CountryAddRequest request)
        {
            request.Validate();
            var countryToInsert = _mapper.Map<Country>(request);
            var commonCountries = await GetByIdAsync("Countries");
            if (commonCountries == null)
            {
                if (!await InsertAsync(new Common() { Id = "Countries", Countries = new List<Country>() }))
                {
                    throw new InvalidRequestException($"Country could not be created");
                }
                commonCountries = await GetByIdAsync("Countries");
            }
            var existingCountry = commonCountries != null ?
                                    commonCountries.Countries.FirstOrDefault(x => x.Name.Equals(request.Name) && !x.Deleted)
                                    : throw new InvalidRequestException($"Country could not be created");
            if (existingCountry != null)
            {
                throw new InvalidRequestException($"Country aleardy exists");
            }
            commonCountries.Countries.Add(countryToInsert);
            var response = await UpdateAsync(commonCountries);
            if (!response)
            {
                throw new InvalidRequestException($"Country could not be created");
            }
        }

        public async Task DeleteCountry(CountryDeleteRequest request)
        {
            request.Validate();
            var commonCountries = await GetByIdAsync("Countries");
            var commonCities = await GetByIdAsync("Cities");
            var countryToDelete = commonCountries?.Countries.FirstOrDefault(x => x.Id.Equals(request.CountryId) && !x.Deleted);
            if (countryToDelete == null)
            {
                throw new InvalidRequestException($"Country does not exist or has already been deleted.");
            }
            countryToDelete.Deleted = true;
            if (!await UpdateAsync(commonCountries))
            {
                throw new InvalidRequestException($"Country could not be deleted.");
            }
            if (commonCities != null)
            {
                var citiesToDelete = commonCities.Cities.FindAll(x => x.CountryId.Equals(countryToDelete.Id));
                if (citiesToDelete != null && citiesToDelete.Any())
                {
                    citiesToDelete.AsParallel().ForAll(x => x.Deleted = true);
                    await UpdateAsync(commonCities);
                }
            }
        }

        public async Task UpdateCountry(CountryUpdateRequest request)
        {
            request.Validate();
            var commonCountries = await GetByIdAsync("Countries");
            var countryToUpdate = commonCountries != null
                ? commonCountries.Countries.FirstOrDefault(x => x.Id.Equals(request.Id) && !x.Deleted)
                : throw new InvalidRequestException($"Country could not be updated");
            if (countryToUpdate == null)
            {
                throw new InvalidRequestException($"Country does not exist.");
            }
            var existingCountry = commonCountries.Countries.FirstOrDefault(x => x.Name.Equals(request.Name) && !x.Id.Equals(request.Id));
            if (existingCountry != null)
            {
                throw new InvalidRequestException($"Country aleardy exists");
            }
            countryToUpdate.Name = request.Name;
            countryToUpdate.DestinationCharge = request.DestinationCharge;
            countryToUpdate.SaverDestinationCharge= request.SaverDestinationCharge;
            countryToUpdate.BaseNumberOfDays= request.BaseNumberOfDays;
            if (!await UpdateAsync(commonCountries))
            {
                throw new InvalidRequestException($"Country could not be updated");
            }
        }

        public async Task<List<DropdownModel>> GetCountriesDropdown()
        {
            var commonCountries = await GetByIdAsync("Countries");
            return commonCountries != null
                ? _mapper.Map<List<DropdownModel>>(commonCountries.Countries.FindAll(x => !x.Deleted))
                : new List<DropdownModel>();
        }

        public async Task<List<ShippingOptionsResponse>> GetShippingOptions(string userName)
        {

            var countryName = await _accountService.GetUserCountry(userName);
            if (!string.IsNullOrEmpty(countryName))
            {
                var shippingOptions = new List<ShippingOptionsResponse>();
                var county = (await GetByIdAsync("Countries")).Countries.Where(x => x.Name.Equals(countryName)).FirstOrDefault();
                shippingOptions.Add(new ShippingOptionsResponse()
                {
                    ShippingMethodName = "Premium Shipping",
                    MinNumberOfDays = county.BaseNumberOfDays - 2,
                    MaxNumberOfDays = county.BaseNumberOfDays + 2,
                    ShippingPrice = county.DestinationCharge

                });
                shippingOptions.Add(new ShippingOptionsResponse()
                {
                    ShippingMethodName = "Saver Shipping",
                    MinNumberOfDays = county.BaseNumberOfDays + 10,
                    MaxNumberOfDays = county.BaseNumberOfDays + 20,
                    ShippingPrice = county.SaverDestinationCharge
                });
                shippingOptions.Add(new ShippingOptionsResponse()
                {
                    ShippingMethodName = "Free Shipping",
                    MinNumberOfDays = county.BaseNumberOfDays + 30,
                    MaxNumberOfDays = county.BaseNumberOfDays + 45,
                    ShippingPrice = 0
                });
                return shippingOptions;
            }
            return null;
        }

        public async Task<List<ShippingOptionsResponse>> GetCountryShippingOptions(string country)
        {
            var shippingOptions = new List<ShippingOptionsResponse>();
            var countryInfo = (await GetByIdAsync("Countries")).Countries.Where(x => x.Name.Equals(country)).FirstOrDefault();
            if (countryInfo != null)
            {
                shippingOptions.Add(new ShippingOptionsResponse()
                {
                    ShippingMethodName = "Free Shipping",
                    MinNumberOfDays = countryInfo.BaseNumberOfDays + 30,
                    MaxNumberOfDays = countryInfo.BaseNumberOfDays + 45,
                    ShippingPrice = 0
                });
                shippingOptions.Add(new ShippingOptionsResponse()
                {
                    ShippingMethodName = "Saver Shipping",
                    MinNumberOfDays = countryInfo.BaseNumberOfDays + 10,
                    MaxNumberOfDays = countryInfo.BaseNumberOfDays + 20,
                    ShippingPrice = Math.Round(countryInfo.SaverDestinationCharge, 2)
                });
                shippingOptions.Add(new ShippingOptionsResponse()
                {
                    ShippingMethodName = "Premium Shipping",
                    MinNumberOfDays = countryInfo.BaseNumberOfDays - 2,
                    MaxNumberOfDays = countryInfo.BaseNumberOfDays + 2,
                    ShippingPrice = Math.Round(countryInfo.DestinationCharge, 2)

                });


                return shippingOptions;
            }
            return new List<ShippingOptionsResponse>();
        }
    }
}
