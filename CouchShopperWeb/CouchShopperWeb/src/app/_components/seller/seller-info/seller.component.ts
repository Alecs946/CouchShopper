import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Product } from '../../../_models/_product/response/product-response';
import { Seller } from '../../../_models/Account/response/seller-response';
import { AccountService } from '../../../_services/account.service';

@Component({
  selector: 'app-seller',
  templateUrl: './seller.component.html',
  styleUrls: ['./seller.component.css']
})
export class SellerComponent implements OnInit {
  id!: string
  seller!: Seller;
  numberOfProducts: number
  itemsPerPage: number = 5;
  currentPage: number = 1
  maxSizePagination: number = 5
  products: Product[]
  constructor(private route: ActivatedRoute ,private accountServce: AccountService) { }
  ngOnInit(): void {
    this.route.params.subscribe(params => {
      this.id = params['id'];
    });
    this.getProducts()
  }
  getProducts() {
    this.accountServce.getSellerDetails(this.id,this.currentPage).subscribe({
      next: (response => {
        this.seller = response
        this.numberOfProducts = response.products.totalEntities
        this.products = response.products.products.map(x => {
          return {
            id: x.id,
            imageBase64: x.photo.content,
            name: x.name,
            numberOfSales: x.numberOfSales,
            price: x.price,
            rating: 0
          }
        })
      }),
      error: (
        error => {
          console.log(error.error.message)
        })
    })
  }

  GetImageToBase64(fileName: string) {
    return './assets/products/' + fileName
  }

  productsPageChanged(event) {
    this.currentPage = event.page;
    this.getProducts();
  }
}


