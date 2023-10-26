import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { CartProduct } from '../../../_models/_cart/response/cart-response';
import { Shipping } from '../../../_models/_product/response/product-response';
import { PaymentMethod } from '../../../_models/_settings/response/settings-response';
import { ShoppingCartService } from '../../../_services/shopping-cart.service';

@Component({
  selector: 'app-checkout-payment',
  templateUrl: './checkout-payment.component.html',
  styleUrls: ['./checkout-payment.component.css']
})
export class CheckoutPaymentComponent implements OnInit {
  @Input() products!: CartProduct[];
  @Output() stepChanged: EventEmitter<number> = new EventEmitter<number>();
  cardCollapsed: boolean = false;
  payPalCollapsed: boolean = true;
  @Input() shipping!: Shipping;
  savedPaymentMethod: PaymentMethod;
  paymentMethods$ = this.shoppingCart.getPaymentMethods();
  addNewPayment: boolean = false;
  constructor(private shoppingCart: ShoppingCartService) { }

  ngOnInit(): void {

    this.savedPaymentMethod = this.shoppingCart.getPaymentDetails();
    if (!this.savedPaymentMethod) {
      this.paymentMethods$.subscribe(
        {
          next: (response => {
            this.savedPaymentMethod = response.find(x => x.isPrimary);
            this.shoppingCart.addPaymentDetails(this.savedPaymentMethod);
          })
        })

    }
  }

  onPaymentMethodSelected(paymentMethod: PaymentMethod) {
    this.savedPaymentMethod = paymentMethod;
    this.shoppingCart.addPaymentDetails(this.savedPaymentMethod)
  }

  paymentAdded(event) {
    this.addNewPayment = false;
    this.paymentMethods$ = this.shoppingCart.getPaymentMethods();
  }

  proceed() {
    this.shoppingCart.addPaymentDetails(this.savedPaymentMethod)
    this.stepChanged.emit(this.shoppingCart.getCurentStep() + 1)
  }

  back() {
    this.stepChanged.emit(this.shoppingCart.getCurentStep() - 1)
  }
}
