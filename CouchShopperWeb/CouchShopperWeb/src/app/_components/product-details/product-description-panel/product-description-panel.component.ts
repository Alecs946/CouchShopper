import { Component, Input } from '@angular/core';
import { ProductDescription } from '../../../_models/_product/response/product-response';

@Component({
  selector: 'app-product-description-panel',
  templateUrl: './product-description-panel.component.html',
  styleUrls: ['./product-description-panel.component.css']
})
export class ProductDescriptionPanelComponent {
  @Input() description!:ProductDescription ;
  isCollapsed: boolean = false;
}
