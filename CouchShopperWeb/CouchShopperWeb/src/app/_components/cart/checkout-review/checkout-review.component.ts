import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { Router } from '@angular/router';
import { CartProduct, ShippingInfo } from '../../../_models/_cart/response/cart-response';
import { Shipping } from '../../../_models/_product/response/product-response';
import { PaymentMethod } from '../../../_models/_settings/response/settings-response';
import { ShoppingCartService } from '../../../_services/shopping-cart.service';

@Component({
  selector: 'app-checkout-review',
  templateUrl: './checkout-review.component.html',
  styleUrls: ['./checkout-review.component.css']
})
export class CheckoutReviewComponent implements OnInit {
  @Input() products!: CartProduct[];
  @Output() stepChanged: EventEmitter<number> = new EventEmitter<number>();
  @Output() orderComplete: EventEmitter<boolean> = new EventEmitter<boolean>();
  shippingInfo!: ShippingInfo;
  paymentMethod!:PaymentMethod
  paymentMethodName!: string;
  paymentMethodValue!: string;
  shipping!: Shipping;
  total!: number;
  @Input() subtotal!: number;

  constructor(private shoppingCart: ShoppingCartService, private router: Router) { }

  ngOnInit(): void {
    this.shippingInfo = this.shoppingCart.getCheckoutDetails();
    this.paymentMethod=this.shoppingCart.getPaymentDetails()
    this.shipping = this.shoppingCart.getShippingDetails()
    this.total = 0;
    this.calculateTotal()
  }

  calculateTotal() {
    this.total = this.subtotal + this.shipping.shippingPrice;
  }

  proceed() {
    this.orderComplete.emit(true);
  }

  back() {
    this.stepChanged.emit(this.shoppingCart.getCurentStep() - 1)
  }
}
