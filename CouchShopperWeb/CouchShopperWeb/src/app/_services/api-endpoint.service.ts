import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class ApiEndpointService {
  apiBaseUrl = "https://localhost:44387/"

  /***********************************************************************************************************************/
  //User
  /***********************************************************************************************************************/
  get UserIsActive(): string {
    return this.apiBaseUrl + "user/is-active";
  }

  get UserActivateUser(): string {
    return this.apiBaseUrl + "user/activate-account";
  }

  get UserLogin(): string {
    return this.apiBaseUrl + "user/login";
  }

  get UserRegister(): string {
    return this.apiBaseUrl + "user/register";
  }

  /***********************************************************************************************************************/
  //Account
  /***********************************************************************************************************************/
  get UserGetProfileDetails(): string {
    return this.apiBaseUrl + "account/profile-details";
  }

  get UserGetProfilePicture(): string {
    return this.apiBaseUrl + "account/profile-picture";
  }

  get UserUpdateProfileDetails(): string {
    return this.apiBaseUrl + "account/update-profile-details";
  }

  get UserUpdatePassword(): string {
    return this.apiBaseUrl + "account/update-password";
  }

  get UserUploadProfilePicture(): string {
    return this.apiBaseUrl + "account/upload-profile-picture";
  }

  get UserPaymentMethod(): string {
    return this.apiBaseUrl + "account/payment-methods";
  }

  get UserAddCard(): string {
    return this.apiBaseUrl + "account/create-card";
  }

  get UserAddPaypal(): string {
    return this.apiBaseUrl + "account/create-paypal";
  }

  get UserUpdatePrimaryPaymentMethod(): string {
    return this.apiBaseUrl + "account/update-payment-method";
  }

  get DeletePaymentMethod(): string {
    return this.apiBaseUrl + "account/delete-payment-method";
  }

  /***********************************************************************************************************************/
  //Seller
  /***********************************************************************************************************************/
  get UserGetSellerInfo(): string {
    return this.apiBaseUrl + "seller/info";
  }

  get UserGetUserDetails(): string {
    return this.apiBaseUrl + "seller/details";
  }

  get UserGetSalesInfo(): string {
    return this.apiBaseUrl + "seller/sales";
  }

  get UserGetPayouts(): string {
    return this.apiBaseUrl + "seller/payouts";
  }

  get UserGetBalance(): string {
    return this.apiBaseUrl + "seller/balance";
  }

  get UserWithdrawal(): string {
    return this.apiBaseUrl + "seller/withdrawal";
  }

  /***********************************************************************************************************************/
  //Favorites
  /***********************************************************************************************************************/
  get UserAddToFavorites(): string {
    return this.apiBaseUrl + "favorites/add";
  }

  get UserGetFavorites(): string {
    return this.apiBaseUrl + "favorites/favorites";
  }

  get UserGetFavoritesCount(): string {
    return this.apiBaseUrl + "favorites/cout";
  }

  /***********************************************************************************************************************/
  //Common
  /***********************************************************************************************************************/
  get CommonGetIcons(): string {
    return this.apiBaseUrl + "icon/icon-datagrid";
  }

  get CommonAddIcon(): string {
    return this.apiBaseUrl + "icon/icon-create";
  }

  get CommonUpdateIcon(): string {
    return this.apiBaseUrl + "icon/icon-update";
  }

  get CommonDeleteIcon(): string {
    return this.apiBaseUrl + "icon/icon-delete";
  }

  get CommonGetIconDropdown(): string {
    return this.apiBaseUrl + "icon/icon-dropdown";
  }

  /***********************************************************************************************************************/
  //Special Offer
  /***********************************************************************************************************************/
  get GetSpecialOffer(): string {
    return this.apiBaseUrl + "special-offer/offer";
  }

  get CommonGetPublishedSpecialOffer(): string {
    return this.apiBaseUrl + "special-offer/offers";
  }

  get CommonGetSpecialOffers(): string {
    return this.apiBaseUrl + "special-offer/datagrid";
  }

  get CommonAddSpecialOffer(): string {
    return this.apiBaseUrl + "special-offer/create";
  }

  get CommonPublishUnpublishSpecialOffer(): string {
    return this.apiBaseUrl + "special-offer/publish-unpublish";
  }

  get CommonUpdateSpecialOffer(): string {
    return this.apiBaseUrl + "special-offer/update";
  }

  get CommonDeleteSpecialOffer(): string {
    return this.apiBaseUrl + "special-offer/delete";
  }

  /***********************************************************************************************************************/
  //Home
  /***********************************************************************************************************************/
  get CommonGetHomePageContent(): string {
    return this.apiBaseUrl + "home/content";
  }

  /***********************************************************************************************************************/
  //Category
  /***********************************************************************************************************************/
  get CommonCategories(): string {
    return this.apiBaseUrl + "category/extended-dropdown";
  }

  get CommonGetCategories(): string {
    return this.apiBaseUrl + "category/datagrid";
  }

  get CommonAddCategory(): string {
    return this.apiBaseUrl + "category/create";
  }

  get CommonUpdateCategory(): string {
    return this.apiBaseUrl + "category/update";
  }

  get CommonDeleteCategory(): string {
    return this.apiBaseUrl + "category/delete";
  }

  get CommonGetCategoryDropdown(): string {
    return this.apiBaseUrl + "category/dropdown";
  }

  /***********************************************************************************************************************/
  //Country
  /***********************************************************************************************************************/
  get CommonGetCountries(): string {
    return this.apiBaseUrl + "country/datagrid";
  }

  get CommonAddCountry(): string {
    return this.apiBaseUrl + "country/create";
  }

  get CommonUpdateCountry(): string {
    return this.apiBaseUrl + "country/update";
  }

  get CommonDeleteCountry(): string {
    return this.apiBaseUrl + "country/delete";
  }

  get CommonGetShippingOptions(): string {
    return this.apiBaseUrl + "country/shipping-options";
  }

  get CommonGetCountryShippingOptions(): string {
    return this.apiBaseUrl + "country/country-shipping-options";
  }

  get CommonGetCountryDropdown(): string {
    return this.apiBaseUrl + "country/dropdown";
  }

  /***********************************************************************************************************************/
  //City
  /***********************************************************************************************************************/
  get CommonGetCities(): string {
    return this.apiBaseUrl + "city/datagrid";
  }

  get CommonAddCity(): string {
    return this.apiBaseUrl + "city/create";
  }

  get CommonDeleteCity(): string {
    return this.apiBaseUrl + "city/delete";
  }

  get CommonUpdateCity(): string {
    return this.apiBaseUrl + "city/update";
  }

  get CommonGetCityDropdown(): string {
    return this.apiBaseUrl + "city/dropdown";
  }

  /***********************************************************************************************************************/
  //Product
  /***********************************************************************************************************************/
  get ProductAddProduct(): string {
    return this.apiBaseUrl + "product/create";
  }

  get ProductDeleteProduct(): string {
    return this.apiBaseUrl + "product";
  }

  get ProductUpdateProduct(): string {
    return this.apiBaseUrl + "product";
  }

  get ProductGetProduct(): string {
    return this.apiBaseUrl + "product";
  }

  get ProductGetProductCount(): string {
    return this.apiBaseUrl + "product/count";
  }

  get ProductGetProductsByUser(): string {
    return this.apiBaseUrl + "product/datagrid-per-user";
  }

  get ProductGetProductReviews(): string {
    return this.apiBaseUrl + "product/product-reviews";
  }
  get ProductGetCartItems(): string {
    return this.apiBaseUrl + "product/cart-items";
  }
  get ProductSearch(): string {
    return this.apiBaseUrl + "product/product-search";
  }

  /***********************************************************************************************************************/
  //Order
  /***********************************************************************************************************************/
  get OrderAddOrder(): string {
    return this.apiBaseUrl + "order/create";
  }

  get OrderGetNewOrders(): string {
    return this.apiBaseUrl + "order/order-created";
  }

  get OrderGetDeclinedOrders(): string {
    return this.apiBaseUrl + "order/order-declined";
  }

  get OrderGetApprovedOrders(): string {
    return this.apiBaseUrl + "order/order-approved";
  }

  get OrderGetSentOrders(): string {
    return this.apiBaseUrl + "order/order-sent";
  }

  get OrderGetDeliveredOrders(): string {
    return this.apiBaseUrl + "order/order-delivered";
  }

  get OrderConfirmOrder(): string {
    return this.apiBaseUrl + "order/order-confirm";
  }

  get OrderDeclineOrder(): string {
    return this.apiBaseUrl + "order/order-decline";
  }

  get OrderSentOrder(): string {
    return this.apiBaseUrl + "order/order-sent";
  }

  get OrderDeliveredOrder(): string {
    return this.apiBaseUrl + "order/order-delivered";
  }

  get OrderUserOrders(): string {
    return this.apiBaseUrl + "order/order-user-orders";
  }

  get OrderRateProduct(): string {
    return this.apiBaseUrl + "order/order-rating";
  }

  get OrderActiveOrdersCount(): string {
    return this.apiBaseUrl + "order/order-active-orders-count";
  }

}
