import { Component, OnInit } from '@angular/core';
import { ProductsByUserResponse } from '../../../_models/_product/response/product-response';
import { SettingsService } from '../../../_services/settings.service';

@Component({
  selector: 'app-settings-products',
  templateUrl: './settings-products.component.html',
  styleUrls: ['./settings-products.component.css']
})
export class SettingsProductsComponent implements OnInit{
  constructor(public settings: SettingsService) { }
    ngOnInit(): void {
        this.getProducts()
    }
  
  currentPage = 1;
  itemsPerPage = 5;
  maxSizePagination = 5;
  numberOfProducts = 0;
  products: ProductsByUserResponse[] = [];
  getProducts(): void {
    this.settings.usersProduct(this.currentPage)
      .subscribe(
        {
          next: (response => {
            this.numberOfProducts = response.totalEntities;
            this.products = response.products;
          }),
          error: (
            error => {
              console.log(error.error.message)
            })
        }
      );
  }

  productPageChanged($event: any): void {
    this.currentPage = $event.page;
    this.getProducts();
  }

  deleteProduct(productId: string) {
    this.settings.deleteProduct(productId).subscribe(
      {
        next: (() => {
          this.getProducts()
        }),
        error: (
          error => {
            console.log(error.error.message)
          })
      }
    );
  }
}
