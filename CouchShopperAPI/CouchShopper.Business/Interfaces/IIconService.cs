using CouchShopper.Data.DTOs.Request.Common.Icon;
using CouchShopper.Data.DTOs.Response;
using CouchShopper.Data.DTOs.Response.Common.Icon;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CouchShopper.Business.Interfaces
{
    public interface IIconService
    {
        Task<IconResponse> GetIcon(string id);

        Task<IconListResponse> GetActiveIcons(int page);

        Task CreateIcon(IconAddRequest request);

        Task UpdateIcon(IconUpdateRequest request);

        Task DeleteIcon(IconDeleteRequest request);

        Task<List<DropdownModel>> GetIconsDropdown();

    }
}
