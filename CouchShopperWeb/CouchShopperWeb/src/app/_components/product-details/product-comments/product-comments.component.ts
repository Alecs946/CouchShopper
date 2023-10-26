import { Component, Input, OnInit } from '@angular/core';
import { ProductReview } from '../../../_models/_product/response/product-response';
import { ProductService } from '../../../_services/product.service';

@Component({
  selector: 'app-product-comments',
  templateUrl: './product-comments.component.html',
  styleUrls: ['./product-comments.component.css']
})
export class ProductCommentsComponent implements OnInit{
  @Input() productId: string
  comments?: ProductReview[];

  constructor(private productService: ProductService) { }

  currentPage = 1;
  number = 0;
  itemsPerPage = 5;
  maxSizePagination = 5;

  pageChanged($event) {
    this.currentPage = $event.page
    this.getReviews()
  }
  getReviews() {
    this.productService.getProductReviews(this.currentPage,this.productId).subscribe({
      next: (response => {
        this.number = response.totalEntities
        this.comments = response.reviews.map((x => {
          return {
            userId: x.userId,
            comment: x.comment,
            rating:x.rating
          }
        }))
      }),
      error: (
        error => {
          console.log(error.error.message)
        })
    })
  }


  ngOnInit(): void {
    this.getReviews()
  }

}
