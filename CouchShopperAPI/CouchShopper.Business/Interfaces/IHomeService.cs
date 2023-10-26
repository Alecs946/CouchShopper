using System.Threading.Tasks;
using CouchShopper.Data.DTOs.Response.Common.Home;

namespace CouchShopper.Business.Interfaces
{
    public interface IHomeService
    {
        Task<HomePageResponse> GetHomePageContent();
    }
}
