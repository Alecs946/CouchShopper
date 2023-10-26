import { Component, Input, OnInit } from '@angular/core';
import { CartProduct } from '../../../_models/_cart/response/cart-response';
import { Shipping } from '../../../_models/_product/response/product-response';
import { ShoppingCartService } from '../../../_services/shopping-cart.service';

@Component({
  selector: 'app-checkout-aside',
  templateUrl: './checkout-aside.component.html',
  styleUrls: ['./checkout-aside.component.css']
})
export class CheckoutAsideComponent implements OnInit {
  
  @Input() products!: CartProduct[];
  @Input() subtotal!:number
  priceTotal!: number;
  total!: number;
  shipping!: Shipping;
  constructor(public shoppingCartService: ShoppingCartService) { }
  ngOnInit(): void {
    this.priceTotal = 0;
    this.total = 0;
    if (!this.subtotal) {

    this.products.forEach((product) => {
      this.priceTotal += (product.price*product.quantity);
    })
    }
    else {
      this.priceTotal = this.subtotal;
    }
    this.shipping = this.shoppingCartService.getShippingDetails()
    this.calculateTotal()
  }

  shippingPriceChanged(shipping: Shipping) {
    this.shipping = shipping;
    this.calculateTotal()
  }

  calculateTotal() {
    this.total = 0
    if (this.priceTotal) {
      this.total+=this.priceTotal
    }
    if (this.shipping) {
      this.total += this.shipping.shippingPrice;
    }
     
  }

}
