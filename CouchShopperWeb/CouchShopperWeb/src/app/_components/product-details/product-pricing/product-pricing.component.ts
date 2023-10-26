import { Component, Input } from '@angular/core';

@Component({
  selector: 'app-product-pricing',
  templateUrl: './product-pricing.component.html',
  styleUrls: ['./product-pricing.component.css']
})
export class ProductPricingComponent {
  @Input() price!: number;
  @Input() salePrice?: number;
}
