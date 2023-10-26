import { HttpParams } from '@angular/common/http';
import { Injectable, OnInit } from '@angular/core';
import { ThemeService } from 'ng2-charts';
import { Observable } from 'rxjs';
import { SettingsAccountPaymentComponent } from '../_components/settings/settings-account-payment/settings-account-payment.component';
import { AccountPasswordUpdateRequest, AccountResponse } from '../_models/Account/response/account-response';
import { NewProductReview } from '../_models/_product/request/product-request';
import { CardPaymentRequest, PaypalPaymentMethod } from '../_models/_settings/request/settings-request';
import { AccountService } from './account.service';
import { CommonService } from './common.service';
import { ModalService } from './modal.service';
import { OrderService } from './order.service';
import { ProductService } from './product.service';

@Injectable({
  providedIn: 'root'
})
export class SettingsService {

  constructor(private accountService: AccountService, private commonService: CommonService,
    private modalService: ModalService, private product: ProductService,
    private orderService: OrderService) { }

  public getProfilePicture() {
    return this.accountService.profilePicture$;
  }

  public getUserInfo(): Observable<AccountResponse> {
    return this.accountService.getProfileDetails()
  }

  public getCountryDropdown() {
    return this.commonService.getCountriesDropdown()
  }

  public getCityDropdown(countryName: string) {
    return this.commonService.getCitiesByCountriesDropdown(countryName);
  }

  public updateDetails(details: AccountResponse) {
    return this.accountService.updateProfileDetails(details);
  }

  public uploadProfilePicture(picture: string): Observable<boolean> {
    return this.accountService.uploadProfilePicture(picture)
  }

  public reloadProfilePicture() {
    return this.accountService.reloadProfilePicture()
  }

  public saveNewPassword(password: AccountPasswordUpdateRequest) {
    return this.accountService.updatePassword(password);
  }

  public modalOpen(paymentMethod: SettingsAccountPaymentComponent) {
    const contentTemplate = paymentMethod;
    const handleConfirm = () => {
    };
    const handleClose = () => {
      this.modalService.closeModal();
    };
    this.modalService.openModal('', contentTemplate, false, false, handleConfirm, handleClose);
  }

  public modelClose() {
    this.modalService.closeModal();
  }

  public getPaymentMethods() {
    return this.accountService.getPaymentMethods();
  }

  public addCard(card: CardPaymentRequest) {
    return this.accountService.createCard(card);
  }

  public addPaypal(card: PaypalPaymentMethod) {
    return this.accountService.createPaypal(card);
  }

  public deletePaymentMethod(id: string) {
    return this.accountService.deletePaymentMethod(id)
  }

  public deleteProduct(productId: string) {
    return this.product.deleteProduct(productId);
  }
  public usersProductCount() {
    return this.product.getProductCount()
  }

  public usersProduct(page: number) {
    return this.product.getProductsPerUser(page);
  }

  public getProductPreview(id: string) {
    return this.product.getProduct(id);
  }

  public getUserOrders(page: number) {
    return this.orderService.getUserOrders(page)
  }

  public ratingModalOpen(loginComponent: any) {
    const contentTemplate = loginComponent;
    const handleConfirm = () => {
    };
    const handleClose = () => {
      this.modalService.closeModal();
    };
    this.modalService.openModal('', contentTemplate, false, false, handleConfirm, handleClose);
  }

  public rateProduct(rating: NewProductReview) {
    return this.orderService.orderRating(rating)
  }

  public getFavorites() {
    return this.accountService.getFavorites();
  }

  public getSalesInfo(periodId: number) {
    return this.accountService.getSalesInfo(periodId)
  }

  public getPayouts(page:number) {
    return this.accountService.getPayouts(page)
  }

  public withdrawal() {
    return this.accountService.withdrawal()
  }

  public updatePrimaryPaymentMethod(paymentId:string) {
    return this.accountService.updatePrimaryPaymentMethod(paymentId);
  }
}
