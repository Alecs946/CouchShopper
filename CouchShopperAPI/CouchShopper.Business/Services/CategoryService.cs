using AutoMapper;
using CouchShopper.Business.Exceptions;
using CouchShopper.Business.Interfaces;
using CouchShopper.Data.Constants;
using CouchShopper.Data.DTOs.Request.Common.Category;
using CouchShopper.Data.DTOs.Response.Common.Category;
using CouchShopper.Data.DTOs.Response;
using CouchShopper.Data.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using CouchShopper.Business.Validators;

namespace CouchShopper.Business.Services
{
    public class CategoryService : BaseService<Common>, ICategoryService
    {
        private readonly IMapper _mapper;

        public CategoryService(IConfiguration configuration, IHttpClientFactory clientFactory, IMapper mapper)
            : base(configuration, clientFactory)
        {
            _mapper = mapper;
        }

        public async Task<CategoryResponse> GetCategory(string id)
        {
            var commonCategories = await GetByIdAsync("Categories");
            var category = commonCategories.Categories.Where(x => x.Id == id && !x.Deleted).FirstOrDefault();
            if (category == null)
            {
                throw new InvalidRequestException($"Category does not exist.");
            }
            return _mapper.Map<CategoryResponse>(category);
        }

        public async Task<CategoryListResponse> GetActiveCategories(int page)
        {
            page = page == 0 ? 1 : page;
            var activeCategories = (await GetByIdAsync("Categories")).Categories.FindAll(x => !x.Deleted);
            return activeCategories != null ? new CategoryListResponse
            {
                TotalEntities = activeCategories.Count,
                Offset = (page - 1) * Numeric.ItemsPerPage,
                Categories = _mapper.Map<List<CategoryResponse>>(activeCategories.Skip((page - 1) * Numeric.ItemsPerPage).Take(Numeric.ItemsPerPage))
            }
            : new CategoryListResponse
            {
                TotalEntities = 0,
                Offset = (page - 1) * Numeric.ItemsPerPage,
                Categories = new List<CategoryResponse>()
            };
        }

        public async Task CreateCategory(CategoryAddRequest request)
        {
            request.Validate();
            var categoryToInsert = _mapper.Map<Category>(request);
            var commonCategories = await GetByIdAsync("Categories");
            if (commonCategories == null)
            {
                if (!await InsertAsync(new Common() { Id = "Categories", Categories = new List<Category>() }))
                {
                    throw new InvalidRequestException($"Category could not be created");
                }
                commonCategories = await GetByIdAsync("Categories");
            }
            var existingCategory = commonCategories != null
                ? commonCategories.Categories.FirstOrDefault(x => x.Name.Equals(request.Name) && !x.Deleted)
                : throw new InvalidRequestException($"Country could not be created");
            if (existingCategory != null)
            {
                throw new InvalidRequestException($"Category aleardy exists");
            }
            commonCategories.Categories.Add(categoryToInsert);
            var response = await UpdateAsync(commonCategories);
            if (!response)
            {
                throw new InvalidRequestException($"Category could not be created");
            }
        }

        public async Task DeleteCategory(CategoryDeleteRequest request)
        {
            request.Validate();
            var commonCategories = await GetByIdAsync("Categories");
            var categoryToDelete = commonCategories?.Categories.FirstOrDefault(x => x.Id.Equals(request.CategoryId) && !x.Deleted);
            if (categoryToDelete == null)
            {
                throw new InvalidRequestException($"Category does not exist or has already been deleted.");
            }
            categoryToDelete.Deleted = true;
            if (!await UpdateAsync(commonCategories))
            {
                throw new InvalidRequestException($"Category could not be deleted.");
            }
        }

        public async Task UpdateCategory(CategoryUpdateRequest request)
        {
            request.Validate();
            var commonCategories = await GetByIdAsync("Categories");
            var categoryToUpdate = commonCategories != null
                ? commonCategories.Categories.FirstOrDefault(x => x.Id.Equals(request.Id) && !x.Deleted)
                : throw new InvalidRequestException($"Category could not be updated");
            if (categoryToUpdate == null)
            {
                throw new InvalidRequestException($"Category does not exist.");
            }
            var existingCategory = commonCategories.Categories.FirstOrDefault(x => x.Name.Equals(request.Name) && !x.Id.Equals(request.Id));
            if (existingCategory != null)
            {
                throw new InvalidRequestException($"Category aleardy exists");
            }
            categoryToUpdate.Name = request.Name;
            categoryToUpdate.IconName = request.IconName;
            if (!await UpdateAsync(commonCategories))
            {
                throw new InvalidRequestException($"Category could not be updated");
            }
        }

        public async Task<List<ExtendedDropDownModel>> GetCategoriesWithIcons()
        {
            var commonCategories = await GetByIdAsync("Categories");
            return commonCategories != null
                ? _mapper.Map<List<ExtendedDropDownModel>>(commonCategories.Categories.FindAll(x => !x.Deleted))
                : new List<ExtendedDropDownModel>();
        }

        public async Task<List<DropdownModel>> GetCategoriesDropdown()
        {
            var commonCategories = await GetByIdAsync("Categories");
            return commonCategories != null
                ? _mapper.Map<List<DropdownModel>>(commonCategories.Categories.FindAll(x => !x.Deleted))
                : new List<DropdownModel>();
        }
    }
}
