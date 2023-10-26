import { Component, Input } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { Observable } from 'rxjs';
import { LoginResponse } from '../../../_models/Account/response/user-response';
import { ProductService } from '../../../_services/product.service';

@Component({
  selector: 'app-product-review',
  templateUrl: './product-review.component.html',
  styleUrls: ['./product-review.component.css']
})
export class ProductReviewComponent {
  @Input() rating!: number;
  @Input() numberOfReviews!: number;
  @Input() productId!: string;
  currentUser$: Observable<LoginResponse>
  constructor(private tostr: ToastrService, private productService: ProductService) {
    this.currentUser$ = productService.isAutorized()
}

  addToFavorites() {
    this.productService.addToFavorites(this.productId).subscribe({
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
