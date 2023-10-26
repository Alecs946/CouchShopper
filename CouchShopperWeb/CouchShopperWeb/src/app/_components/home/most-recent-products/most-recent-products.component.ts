import { Component, Input } from '@angular/core';
import { DropDownItem } from '../../../_models/_common/response/home-content-response';
import { Product } from '../../../_models/_product/request/product-request';

@Component({
  selector: 'app-most-recent-products',
  templateUrl: './most-recent-products.component.html',
  styleUrls: ['./most-recent-products.component.css']
})
export class MostRecentProductsComponent {
  @Input() categories: DropDownItem[] = []
  @Input() products: Product[];
  @Input() showCategories: boolean = false;
  @Input() columns: number = 0;
  @Input() rows: number = 0;
}
