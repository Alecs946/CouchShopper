import { Component, Input, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Product } from '../../../_models/_product/request/product-request';

@Component({
  selector: 'app-featured-products',
  templateUrl: './featured-products.component.html',
  styleUrls: ['./featured-products.component.css']
})
export class FeaturedProductsComponent implements OnInit {
  
  @Input() products: Product[] = [];
  @Input() numberOfProducts: number = 1;

  constructor(private http: HttpClient) { }

  ngOnInit() {
  }
  

 
}
