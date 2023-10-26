using CouchShopper.Data.DTOs.Request.Seller;
using CouchShopper.Data.DTOs.Response.Seller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CouchShopper.Business.Interfaces
{
    public interface ISellerService
    {
        Task<SellerByRatingResponse> GetTopSeller();

        Task RateUser(SellerRatingRequest request);

        Task<SellerInfoResponse> GetSellerInfo(string userId);

        Task<SellerDetailsResponse> GetSellerDetails(string sellerId, int page);

        Task<SellerSalesInfoResponse> GetSalesInfo(string sellerId, int periodId);

        Task<double> GetBalance(string userId);

        Task<SellerPayoutsResponseList> GetPayouts(string userId, int page);

        Task AddRevenue(SellerRevenue request);

        Task Withdrawal(SellerWithdrawalRequest request);
    }
}
