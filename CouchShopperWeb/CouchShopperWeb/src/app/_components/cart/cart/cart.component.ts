import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { CartItem } from '../../../_models/_cart/request/cart-request';
import { CartProduct } from '../../../_models/_cart/response/cart-response';
import { ShoppingCartService } from '../../../_services/shopping-cart.service';

@Component({
  selector: 'app-cart',
  templateUrl: './cart.component.html',
  styleUrls: ['./cart.component.css']
})
export class CartComponent implements OnInit {
  @Input() products!: CartProduct[];
  @Input() subtotal!: number;
  @Output() stepChanged: EventEmitter<number> = new EventEmitter<number>();
  @Output() quantityChange: EventEmitter<{ newQuantity: number, product: CartItem }> = new EventEmitter<{ newQuantity: number, product: CartItem }>();
  @Output() productRemove: EventEmitter<CartItem> = new EventEmitter<CartItem>();
  constructor(private shoppingCart: ShoppingCartService) {

  }
  ngOnInit(): void {
  }
  proceed() {
    this.stepChanged.emit(this.shoppingCart.getCurentStep() + 1)
  }
  quantityChanged(quantity: number, product: CartProduct) {
    const item: CartItem = {
      productId: product.productId,
      quantity: product.quantity,
      selectedOption: product.selectedOptions,
    }
    this.quantityChange.emit({ newQuantity: quantity, product: item })
  }

  productRemoved(product: CartProduct) {
    const item: CartItem = {
      productId: product.productId,
      quantity: product.quantity,
      selectedOption: product.selectedOptions,
    }
    this.productRemove.emit(item)
  }
}
