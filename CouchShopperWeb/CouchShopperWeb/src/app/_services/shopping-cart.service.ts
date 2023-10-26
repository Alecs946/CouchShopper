import { Injectable, OnInit } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { CartItem, SelectedOption } from '../_models/_cart/request/cart-request';
import { ShippingInfo } from '../_models/_cart/response/cart-response';
import { OrderAddRequest } from '../_models/_order/request/order-request';
import { Shipping } from '../_models/_product/response/product-response';
import { PaymentMethod } from '../_models/_settings/response/settings-response';
import { AccountService } from './account.service';
import { CommonService } from './common.service';
import { OrderService } from './order.service';
import { ProductService } from './product.service';

@Injectable({
  providedIn: 'root'
})
export class ShoppingCartService {
  private cartItems: CartItem[] = [];
  private step!: number;
  totalItems: BehaviorSubject<number>;
  constructor(private productService: ProductService, private accountService: AccountService, private commonService: CommonService, private orderService: OrderService) {
    this.loadCartItems();
    const currentStep = localStorage.getItem('currentStep');
    if (currentStep) {
      this.step = JSON.parse(currentStep);
    }
    else {
      this.step = 1;
      localStorage.setItem('currentStep', JSON.stringify(this.step));
    }
    this.totalItems = new BehaviorSubject(this.cartItems.length);
  }

  public addToCart(item: CartItem) {
    const existingItem = this.cartItems.find(cartItem =>
      this.areOptionsEqual(cartItem.selectedOption, item.selectedOption)
    );

    if (existingItem) {
      existingItem.quantity += item.quantity;
    } else {
      this.cartItems.push(item);
    }

    this.saveCartItems();
    this.totalItems.next(this.cartItems.length);
  }

  private areOptionsEqual(options1: SelectedOption[], options2: SelectedOption[]): boolean {
    if (options1.length !== options2.length) {
      return false;
    }

    for (let i = 0; i < options1.length; i++) {
      if (
        options1[i].optionName !== options2[i].optionName ||
        options1[i].optionValue !== options2[i].optionValue
      ) {
        return false;
      }
    }

    return true;
  }

  public updateCartItemQuantity(item: CartItem, newQuantity: number) {
    const existingItem = this.cartItems.find(cartItem =>
      this.areOptionsEqual(cartItem.selectedOption, item.selectedOption)
    );

    if (existingItem) {
      existingItem.quantity = newQuantity;
      this.saveCartItems();
      this.totalItems.next(this.cartItems.length);
    }
  }

  public removeFromCart(item: CartItem) {
    const index = this.cartItems.findIndex(cartItem =>
      this.areOptionsEqual(cartItem.selectedOption, item.selectedOption)
    );
    if (index > -1) {
      this.cartItems.splice(index, 1);
      this.saveCartItems();
      this.totalItems.next(this.cartItems.length);
    }
  }

  public getCartItems() {
    return this.cartItems;
  }

  public getTotalItemsCount() {
    return this.totalItems.asObservable();
  }

  public clearCart() {
    localStorage.removeItem('cartItems')
    localStorage.removeItem('details')
    localStorage.removeItem('shipping')
    localStorage.removeItem('payment')
    localStorage.removeItem('currentStep')
    this.totalItems.next(this.cartItems?.length ?? 0);
  }

  public getCurentStep() {
    return this.step;
  }

  public setCurrentStep(step: number) {
    this.step = step;
    localStorage.setItem('currentStep', JSON.stringify(this.step));
  }

  public getCartItemsDetails() {
    return this.productService.getCartItems(this.cartItems)
  }

  public getUserDetails() {
    return this.accountService.getProfileDetails()
  }

  public getUserPhoto() {
    return this.accountService.profilePicture$
  }

  public getCountryDropdown() {
    return this.commonService.getCountriesDropdown()
  }

  public getCityDropdown(countryName: string) {
    return this.commonService.getCitiesByCountriesDropdown(countryName);
  }

  public getShippingOptions(country: string) {
    return this.commonService.getCountryShippingOptions(country)
  }

  private saveCartItems() {
    localStorage.setItem('cartItems', JSON.stringify(this.cartItems));
  }
  private loadCartItems() {
    const savedCartItems = localStorage.getItem('cartItems');
    if (savedCartItems) {
      this.cartItems = JSON.parse(savedCartItems);
    }
  }
  /////////////////////////////////////////////////////////////////////////
  public addCheckoutDetails(details: ShippingInfo) {
    localStorage.setItem('details', JSON.stringify(details));
  }
  public removeCheckoutDetails() {
    localStorage.removeItem('details');
  }

  public getCheckoutDetails() {
    const checkoutDetails = localStorage.getItem('details')
    if (checkoutDetails) {
      return JSON.parse(checkoutDetails) as ShippingInfo;
    }
    return null;
  }

  public addShippingDetails(details: Shipping) {
    localStorage.setItem('shipping', JSON.stringify(details));
  }

  public removeShippingDetails() {
    localStorage.removeItem('shipping');
  }

  public getShippingDetails() {
    const shippingDetails = localStorage.getItem('shipping')
    if (shippingDetails) {
      return JSON.parse(shippingDetails) as Shipping;
    }
    return null;
  }
  ////////////////////////////////////////////////////////////////////////

  public getPaymentMethods() {
    return this.accountService.getPaymentMethods()
  }

  public addPaymentDetails(details: PaymentMethod) {
    localStorage.setItem('payment', JSON.stringify(details));
  }

  public removePaymentDetails() {
    localStorage.removeItem('payment');
  }

  public getPaymentDetails() {
    const paymentDetails = localStorage.getItem('payment')
    if (paymentDetails) {
      return JSON.parse(paymentDetails) as PaymentMethod;
    }
    return null;
  }

  public completeOrder(order: OrderAddRequest) {
    return this.orderService.addProduct(order);
  }

}
