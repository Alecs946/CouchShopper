using AutoMapper;
using CouchShopper.Business.Exceptions;
using CouchShopper.Business.Interfaces;
using CouchShopper.Data.Constants;
using CouchShopper.Data.DTOs.Request.Common.Cities;
using CouchShopper.Data.DTOs.Response.Common.City;
using CouchShopper.Data.DTOs.Response;
using CouchShopper.Data.Models;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using CouchShopper.Business.Validators;

namespace CouchShopper.Business.Services
{
    public class CityService : BaseService<Common>, ICityService
    {
        private readonly IMapper _mapper;

        public CityService(IConfiguration configuration, IHttpClientFactory clientFactory, IMapper mapper)
            : base(configuration, clientFactory)
        {
            _mapper = mapper;
        }

        #region Cities
        public async Task<CityResponse> GetCity(string id)
        {
            var commonCities = await GetByIdAsync("Cities");
            var city = commonCities.Cities.Where(x => x.Id == id && !x.Deleted).FirstOrDefault();
            if (city == null)
            {
                throw new InvalidRequestException($"City does not exist.");
            }
            return _mapper.Map<CityResponse>(city);
        }

        public async Task<CityListResponse> GetActiveCities(int page, string countryId)
        {
            page = page == 0 ? 1 : page;
            var activeCities = (await GetByIdAsync("Cities"))?.Cities?.OrderBy(x => x.Name)?.ToList()?.FindAll(x => !x.Deleted && x.CountryId.Equals(countryId));
            return activeCities != null ? new CityListResponse
            {
                TotalEntities = activeCities.Count,
                Offset = (page),
                Cities = _mapper.Map<List<CityResponse>>(activeCities.Skip((page - 1) * Numeric.ItemsPerPage).Take(Numeric.ItemsPerPage))
            }
            : new CityListResponse
            {
                TotalEntities = 0,
                Offset = (page),
                Cities = new List<CityResponse>()
            };
        }

        public async Task CreateCity(CityAddRequest request)
        {
            request.Validate();
            var cityToInsert = _mapper.Map<City>(request);
            var commonCities = await GetByIdAsync("Cities");
            if (commonCities == null)
            {
                if (!await InsertAsync(new Common() { Id = "Cities", Cities = new List<City>() }))
                {
                    throw new InvalidRequestException($"City could not be created");
                }
                commonCities = await GetByIdAsync("Cities");
            }
            var existingCity = commonCities != null ?
                                    commonCities.Cities.FirstOrDefault(x => x.Name.Equals(request.Name) && x.CountryId.Equals(request.CountryId) && !x.Deleted)
                                    : throw new InvalidRequestException($"City could not be created");
            if (existingCity != null)
            {
                throw new InvalidRequestException($"City aleardy exists");
            }
            commonCities.Cities.Add(cityToInsert);
            var response = await UpdateAsync(commonCities);
            if (!response)
            {
                throw new InvalidRequestException($"City could not be created");
            }
        }

        public async Task DeleteCity(CityDeleteRequest request)
        {
            request.Validate();
            var commonCities = await GetByIdAsync("Cities");
            var cityToDelete = commonCities?.Cities.FirstOrDefault(x => x.Id.Equals(request.CityId) && !x.Deleted);
            if (cityToDelete == null)
            {
                throw new InvalidRequestException($"City does not exist or has already been deleted.");
            }
            cityToDelete.Deleted = true;
            if (!await UpdateAsync(commonCities))
            {
                throw new InvalidRequestException($"City could not be deleted.");
            }
        }

        public async Task UpdateCity(CityUpdateRequest request)
        {
            request.Validate();
            var commonCities = await GetByIdAsync("Cities");
            var cityToUpdate = commonCities != null
                ? commonCities.Cities.FirstOrDefault(x => x.Id.Equals(request.Id) && !x.Deleted)
                : throw new InvalidRequestException($"City could not be updated");
            if (cityToUpdate == null)
            {
                throw new InvalidRequestException($"City does not exist.");
            }
            var existingCity = commonCities.Cities.FirstOrDefault(x => x.Name.Equals(request.Name) && !x.Id.Equals(request.Id) && !x.Deleted && x.CountryId.Equals(request.CountryId));
            if (existingCity != null)
            {
                throw new InvalidRequestException($"City aleardy exists");
            }
            cityToUpdate.Name = request.Name;
            if (!await UpdateAsync(commonCities))
            {
                throw new InvalidRequestException($"City could not be updated");
            }
        }

        public async Task<List<DropdownModel>> GetCitiesDropdown(string countryName)
        {
            var countries = await GetByIdAsync("Countries");
            var countryId = countries.Countries.Where(c => c.Name.Equals(countryName)).FirstOrDefault()?.Id;
            var commonCities = await GetByIdAsync("Cities");
            return commonCities != null && !string.IsNullOrEmpty(countryId)
                ? _mapper.Map<List<DropdownModel>>(commonCities.Cities.FindAll(x => !x.Deleted && x.CountryId == countryId))
                : new List<DropdownModel>();
        }
        #endregion
    }
}
