using AutoMapper;
using CouchShopper.Business.Exceptions;
using CouchShopper.Business.Interfaces;
using CouchShopper.Data.Constants;
using CouchShopper.Data.Models;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using CouchShopper.Data.DTOs.Response;
using CouchShopper.Business.Validators;
using CouchShopper.Data.DTOs.Response.Common.Icon;
using CouchShopper.Data.DTOs.Request.Common.Icon;

namespace CouchShopper.Business.Services
{
    public class IconService : BaseService<Common>, IIconService
    {
        private readonly IMapper _mapper;
        private readonly IAccountService _accountService;
        
        public IconService(IConfiguration configuration, IHttpClientFactory clientFactory, IMapper mapper, IAccountService accountService)
            : base(configuration, clientFactory)
        {
            _mapper = mapper;
            _accountService = accountService;
        }
        
        public async Task<IconResponse> GetIcon(string id)
        {
            var commonIcons = await GetByIdAsync("Icons");
            var icon = commonIcons.Icons.Where(x => x.Id == id && !x.Deleted).FirstOrDefault();
            if (icon == null)
            {
                throw new InvalidRequestException($"Icon does not exist.");
            }
            return _mapper.Map<IconResponse>(icon);
        }

        public async Task<IconListResponse> GetActiveIcons(int page)
        {
            page = page == 0 ? 1 : page;
            var activeIcons = (await GetByIdAsync("Icons")).Icons.OrderBy(x => x.Name).ToList().FindAll(x => !x.Deleted);
            return activeIcons != null ? new IconListResponse
            {
                TotalEntities = activeIcons.Count,
                Offset = (page),
                Icons = _mapper.Map<List<IconResponse>>(activeIcons.Skip((page - 1) * Numeric.ItemsPerPage).Take(Numeric.ItemsPerPage))
            }
            : new IconListResponse
            {
                TotalEntities = 0,
                Offset = (page),
                Icons = new List<IconResponse>()
            };
        }

        public async Task CreateIcon(IconAddRequest request)
        {
            request.Validate();
            var iconToInsert = _mapper.Map<Icon>(request);
            var commonIcons = await GetByIdAsync("Icons");
            if (commonIcons == null)
            {
                if (!await InsertAsync(new Common() { Id = "Icons", Icons = new List<Icon>() }))
                {
                    throw new InvalidRequestException($"Icon could not be created");
                }
                commonIcons = await GetByIdAsync("Icons");
            }
            var existingIcon = commonIcons != null ?
                                    commonIcons.Icons.FirstOrDefault(x => x.Name.Equals(request.Name) && !x.Deleted)
                                    : throw new InvalidRequestException($"Icon could not be created");
            if (existingIcon != null)
            {
                throw new InvalidRequestException($"Icon aleardy exists");
            }
            commonIcons.Icons.Add(iconToInsert);
            var response = await UpdateAsync(commonIcons);
            if (!response)
            {
                throw new InvalidRequestException($"Icon could not be created");
            }
        }

        public async Task DeleteIcon(IconDeleteRequest request)
        {
            request.Validate();
            var commonIcons = await GetByIdAsync("Icons");
            var commonCities = await GetByIdAsync("Cities");
            var iconToDelete = commonIcons?.Icons.FirstOrDefault(x => x.Id.Equals(request.IconId) && !x.Deleted);
            if (iconToDelete == null)
            {
                throw new InvalidRequestException($"Icon does not exist or has already been deleted.");
            }
            iconToDelete.Deleted = true;
            if (!await UpdateAsync(commonIcons))
            {
                throw new InvalidRequestException($"Icon could not be deleted.");
            }
        }

        public async Task UpdateIcon(IconUpdateRequest request)
        {
            request.Validate();
            var commonIcons = await GetByIdAsync("Icons");
            var iconToUpdate = commonIcons != null
                ? commonIcons.Icons.FirstOrDefault(x => x.Id.Equals(request.Id) && !x.Deleted)
                : throw new InvalidRequestException($"Icon could not be updated");
            if (iconToUpdate == null)
            {
                throw new InvalidRequestException($"Icon does not exist.");
            }
            var existingIcon = commonIcons.Icons.FirstOrDefault(x => x.Name.Equals(request.Name) && !x.Id.Equals(request.Id));
            if (existingIcon != null)
            {
                throw new InvalidRequestException($"Icon aleardy exists");
            }
            iconToUpdate.Name = request.Name;
            if (!await UpdateAsync(commonIcons))
            {
                throw new InvalidRequestException($"Icon could not be updated");
            }
        }

        public async Task<List<DropdownModel>> GetIconsDropdown()
        {
            var commonIcons = await GetByIdAsync("Icons");
            return commonIcons != null
                ? _mapper.Map<List<DropdownModel>>(commonIcons.Icons.FindAll(x => !x.Deleted))
                : new List<DropdownModel>();
        }
    }
}
