import { Component, Input, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Product } from '../../_models/_product/response/product-response';
import { ProductService } from '../../_services/product.service';

@Component({
  selector: 'app-search-resault',
  templateUrl: './search-resault.component.html',
  styleUrls: ['./search-resault.component.css']
})
export class SearchResaultComponent implements OnInit {
  products: Product[] = [];
  showCategories: boolean = false;
  numberOfResaults: number
  itemsPerPage: number = 5;
  currentPage: number = 1
  maxSizePagination: number = 5
  private getScreenWidth!: number;
  constructor(private route: ActivatedRoute, private producService: ProductService) { }
  private _searchPhrase: string;
  private _searchCategory: string;
  @Input() set searchPhrase(value: string) {
    if (value !== this._searchPhrase) {
      this._searchPhrase = value;
      this.getProducts()
    }
  }
  @Input() set searchCategory(value: string) {
    if (value !== this._searchCategory) {
      this._searchCategory = value;
      this.getProducts()
    }
  }

  ngOnInit(): void {
    this.getScreenWidth = window.innerWidth;
    this.route.params.subscribe(params => {
      this.searchPhrase = params['searchPhrase'];
      this.searchCategory = params['searchCategory']
    });
    if (this.searchPhrase) {
      this.getProducts()
    }
  }
  getProducts() {

    this.producService.getSearchProducts(this._searchPhrase, this._searchCategory, this.currentPage).subscribe({
      next: (response => {
        this.numberOfResaults = response.totalEntities
        this.products = response.products.map((x) => {
          return {
            id: x.id,
            name: x.name,
            imageBase64: x.photo.content,
            numberOfSales: x.numberOfSales,
            price: x.price,
            rating: 1,
          }
        })
      }),
      error: (
        error => {
          console.log(error.error.message)
        })
    })
  }

  GetNumberOfProducts() {
    if (this.products.length < 2) {
      return 1
    }
    if (this.products.length == 2) {
      return 2
    }
    if (this.getScreenWidth < 910) {
      console.log(1)
      return 1;
    }
    if (this.getScreenWidth < 1440) {
      return 2;
    }
    if (this.getScreenWidth < 1850) {
      return 2;
    }
    return 3;
  }

  pageChanged(event) {
    this.currentPage = event.page;
    this.getProducts()
  }
}
