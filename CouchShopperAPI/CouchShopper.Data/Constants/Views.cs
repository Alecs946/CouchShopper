using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CouchShopper.Data.Constants
{
    public static class Views
    {
        public const string byUsername = @"_design/active/_view/by_username";

        #region Common
        public const string activeCategories = @"_design/active/_view/active-categories";
        public const string existingCategoriesByName = @"_design/existing/_view/existing-category-by-name";
        #endregion

        #region Products
        public const string productByUserWithName = @"_design/active/_view/product_by_user_with_name";
        public const string productsPerUser = @"_design/active/_view/products_per_user";
        public const string productsByUser = @"_design/active/_view/product_by_user";
        public const string activeProduct = @"_design/active/_view/active-products";
        public const string featuredProducts = @"_design/active/_view/featured_products";
        public const string activeProductsWithDate = @"_design/active/_view/active-products-with-date";
        #endregion
        #region Users
        public const string userRating = @"_design/users/_view/user-rating?group=true";
        public const string userEarnings = @"_design/active/_view/user-earnings";
        #endregion
        #region Orders
        public const string orderByStatus = @"_design/active/_view/order-by-status";
        public const string ordersPerBuyer = @"_design/active/_view/orders-per-buyer";
        public const string productEarnings = @"_design/active/_view/product-earnings";
        public const string orderBySeller = @"_design/active/_view/order-by-seller";
        public const string ordersInPeriod = @"_design/active/_view/orders-in-period";

        #endregion

    }
}
