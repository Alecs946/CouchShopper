using CouchShopper.Data.DTOs.Request.Common.Category;
using CouchShopper.Data.DTOs.Response.Common.Category;
using CouchShopper.Data.DTOs.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CouchShopper.Business.Interfaces
{
    public interface ICategoryService
    {
        Task<CategoryResponse> GetCategory(string id);

        Task<CategoryListResponse> GetActiveCategories(int page);

        Task CreateCategory(CategoryAddRequest request);

        Task UpdateCategory(CategoryUpdateRequest request);

        Task DeleteCategory(CategoryDeleteRequest request);

        Task<List<ExtendedDropDownModel>> GetCategoriesWithIcons();

        Task<List<DropdownModel>> GetCategoriesDropdown();
    }
}
