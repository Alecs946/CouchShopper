import { Component, Input } from '@angular/core';
import { Product } from '../../_models/_product/request/product-request';

@Component({
  selector: 'app-product-grid',
  templateUrl: './product-grid.component.html',
  styleUrls: ['./product-grid.component.css']
})
export class ProductGridComponent {
  @Input() products!: Product[];
  @Input() columns: number = 0;
  @Input() rows: number = 0;

  getGridColumnClass() {
    const gridClass = `col-lg-${12 / this.columns} col-md-${12 / this.columns} col-sm-${12 / this.columns}`;
    return gridClass;
  }
  getGridRowStyle() {
    const gridRowStyle = {
      'grid-template-rows': `repeat(${this.rows}, 1fr)`
    };
    return gridRowStyle;
  }
}
