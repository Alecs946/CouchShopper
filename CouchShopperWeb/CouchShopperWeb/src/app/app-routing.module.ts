import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CheckoutComponent } from './_components/cart/checkout/checkout.component';
import { HomeComponent } from './_components/home/home/home.component';
import { ProductDetailsComponent } from './_components/product-details/product-details/product-details.component';
import { SearchResaultComponent } from './_components/search-resault/search-resault.component';
import { SellerComponent } from './_components/seller/seller-info/seller.component';
import { OrderOverviewComponent } from './_components/settings/admin/order/order-overview/order-overview.component';
import { SettingsComponent } from './_components/settings/settings/settings.component';
import { StttingsProductOverviewComponent } from './_components/settings/stttings-product-overview/stttings-product-overview.component';
import { NotFoundComponent } from './errors/not-found/not-found.component';
import { AuthGuard } from './_guards/auth.guard';
import { AdminGuard, BuyerGuard, SellerGuard } from './_guards/role.guard';
const routes: Routes = [
  { path: '', component: HomeComponent },
  {
    path: '',
    children: [
      { path: 'product-details/:id', component: ProductDetailsComponent },
      { path: 'seller/:id', component: SellerComponent },
      { path: 'cart', component: CheckoutComponent, canActivate: [AuthGuard, BuyerGuard] },
      { path: 'settings', component: SettingsComponent, canActivate: [AuthGuard] },
      { path: 'purchases', component: SettingsComponent, canActivate: [AuthGuard, BuyerGuard] },
      { path: 'favorites', component: SettingsComponent, canActivate: [AuthGuard, BuyerGuard] },
      { path: 'sales', component: SettingsComponent, canActivate: [AuthGuard, SellerGuard] },
      { path: 'products', component: SettingsComponent, canActivate: [AuthGuard, SellerGuard] },
      { path: 'edit-product/:id', component: SettingsComponent, canActivate: [AuthGuard, SellerGuard] },
      { path: 'new-product', component: SettingsComponent, canActivate: [AuthGuard, SellerGuard] },
      { path: 'payouts', component: SettingsComponent, canActivate: [AuthGuard, SellerGuard] },
      { path: 'countries', component: SettingsComponent, canActivate: [AuthGuard, AdminGuard] },
      { path: 'categories', component: SettingsComponent, canActivate: [AuthGuard, AdminGuard] },
      { path: 'icons', component: SettingsComponent, canActivate: [AuthGuard, AdminGuard] },
      { path: 'special-offer', component: SettingsComponent, canActivate: [AuthGuard, AdminGuard] },
      { path: 'product-overview/:id', component: StttingsProductOverviewComponent, canActivate: [AuthGuard] },
      { path: 'order-new', component: OrderOverviewComponent, canActivate: [AuthGuard, AdminGuard] },
      { path: 'order-approved', component: OrderOverviewComponent, canActivate: [AuthGuard, AdminGuard] },
      { path: 'order-sent', component: OrderOverviewComponent, canActivate: [AuthGuard, AdminGuard] },
      { path: 'order-delivered', component: OrderOverviewComponent, canActivate: [AuthGuard, AdminGuard] },
      { path: 'order-declined', component: OrderOverviewComponent, canActivate: [AuthGuard, AdminGuard] },
      { path: 'search', component: SearchResaultComponent },
      { path: 'search/:searchPhrase/:searchCategory', component: SearchResaultComponent },
      { path: '**', component: NotFoundComponent, pathMatch: 'full' },
    ]
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
