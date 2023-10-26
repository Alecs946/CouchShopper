import { Component, Input, OnInit } from '@angular/core';
import { ProductDescription, Shipping } from '../../../_models/_product/response/product-response';

@Component({
  selector: 'app-product-panels',
  templateUrl: './product-panels.component.html',
  styleUrls: ['./product-panels.component.css']
})
export class ProductPanelsComponent {
  //@Input() productPanel!: ProductPanel;
  @Input() productDescription: ProductDescription;
  @Input() shippingOptions: Shipping[];
  
}
