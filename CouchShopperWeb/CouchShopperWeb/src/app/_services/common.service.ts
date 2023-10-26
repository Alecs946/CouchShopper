import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { LoginResponse } from '../_models/Account/response/user-response';
import { Category } from '../_models/_common/request/category-request';
import { CategoryList } from '../_models/_common/response/category-response';
import { City } from '../_models/_common/request/city-request';
import { CityList } from '../_models/_common/response/city-response';
import { Country } from '../_models/_common/request/country-request';
import { CountryList } from '../_models/_common/response/country-response';
import { HomeContentResponse } from '../_models/_common/response/home-content-response';
import { Icon } from '../_models/_common/request/icon-request';
import { IconList } from '../_models/_common/response/icon-response';
import { SpecialOffer, PublishUnpublishRequest } from '../_models/_common/request/special-offer-request';
import { SpecialOfferList } from '../_models/_common/response/special-offer-response';
import { AvatarDropDownGroup, DropDownItem, Dropdown } from '../_models/_common/response/home-content-response';
import { Shipping } from '../_models/_product/response/product-response';
import { ApiEndpointService } from './api-endpoint.service';
import { OrderService } from './order.service';
import { AccountService } from './account.service';

@Injectable({
  providedIn: 'root'
})
export class CommonService {
  constructor(private http: HttpClient, private apiBase: ApiEndpointService, private orderService: OrderService, private accountService: AccountService) { }

  public getAvatarDropdown(balance$: Observable<number>, favorites$: Observable<number>): Observable<AvatarDropDownGroup[]> {
    const avatarDropDownItems: AvatarDropDownGroup[] = [
      {
        groupName: "Account",
        role: ['1', '2', '3'],
        dropDownItems: [
          { name: "Settings", iconName: "fa-solid fa-gear", action: "/settings", alwaysShow: true, role: ['1', '2', '3'] },
          { name: "My profile", iconName: "fa-solid fa-user", action: "/settings", alwaysShow: true, role: ['1', '2', '3'] },
          { name: "Purchases", iconName: "fa-solid fa-basket-shopping", numberOfNotifications: this.getOrdersCount(), action: "/purchases", alwaysShow: true, role: ['3'] },
          { name: "Favorites", iconName: "fa-solid fa-heart", numberOfNotifications: favorites$, action: "/favorites", alwaysShow: false, role: ['3'] },
          { name: "Cart", iconName: "fas fa-shopping-cart", action: "/cart", alwaysShow: false, role: ['3'] },
        ]
      },
      {
        groupName: "Seller Dashboard",
        role: ['2'],
        dropDownItems: [
          { name: "Add New Product", iconName: "fa-solid fa-plus", action: "/new-product", alwaysShow: true, role: ['2'] },
          { name: "Products", iconName: "fa-solid fa-box", action: "/products", alwaysShow: true, role: ['2'] },
          { name: "Sales", iconName: "fa-solid fa-dollar-sign", numberOfNotifications: balance$, action: "/sales", alwaysShow: true, role: ['2'] },
          { name: "Payouts", iconName: "fa-solid fa-money-bill-wave", action: "/payouts", alwaysShow: true, role: ['2'] },
        ]
      }, {
        groupName: "Admin Dashboard",
        role: ['1'],
        dropDownItems: [
          { name: "Countries", iconName: "fa-solid fa-earth-europe", action: "/countries", alwaysShow: true, role: ['1'] },
          { name: "Categories", iconName: "fa-solid fa-clipboard-list", action: "/categories", alwaysShow: true, role: ['1'] },
          { name: "Icons", iconName: "fa-solid fa-icons", action: "/icons", alwaysShow: true, role: ['1'] },
          { name: "Special Offer", iconName: "fa-solid fa-award", action: "/special-offer", alwaysShow: true, role: ['1'] },
          { name: "New Orders", iconName: "fa-solid fa-boxes-packing", action: "/order-new", alwaysShow: true, role: ['1'] },
          { name: "Approved Orders", iconName: "fa-solid fa-dolly", action: "/order-approved", alwaysShow: true, role: ['1'] },
          { name: "Sent Orders", iconName: "fa-solid fa-truck-ramp-box", action: "/order-sent", alwaysShow: true, role: ['1'] },
          { name: "Delivered Orders", iconName: "fa-solid fa-box-open", action: "/order-delivered", alwaysShow: true, role: ['1'] },
          { name: "Declined Orders", iconName: "fa-solid fa-rectangle-xmark", action: "/order-declined", alwaysShow: true, role: ['1'] },
        ]
      }
    ]
    return of(avatarDropDownItems)
  }

  /***********************************************************************************************************************/
  ///Category
  /***********************************************************************************************************************/
  public getCategoriesExtendedDropown(): Observable<DropDownItem[]> {
    return this.http.get<DropDownItem[]>(this.apiBase.CommonCategories);
  }

  public getCategories(page: number): Observable<CategoryList> {
    const params = new HttpParams().append('page', page);
    return this.http.get<CategoryList>(this.apiBase.CommonGetCategories, { params });
  }

  public addCategory(category: Category) {
    return this.http.post<CategoryList>(this.apiBase.CommonAddCategory, category);
  }

  public updateCategory(category: Category) {
    return this.http.put<CategoryList>(this.apiBase.CommonUpdateCategory, category);
  }

  public deleteCategory(categoryId: string) {
    const params = new HttpParams().append('categoryId', categoryId);
    return this.http.delete<CategoryList>(this.apiBase.CommonDeleteCategory, { params });
  }

  public getCategoriesDropdown(): Observable<Dropdown[]> {
    return this.http.get<Dropdown[]>(this.apiBase.CommonGetCategoryDropdown);
  }

  /***********************************************************************************************************************/
  ///Country
  /***********************************************************************************************************************/
  public getCountries(page: number): Observable<CountryList> {
    const params = new HttpParams().append('page', page);
    return this.http.get<CountryList>(this.apiBase.CommonGetCountries, { params });
  }

  public addCountry(country: Country) {
    return this.http.post<CountryList>(this.apiBase.CommonAddCountry, country);
  }

  public updateCountry(country: Country) {
    return this.http.put<CountryList>(this.apiBase.CommonUpdateCountry, country);
  }

  public deleteCountry(countryId: string) {
    const params = new HttpParams().append('countryId', countryId);
    return this.http.delete<CountryList>(this.apiBase.CommonDeleteCountry, { params });
  }

  public getShippingOptions() {
    const params = new HttpParams().append('userId', this.accountService.getUsername());
    return this.http.get<Shipping[]>(this.apiBase.CommonGetShippingOptions, { params });
  }

  public getCountryShippingOptions(country: string) {
    const params = new HttpParams().append('country', country);
    return this.http.get<Shipping[]>(this.apiBase.CommonGetCountryShippingOptions, { params });
  }

  public getCountriesDropdown(): Observable<Dropdown[]> {
    return this.http.get<Dropdown[]>(this.apiBase.CommonGetCountryDropdown);
  }

  /***********************************************************************************************************************/
  ///Country
  /***********************************************************************************************************************/
  public getCitiesByCountry(page: number, countryId: string): Observable<CityList> {
    const params = new HttpParams().append('page', page)
      .append("countryId", countryId);
    return this.http.get<CityList>(this.apiBase.CommonGetCities, { params });
  }

  public addCity(city: City) {
    return this.http.post<City>(this.apiBase.CommonAddCity, city);
  }

  public updateCity(city: City) {
    return this.http.put<CityList>(this.apiBase.CommonUpdateCity, city);
  }

  public deleteCity(cityId: string) {
    const params = new HttpParams().append('cityId', cityId);
    return this.http.delete<CountryList>(this.apiBase.CommonDeleteCity, { params });
  }

  public getCitiesByCountriesDropdown(countryName: string): Observable<Dropdown[]> {
    const params = new HttpParams().append('countryName', countryName);
    return this.http.get<Dropdown[]>(this.apiBase.CommonGetCityDropdown, { params });
  }

  /***********************************************************************************************************************/
  ///Icon
  /***********************************************************************************************************************/
  public getIcons(page: number): Observable<IconList> {
    const params = new HttpParams().append('page', page);
    return this.http.get<IconList>(this.apiBase.CommonGetIcons, { params });
  }

  public addIcon(icon: Icon) {
    return this.http.post<IconList>(this.apiBase.CommonAddIcon, icon);
  }

  public updateIcon(icon: Icon) {
    return this.http.put<IconList>(this.apiBase.CommonUpdateIcon, icon);
  }

  public deleteIcon(iconId: string) {
    const params = new HttpParams().append('iconId', iconId);
    return this.http.delete<IconList>(this.apiBase.CommonDeleteIcon, { params });
  }

  public getIconsDropdown(): Observable<Dropdown[]> {
    return this.http.get<Dropdown[]>(this.apiBase.CommonGetIconDropdown);
  }

  /***********************************************************************************************************************/
  ///Special offer
  /***********************************************************************************************************************/
  public getSpecialOffers(page: number): Observable<SpecialOfferList> {
    return this.http.get<SpecialOfferList>(this.apiBase.CommonGetSpecialOffers);
  }

  public getPublishedSpecialOffer() {
    return this.http.get<SpecialOffer[]>(this.apiBase.CommonGetPublishedSpecialOffer);
  }

  public getSpecialOffer(id: string) {
    const params = new HttpParams().append('id', id);
    return this.http.get<SpecialOffer>(this.apiBase.GetSpecialOffer, { params });
  }

  public addSpecialOffer(specialOffer: SpecialOffer) {
    return this.http.post<SpecialOffer>(this.apiBase.CommonAddSpecialOffer, specialOffer);
  }

  public publisUnpublishSpecialOffer(publishUnpublishRequest: PublishUnpublishRequest) {
    return this.http.put(this.apiBase.CommonPublishUnpublishSpecialOffer, publishUnpublishRequest);
  }

  public updateSpecialOffer(specialOffer: SpecialOffer) {
    return this.http.put<SpecialOffer>(this.apiBase.CommonUpdateSpecialOffer, specialOffer);
  }

  public deleteSpecialOffer(specialOfferId: string) {
    const params = new HttpParams().append('specialOfferId', specialOfferId);
    return this.http.delete(this.apiBase.CommonDeleteSpecialOffer, { params });
  }

  /***********************************************************************************************************************/
  ///Home page
  /***********************************************************************************************************************/
  public getHomePageContent() {
    return this.http.get<HomeContentResponse>(this.apiBase.CommonGetHomePageContent)
  }

  /***********************************************************************************************************************/
  ///Order
  /***********************************************************************************************************************/
  public getOrdersCount() {
    return this.orderService.getActiveOrdersCount();
  }
}
