import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ToastrModule } from 'ngx-toastr'
import { NavComponent } from '../_components/navigation/nav/nav.component';
import { ProductComponent } from '../_components/product/product.component';
import { FeaturedProductsComponent } from '../_components/home/featured-products/featured-products.component';
import { SpecialOfferComponent } from '../_components/home/special-offer/special-offer.component';
import { NavDropdownComponent } from '../_components/navigation/nav-dropdown/nav-dropdown.component';
import { ProductGridComponent } from '../_components/product-grid/product-grid.component';
import { MostRecentProductsComponent } from '../_components/home/most-recent-products/most-recent-products.component';
import { TopSellerComponent } from '../_components/home/top-seller/top-seller.component';
import { HomeComponent } from '../_components/home/home/home.component';
import { ProductDetailsComponent } from '../_components/product-details/product-details/product-details.component';
import { ProductHeaderComponent } from '../_components/product-details/product-header/product-header.component';
import { GalleryComponent } from '../_components/product-details/gallery/gallery.component';
import { ProductOptionsComponent } from '../_components/product-details/product-options/product-options.component';
import { ProductInfoComponent } from '../_components/product-details/product-info/product-info.component';
import { ProductReviewComponent } from '../_components/product-details/product-review/product-review.component';
import { ProductPricingComponent } from '../_components/product-details/product-pricing/product-pricing.component';
import { ProductAvailabilityComponent } from '../_components/product-details/product-availability/product-availability.component';
import { ProductPanelsComponent } from '../_components/product-details/product-panels/product-panels.component';
import { ProductDescriptionPanelComponent } from '../_components/product-details/product-description-panel/product-description-panel.component';
import { ProductShippingPanelComponent } from '../_components/product-details/product-shipping-panel/product-shipping-panel.component';
import { ProductCommentsComponent } from '../_components/product-details/product-comments/product-comments.component';
import { SellerHeaderComponent } from '../_components/seller/seller-header/seller-header.component';
import { SellerComponent } from '../_components/seller/seller-info/seller.component';
import { LoadingSpinnerComponent } from '../_components/loading-spinner/loading-spinner.component';
import { SellerInfoComponent } from '../_components/product-details/seller-info/seller-info.component';
import { ModalComponent } from '../_components/modal/modal.component';
import { AccountComponent } from '../_components/account/account/account.component';
import { CheckoutHeaderComponent } from '../_components/cart/checkout-header/checkout-header.component';
import { CartComponent } from '../_components/cart/cart/cart.component';
import { CheckoutStepsComponent } from '../_components/cart/checkout-steps/checkout-steps.component';
import { CheckoutComponent } from '../_components/cart/checkout/checkout.component';
import { CheckoutDetailsComponent } from '../_components/cart/checkout-details/checkout-details.component';
import { CheckoutShippingComponent } from '../_components/cart/checkout-shipping/checkout-shipping.component';
import { CheckoutPaymentComponent } from '../_components/cart/checkout-payment/checkout-payment.component';
import { CheckoutReviewComponent } from '../_components/cart/checkout-review/checkout-review.component';
import { CartAsideComponent } from '../_components/cart/cart-aside/cart-aside.component';
import { CheckoutAsideComponent } from '../_components/cart/checkout-aside/checkout-aside.component';
import { SettingsComponent } from '../_components/settings/settings/settings.component';
import { SettingsAccountComponent } from '../_components/settings/settings-account/settings-account.component';
import { SettingsPurchasesComponent } from '../_components/settings/settings-purchases/settings-purchases.component';
import { SettingsAsideComponent } from '../_components/settings/settings-aside/settings-aside.component';
import { SettingsFavoritesComponent } from '../_components/settings/settings-favorites/settings-favorites.component';
import { SettingsSalesComponent } from '../_components/settings/settings-sales/settings-sales.component';
import { SettingsProductsComponent } from '../_components/settings/settings-products/settings-products.component';
import { SettingsNewProductComponent } from '../_components/settings/settings-new-product/settings-new-product.component';
import { SettingsPayoutsComponent } from '../_components/settings/settings-payouts/settings-payouts.component';
import { AccountActivateComponent } from '../_components/account/account-activate/account-activate.component';
import { CountriesComponent } from '../_components/settings/admin/countries/countries.component';


import { CarouselModule } from 'ngx-bootstrap/carousel';
import { CollapseModule } from 'ngx-bootstrap/collapse';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { RatingModule } from 'ngx-bootstrap/rating';
import { ModalModule } from 'ngx-bootstrap/modal';
import { TabsModule } from 'ngx-bootstrap/tabs';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { AppRoutingModule } from '../app-routing.module';
import { PaginationModule } from 'ngx-bootstrap/pagination';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { FileUploadComponent } from '../_components/file-upload/file-upload.component';
import { SettingsAccountPaymentComponent } from '../_components/settings/settings-account-payment/settings-account-payment.component';
import { CategoriesComponent } from '../_components/settings/admin/categories/categories.component';
import { IconPickerComponent } from '../_components/icon-picker/icon-picker.component';
import { IconsComponent } from '../_components/settings/icons/icons.component';
import { StttingsProductOverviewComponent } from '../_components/settings/stttings-product-overview/stttings-product-overview.component';
import { SettingsHomeComponent } from '../_components/settings/admin/settings-home/settings-home.component';
import { OrderOverviewComponent } from '../_components/settings/admin/order/order-overview/order-overview.component';
import { AccordionModule } from 'ngx-bootstrap/accordion';
import { OrderNewOrdersComponent } from '../_components/settings/admin/order/order-new-orders/order-new-orders.component';
import { OrderDeclinedOrdersComponent } from '../_components/settings/admin/order/order-declined-orders/order-declined-orders.component';
import { OrderApprovedOrdersComponent } from '../_components/settings/admin/order/order-approved-orders/order-approved-orders.component';
import { OrderDeliveredOrdersComponent } from '../_components/settings/admin/order/order-delivered-orders/order-delivered-orders.component';
import { OrderSentOrdersComponent } from '../_components/settings/admin/order/order-sent-orders/order-sent-orders.component';
import { SearchResaultComponent } from '../_components/search-resault/search-resault.component';
import { HasRoleDirective } from '../_directives/has-role.directive';


@NgModule({
  declarations: [
    NavComponent,
    ProductComponent,
    SpecialOfferComponent,
    FeaturedProductsComponent,
    NavDropdownComponent,
    ProductGridComponent,
    MostRecentProductsComponent,
    TopSellerComponent,
    HomeComponent,
    ProductDetailsComponent,
    ProductHeaderComponent,
    GalleryComponent,
    ProductOptionsComponent,
    ProductInfoComponent,
    ProductReviewComponent,
    ProductPricingComponent,
    ProductAvailabilityComponent,
    ProductPanelsComponent,
    ProductDescriptionPanelComponent,
    ProductShippingPanelComponent,
    ProductCommentsComponent,
    SellerHeaderComponent,
    SellerComponent,
    LoadingSpinnerComponent,
    SellerInfoComponent,
    ModalComponent,
    AccountComponent,
    CheckoutHeaderComponent,
    CartComponent,
    CheckoutStepsComponent,
    CheckoutComponent,
    CheckoutDetailsComponent,
    CheckoutShippingComponent,
    CheckoutPaymentComponent,
    CheckoutReviewComponent,
    CartAsideComponent,
    CheckoutAsideComponent,
    SettingsComponent,
    SettingsAccountComponent,
    SettingsPurchasesComponent,
    SettingsAsideComponent,
    SettingsFavoritesComponent,
    SettingsSalesComponent,
    SettingsProductsComponent,
    SettingsNewProductComponent,
    SettingsPayoutsComponent,
    AccountActivateComponent,
    CountriesComponent,
    FileUploadComponent,
    SettingsAccountPaymentComponent,
    CategoriesComponent,
    IconPickerComponent,
    IconsComponent,
    StttingsProductOverviewComponent,
    SettingsHomeComponent,
    OrderOverviewComponent,
    OrderNewOrdersComponent,
    OrderDeclinedOrdersComponent,
    OrderApprovedOrdersComponent,
    OrderSentOrdersComponent,
    OrderDeliveredOrdersComponent,
    SearchResaultComponent,
    HasRoleDirective
  ],
  imports: [
    AppRoutingModule,
    CommonModule,
    BrowserAnimationsModule,
    ReactiveFormsModule,
    FormsModule,
    CarouselModule.forRoot(),
    CollapseModule.forRoot(),
    BsDropdownModule.forRoot(),
    RatingModule.forRoot(),
    ModalModule.forRoot(),
    TabsModule.forRoot(),
    PaginationModule.forRoot(),
    AccordionModule.forRoot(),
    ToastrModule.forRoot({
      positionClass: 'toast-top-center', 
      preventDuplicates: true, 
      closeButton: true, 
      timeOut: 3000 })
  ],
  exports: [
    AppRoutingModule,
    BrowserAnimationsModule,
    NavComponent,
    ProductComponent,
    FeaturedProductsComponent,
    SpecialOfferComponent,
    FeaturedProductsComponent,
    NavDropdownComponent,
    ProductGridComponent,
    MostRecentProductsComponent,
    TopSellerComponent,
    HomeComponent,
    ProductDetailsComponent,
    ProductHeaderComponent,
    GalleryComponent,
    ProductOptionsComponent,
    ProductInfoComponent,
    ProductReviewComponent,
    ProductPricingComponent,
    ProductAvailabilityComponent,
    ProductPanelsComponent,
    ProductDescriptionPanelComponent,
    ProductShippingPanelComponent,
    ProductCommentsComponent,
    SellerHeaderComponent,
    SellerComponent,
    LoadingSpinnerComponent,
    SellerInfoComponent,
    ModalComponent,
    AccountComponent,
    CheckoutHeaderComponent,
    CartComponent,
    CheckoutStepsComponent,
    CheckoutComponent,
    CheckoutDetailsComponent,
    CheckoutShippingComponent,
    CheckoutPaymentComponent,
    CheckoutReviewComponent,
    CartAsideComponent,
    CheckoutAsideComponent,
    SettingsComponent,
    SettingsAccountComponent,
    SettingsPurchasesComponent,
    SettingsAsideComponent,
    SettingsFavoritesComponent,
    SettingsSalesComponent,
    CommonModule,
    SettingsProductsComponent,
    SettingsNewProductComponent,
    SettingsPayoutsComponent,
    AccountActivateComponent,
    CountriesComponent,
    PaginationModule,
    FileUploadComponent,
    SettingsAccountPaymentComponent,
    CategoriesComponent,
    IconPickerComponent,
    IconsComponent,
    StttingsProductOverviewComponent,
    SettingsHomeComponent,
    OrderOverviewComponent,
    OrderNewOrdersComponent,
    OrderDeclinedOrdersComponent,
    OrderApprovedOrdersComponent,
    OrderSentOrdersComponent,
    OrderDeliveredOrdersComponent,
    SearchResaultComponent,
    ToastrModule,
    HasRoleDirective
  ]
})
export class SharedModule { }
