using AutoMapper;
using CouchShopper.Data.DTOs.Request.Account;
using CouchShopper.Data.DTOs.Request.Common.Category;
using CouchShopper.Data.DTOs.Request.Common.Cities;
using CouchShopper.Data.DTOs.Request.Common.Country;
using CouchShopper.Data.DTOs.Request.Common.Icon;
using CouchShopper.Data.DTOs.Request.Common.SpecialOffer;
using CouchShopper.Data.DTOs.Request.Orders;
using CouchShopper.Data.DTOs.Request.Products;
using CouchShopper.Data.DTOs.Request.Users;
using CouchShopper.Data.DTOs.Response;
using CouchShopper.Data.DTOs.Response.Account;
using CouchShopper.Data.DTOs.Response.Common.Category;
using CouchShopper.Data.DTOs.Response.Common.City;
using CouchShopper.Data.DTOs.Response.Common.Country;
using CouchShopper.Data.DTOs.Response.Common.Icon;
using CouchShopper.Data.DTOs.Response.Common.SpecialOffer;
using CouchShopper.Data.DTOs.Response.Order;
using CouchShopper.Data.DTOs.Response.Products;
using CouchShopper.Data.DTOs.Response.Seller;
using CouchShopper.Data.DTOs.Response.Users;
using CouchShopper.Data.enums;
using CouchShopper.Data.Enums;
using CouchShopper.Data.Extensions;
using CouchShopper.Data.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CouchShopper.Business
{
    public class Mappers : Profile
    {
        public Mappers()
        {
            #region Users

            CreateMap<UserAddRequest, Users>()
                   .ForMember(x => x.Id, opt => opt.MapFrom(src => src.UserName))
                   .ForMember(x => x.ActivationCode, opt => opt.MapFrom(src => Guid.NewGuid().ToString().Substring(0, 8)))
                   .ForMember(x => x.Role, opt => opt.MapFrom(src => src.IsSeller ? (int)Roles.Seller : (int)Roles.Buyer))
                   .ForMember(x => x.CreatedOn, opt => opt.MapFrom(src => DateTime.Now));

            CreateMap<Users, UserResponse>();
            CreateMap<AccountAddPaymentCreditCardRequest, PaymentMethod>()
                .ForMember(x => x.Id, opt => opt.MapFrom(src => Guid.NewGuid().ToString()))
                .ForMember(x => x.CardName, opt => opt.MapFrom(src => ((CardName)src.CardName).GetStringValue()))
                .ForMember(x => x.PaymentMethodType, opt => opt.MapFrom(src => (int)PaymentMethods.Card));
            CreateMap<AccountAddPayPal, PaymentMethod>()
                .ForMember(x => x.Id, opt => opt.MapFrom(src => Guid.NewGuid().ToString()))
                .ForMember(x => x.CardName, opt => opt.MapFrom(src => "Paypal"))
                .ForMember(x => x.PaymentMethodType, opt => opt.MapFrom(src => (int)PaymentMethods.Paypal));

            CreateMap<PaymentMethod, AccountPaymentMethodResponse>()
                .ForMember(x => x.NameOnCard, opt => opt.MapFrom(src => src.PaymentMethodType == (int)PaymentMethods.Paypal ? src.Email : src.NameOnCard))
                .ForMember(x => x.CardNumber, opt => opt.MapFrom(src => src.CardNumber.Substring(7, 4).PadLeft(12, '*')))
                .ForMember(x => x.PaymentMethodType, opt => opt.MapFrom(src => ((PaymentMethods)src.PaymentMethodType).GetStringValue()))
                .ForMember(x => x.IsCard, opt => opt.MapFrom(src => src.PaymentMethodType == (int)PaymentMethods.Card));
            CreateMap<ProductRangeResponse, UserFavoritesResponse>()
                .ForMember(x => x.IsAvailable, opt => opt.MapFrom(src => src.Quantity > 0));
            CreateMap<UserPayout, SellerPayoutResponse>()
                .ForMember(x => x.OnDate, opt => opt.MapFrom(src => src.OnDate.ToString("dd-MMM-yyyy")));
            #endregion
            #region Account
            CreateMap<Users, AccountResponse>();
            //CreateMap<PaymentMethod, AccountPaymentMethodResponse>();
            #endregion

            #region Categories

            CreateMap<Category, CategoryResponse>();

            CreateMap<CategoryAddRequest, Category>()
                   .ForMember(x => x.Id, opt => opt.MapFrom(src => Guid.NewGuid().ToString()));

            CreateMap<Category, DropdownModel>()
                .ForMember(x => x.Key, opt => opt.MapFrom(src => src.Name))
                .ForMember(x => x.Value, opt => opt.MapFrom(src => src.Name));
            CreateMap<Category, ExtendedDropDownModel>()
                .ForMember(x => x.Key, opt => opt.MapFrom(src => src.Id))
                .ForMember(x => x.Value, opt => opt.MapFrom(src => src.Name))
                .ForMember(x => x.IconName, opt => opt.MapFrom(src => src.IconName));


            CreateMap<BaseViewEntity<Category>, CategoryListResponse>()
                .ForMember(x => x.TotalEntities, opt => opt.MapFrom(src => src.TotalEntities))
                .ForMember(x => x.Offset, opt => opt.MapFrom(src => src.Offset))
                .ForMember(x => x.Categories, opt => opt.MapFrom(src => src.Rows.Select(x => x.Doc)));

            #endregion

            #region Countries

            CreateMap<Country, CountryResponse>();

            CreateMap<CountryAddRequest, Country>()
                   .ForMember(x => x.Id, opt => opt.MapFrom(src => Guid.NewGuid().ToString()));

            CreateMap<Country, DropdownModel>()
                .ForMember(x => x.Key, opt => opt.MapFrom(src => src.Name))
                .ForMember(x => x.Value, opt => opt.MapFrom(src => src.Name));


            CreateMap<BaseViewEntity<Country>, CountryListResponse>()
                .ForMember(x => x.TotalEntities, opt => opt.MapFrom(src => src.TotalEntities))
                .ForMember(x => x.Offset, opt => opt.MapFrom(src => src.Offset))
                .ForMember(x => x.Countries, opt => opt.MapFrom(src => src.Rows.Select(x => x.Doc)));

            #endregion

            #region City

            CreateMap<City, CityResponse>();

            CreateMap<CityAddRequest, City>()
                   .ForMember(x => x.Id, opt => opt.MapFrom(src => Guid.NewGuid().ToString()));

            CreateMap<City, DropdownModel>()
                .ForMember(x => x.Key, opt => opt.MapFrom(src => src.Name))
                .ForMember(x => x.Value, opt => opt.MapFrom(src => src.Name));


            CreateMap<BaseViewEntity<City>, CityListResponse>()
                .ForMember(x => x.TotalEntities, opt => opt.MapFrom(src => src.TotalEntities))
                .ForMember(x => x.Offset, opt => opt.MapFrom(src => src.Offset))
                .ForMember(x => x.Cities, opt => opt.MapFrom(src => src.Rows.Select(x => x.Doc)));

            #endregion

            #region Icon

            CreateMap<Icon, IconResponse>();

            CreateMap<IconAddRequest, Icon>()
                   .ForMember(x => x.Id, opt => opt.MapFrom(src => Guid.NewGuid().ToString()));

            CreateMap<Icon, DropdownModel>()
                .ForMember(x => x.Key, opt => opt.MapFrom(src => src.Name))
                .ForMember(x => x.Value, opt => opt.MapFrom(src => src.Name));


            CreateMap<BaseViewEntity<Icon>, IconListResponse>()
                .ForMember(x => x.TotalEntities, opt => opt.MapFrom(src => src.TotalEntities))
                .ForMember(x => x.Offset, opt => opt.MapFrom(src => src.Offset))
                .ForMember(x => x.Icons, opt => opt.MapFrom(src => src.Rows.Select(x => x.Doc)));

            #endregion

            #region Home
            #region Special Offer
            CreateMap<SpecialOfferAddRequest, SpecialOffer>()
                .ForMember(x => x.Id, opt => opt.MapFrom(src => Guid.NewGuid().ToString()))
                .ForMember(x => x.CreatedOn, opt => opt.MapFrom(src => DateTime.UtcNow));
            CreateMap<SpecialOfferAddRequest, SpecialOfferPhoto>()
                .ForMember(x => x.Id, opt => opt.MapFrom(src => Guid.NewGuid().ToString()))
                .ForMember(x => x.Content, opt => opt.MapFrom(src => Convert.FromBase64String(src.ImageBase64)));
            CreateMap<SpecialOffer, SpecialOfferResponse>();


            #endregion
            #endregion

            #region Products
            CreateMap<ProductOptionRequest, ProductOptions>();
            CreateMap<ProductAddRequest, Products>()
                .ForMember(x => x.Id, opt => opt.MapFrom(src => Guid.NewGuid().ToString()))
                .ForMember(x => x.CreatedOn, opt => opt.MapFrom(src => DateTime.UtcNow))
                .ForMember(x => x.Options, opt => opt.MapFrom(src => src.Options.Select(x => x.Option).ToList()));
            CreateMap<Products, ProductResponse>()
                .ForMember(x => x.Rating, opt => opt.MapFrom(src => src.Reviews.Select(x => x.Rating).Sum() / src.Reviews.Count))
                .ForMember(x => x.NumberOfReviews, opt => opt.MapFrom(src => src.Reviews.Count))
                .ForMember(x => x.Photos, opt => opt.MapFrom(src => new List<ProductPhotoResponse>()));
            CreateMap<Products, ProductAddedResponse>();
            CreateMap<ProductPhotoRequest, ProductPhotos>()
                .ForMember(x => x.Id, opt => opt.MapFrom(src => Guid.NewGuid().ToString()))
                .ForMember(x => x.Content, opt => opt.MapFrom(src => Convert.FromBase64String(src.Content)));
            CreateMap<ProductOptions, ProductOptionsResponse>();
            CreateMap<ProductOptionsRequest, ProductOptions>()
                .ForMember(x => x.ProductOptionName, opt => opt.MapFrom(src => src.Option.ProductOptionName))
                .ForMember(x => x.ProductOptionValues, opt => opt.MapFrom(src => src.Option.ProductOptionValues));
            CreateMap<Products, ProductsByUserResponse>()
                .ForMember(x => x.Photo, opt => opt.MapFrom(src => new ProductPhotoResponse()))
                .ForMember(x => x.Price, opt => opt.MapFrom(src => (src.SalePrice != null && src.SalePrice > 0) ? src.SalePrice : src.Price))
                .ForMember(x => x.Rating, opt => opt.MapFrom(src => src.Reviews.Select(x => x.Rating).Sum() / src.Reviews.Count))
                .ForMember(x => x.PhotoId, opt => opt.MapFrom(src => src.DefaultPhotoId ?? src._attachments.Keys.FirstOrDefault()));
            CreateMap<Products, ProductSearchResponse>()
                .ForMember(x => x.Photo, opt => opt.MapFrom(src => new ProductPhotoResponse()))
                .ForMember(x => x.Price, opt => opt.MapFrom(src => (src.SalePrice != null && src.SalePrice > 0) ? src.SalePrice : src.Price))
                .ForMember(x => x.Rating, opt => opt.MapFrom(src => src.Reviews.Select(x => x.Rating).Sum() / src.Reviews.Count))
                .ForMember(x => x.PhotoId, opt => opt.MapFrom(src => src.DefaultPhotoId ?? src._attachments.Keys.FirstOrDefault()));
            CreateMap<Products, ProductFeaturedProductsResponse>()
                .ForMember(x => x.Rating, opt => opt.MapFrom(src => src.Reviews.Select(x => x.Rating).Sum() / src.Reviews.Count))
                .ForMember(x => x.Price, opt => opt.MapFrom(src => (src.SalePrice != null && src.SalePrice > 0) ? src.SalePrice : src.Price))
                .ForMember(x => x.Photo, opt => opt.MapFrom(src => new ProductPhotoResponse()))
                .ForMember(x => x.PhotoId, opt => opt.MapFrom(src => src.DefaultPhotoId ?? src._attachments.Keys.FirstOrDefault()));
            CreateMap<Products, ProductRecentAddResponse>()
                .ForMember(x => x.Rating, opt => opt.MapFrom(src => src.Reviews.Select(x => x.Rating).Sum() / src.Reviews.Count))
                .ForMember(x => x.Price, opt => opt.MapFrom(src => (src.SalePrice != null && src.SalePrice > 0) ? src.SalePrice : src.Price))
                .ForMember(x => x.Photo, opt => opt.MapFrom(src => new ProductPhotoResponse()))
                .ForMember(x => x.PhotoId, opt => opt.MapFrom(src => src.DefaultPhotoId ?? src._attachments.Keys.FirstOrDefault()));
            CreateMap<Products, ProductShortOverviewResponse>()
                .ForMember(x => x.Rating, opt => opt.MapFrom(src => src.Reviews.Select(x => x.Rating).Sum() / src.Reviews.Count))
                .ForMember(x => x.PhotoId, opt => opt.MapFrom(src => src.DefaultPhotoId ?? src._attachments.Keys.FirstOrDefault()));
            CreateMap<ProductReview, ProductReviewResponse>();
            CreateMap<Products, ProductCartResponse>()
                .ForMember(x => x.ProductId, opt => opt.MapFrom(src => src.Id))
                .ForMember(x => x.SellerId, opt => opt.MapFrom(src => src.UserId))
                .ForMember(x => x.ProductName, opt => opt.MapFrom(src => src.Name))
                .ForMember(x => x.PhotoId, opt => opt.MapFrom(src => src.DefaultPhotoId))
                .ForMember(x => x.SelectedOptions, opt => opt.MapFrom(src => new List<ProductCartResponse>()))
                .ForMember(x => x.Price, opt => opt.MapFrom(src => (src.SalePrice != null && src.SalePrice > 0) ? src.SalePrice : src.Price))
                .ForMember(x => x.NumberOfAvailableProducts, opt => opt.MapFrom(src => src.Quantity));
            CreateMap<ProductCartOptionRequest, ProductCartOptionResponse>();
            CreateMap<Products, ProductRangeResponse>()
                .ForMember(x => x.ProductId, opt => opt.MapFrom(src => src.Id))
                .ForMember(x => x.ProductName, opt => opt.MapFrom(src => src.Name))
                .ForMember(x => x.Price, opt => opt.MapFrom(src => (src.SalePrice != null && src.SalePrice > 0) ? src.SalePrice : src.Price))
                .ForMember(x => x.SellerId, opt => opt.MapFrom(src => src.UserId))
                .ForMember(x => x.PhotoId, opt => opt.MapFrom(src => src.DefaultPhotoId));
            #endregion

            #region Order
            CreateMap<OrderAddRequest, Orders>()
                .ForMember(x => x.Id, opt => opt.MapFrom(src => Guid.NewGuid().ToString()))
                .ForMember(x => x.OrderStatus, opt => opt.MapFrom(src => OrderStatus.Created))
                .ForMember(x => x.OrderItems, opt => opt.MapFrom(src => new List<OrderItem>()))
                .ForMember(x => x.CreatedOn, opt => opt.MapFrom(src => DateTime.UtcNow));
            CreateMap<OrderItemRequest, OrderItem>()
                .ForMember(x => x.Id, opt => opt.MapFrom(src => Guid.NewGuid().ToString()))
                .ForMember(x => x.PhotoId, opt => opt.MapFrom(src => Guid.NewGuid().ToString()))
                .ForMember(x => x.Options, opt => opt.MapFrom(src => src.SelectedOptions));
            CreateMap<ProductCartOptionRequest, SelectedOption>();
            CreateMap<Orders, OrderOverviewResponse>()
                .ForMember(x => x.OrderId, opt => opt.MapFrom(src => src.Id));
            CreateMap<SelectedOption, OrderItemOptionResponse>();
            CreateMap<OrderItem, OrderItemResponse>()
                .ForMember(x => x.SelectedOptions, opt => opt.MapFrom(src => src.Options));

            #endregion
        }

    }
}
