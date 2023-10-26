import { Component, Input } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { Observable } from 'rxjs';
import { LoginResponse } from '../../_models/Account/response/user-response';
import { Product } from '../../_models/_product/request/product-request';
import { ProductService } from '../../_services/product.service';

@Component({
  selector: 'app-product',
  templateUrl: './product.component.html',
  styleUrls: ['./product.component.css']
})
export class ProductComponent {
  max = 5;
  @Input() product!: Product;
  currentUser$: Observable<LoginResponse>
  constructor(private tostr:ToastrService,private productService: ProductService) {
    this.currentUser$ = productService.isAutorized()
  }

  wishlistClick(productId: string) {
    this.productService.addToFavorites(productId).subscribe({
      next: (response => {
        this.tostr.success(response)
      }),
      error: (
        error => {
          this.tostr.error(error)
        })
    });
  }


}
