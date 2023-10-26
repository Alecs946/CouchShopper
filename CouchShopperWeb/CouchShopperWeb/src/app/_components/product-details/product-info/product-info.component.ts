import { Component, Input, OnInit } from '@angular/core';
import { CartItem } from '../../../_models/_cart/request/cart-request';
import { ProductDescription, ProductDetails, Shipping } from '../../../_models/_product/response/product-response';
import { SellerInfo } from '../../../_models/Account/response/seller-response';
import { ShoppingCartService } from '../../../_services/shopping-cart.service';
import { LoginResponse } from '../../../_models/Account/response/user-response';
import { Observable } from 'rxjs';
import { ProductService } from '../../../_services/product.service';

@Component({
  selector: 'app-product-info',
  templateUrl: './product-info.component.html',
  styleUrls: ['./product-info.component.css']
})
export class ProductInfoComponent implements OnInit {
  @Input() productDetails!: ProductDetails;
  @Input()  productDescription: ProductDescription;
  @Input() shippingOptions: Shipping[];
  @Input() sellerInfo: SellerInfo;
  @Input() id: string;
  selectedOptions: { optionName: string, optionValue: string }[] = []
  quantity: number = 1;
  currentUser$: Observable<LoginResponse>

  constructor(private shoppingCartService: ShoppingCartService, private productService: ProductService)
  {
    this.currentUser$ = productService.isAutorized()
  }
  ngOnInit(): void {
    this.productDetails.productOptions.forEach((item => {
      this.selectedOptions.push({ optionName: item.optionName, optionValue: item.optionValues[0].name })
    }))
    }

  addToCartClick() {
    const productItem: CartItem = {
      productId: this.id,
      selectedOption: this.selectedOptions.map((x => {
        return {
          optionName: x.optionName,
          optionValue: x.optionValue
        }
      })),
      quantity: this.quantity
    }
    this.shoppingCartService.addToCart(productItem);
  }
  optionChanged(event: { index: number, optionValue: string }) {
    this.selectedOptions[event.index].optionValue = event.optionValue
  }
}
