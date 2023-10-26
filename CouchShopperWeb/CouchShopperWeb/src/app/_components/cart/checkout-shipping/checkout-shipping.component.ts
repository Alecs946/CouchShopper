import { Component, EventEmitter, Input, OnInit, Output, ViewChild } from '@angular/core';
import { CartProduct } from '../../../_models/_cart/response/cart-response';
import { Shipping } from '../../../_models/_product/response/product-response';
import { ShoppingCartService } from '../../../_services/shopping-cart.service';
import { CheckoutAsideComponent } from '../checkout-aside/checkout-aside.component';

@Component({
  selector: 'app-checkout-shipping',
  templateUrl: './checkout-shipping.component.html',
  styleUrls: ['./checkout-shipping.component.css']
})
export class CheckoutShippingComponent implements OnInit {
  @Input() products!: CartProduct[];
  @Output() stepChanged: EventEmitter<number> = new EventEmitter<number>();
  shippingCountry!: string;
  shippingMethods!: Shipping[];
  shipping!: Shipping;
  @ViewChild('aside') childComponent!: CheckoutAsideComponent;
  constructor(private shoppingCart: ShoppingCartService) { }
  ngOnInit(): void {
    this.getShippingMethods();
  }

  methodChanged(method: Shipping) {
    this.shoppingCart.addShippingDetails(method)
    this.childComponent.shippingPriceChanged(method)
  }

  getShippingMethods() {
    this.shoppingCart.getShippingOptions(this.shoppingCart.getCheckoutDetails().country).subscribe(
      {
        next: (response => {
          this.shippingMethods = response.map(x => {
            return {
              estimatedTime: x.minNumberOfDays + '-' + x.maxNumberOfDays,
              shippingMethodName: x.shippingMethodName,
              shippingPrice: x.shippingPrice
            }
          })
          this.shipping = this.shoppingCart.getShippingDetails()
          if (!this.shipping) {
            this.shipping = this.shippingMethods[0];
            this.shoppingCart.addShippingDetails(this.shipping)
          }
            
        }),
        error: (
          error => {
            console.log(error.error.message)
          })
      })

  }

  proceed() {
    this.stepChanged.emit(this.shoppingCart.getCurentStep() + 1)
  }

  back() {
    this.stepChanged.emit(this.shoppingCart.getCurentStep() - 1)
  }
}
