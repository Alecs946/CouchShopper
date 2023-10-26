import { Injectable } from '@angular/core';
import { Route, Router } from '@angular/router';
import { Observable, of, Subject } from 'rxjs';
import { AccountActivateComponent } from '../_components/account/account-activate/account-activate.component';
import { AccountComponent } from '../_components/account/account/account.component';
import { LoginResponse } from '../_models/Account/response/user-response';
import { AvatarDropDownGroup, DropDownItem } from '../_models/_common/response/home-content-response';
import { AccountService } from './account.service';
import { CommonService } from './common.service';
import { ModalService } from './modal.service';
import { ShoppingCartService } from './shopping-cart.service';

@Injectable({
  providedIn: 'root'
})
export class NavigationService {

  constructor(private cartService: ShoppingCartService, private modalService: ModalService,
    private commonService: CommonService, private accountService: AccountService, private router: Router,
    private shoppingCartService: ShoppingCartService) { }

  public modalOpen(loginComponent: AccountComponent) {
    const contentTemplate = loginComponent;
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

  public currentUser(): Observable<LoginResponse | null> {
    return this.accountService.currentUser$;
  }

  public getAvatarDropdown(): Observable<AvatarDropDownGroup[]> {
    return this.commonService.getAvatarDropdown(this.getBalance(), this.getFavoritesCount());
  }

  public getActiveOrdersCount() {
    return this.commonService.getOrdersCount()
  }

  public getCategories(): Observable<DropDownItem[]> {
    return this.commonService.getCategoriesExtendedDropown();
  }

  public logoutUser() {
    return this.accountService.logout();
  }

  public activateAccount(accountActivat: AccountActivateComponent) {
    const contentTemplate = accountActivat;
    const handleConfirm = () => {
    };
    const handleClose = () => {
      this.modalService.closeModal();
    };
    this.modalService.openModal('', contentTemplate, false, false, handleConfirm, handleClose);
  }

  public getProfilePicture() {
    return this.accountService.profilePicture$;
  }
  public getCartItemsCount() {
    return this.shoppingCartService.getTotalItemsCount()
  }
  public getBalance() {
    return this.accountService.getBalance();
  }
  public getFavoritesCount() {
    return this.accountService.getFavoritesCount();
  }
  
}
