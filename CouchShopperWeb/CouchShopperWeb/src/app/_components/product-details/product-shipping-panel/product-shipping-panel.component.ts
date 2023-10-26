import { Component, Input } from '@angular/core';
import { Shipping } from '../../../_models/_product/response/product-response';

@Component({
  selector: 'app-product-shipping-panel',
  templateUrl: './product-shipping-panel.component.html',
  styleUrls: ['./product-shipping-panel.component.css']
})
export class ProductShippingPanelComponent {
  @Input() shipping!: Shipping[];
  isCollapsed: boolean = true;
}
