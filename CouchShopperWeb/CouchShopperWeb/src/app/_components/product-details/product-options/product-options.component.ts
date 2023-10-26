import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { ProductOption } from '../../../_models/_product/request/product-request';

@Component({
  selector: 'app-product-options',
  templateUrl: './product-options.component.html',
  styleUrls: ['./product-options.component.css']
})
export class ProductOptionsComponent implements OnInit {
  @Input() productOption!: ProductOption;
  @Input() index!: number;
  selectedOption: string = '';
  @Output() valueChanged: EventEmitter<{ index: number, optionValue: string }> = new EventEmitter<{ index: number, optionValue: string }>();
  ngOnInit() {
    if (this.productOption.optionValues && this.productOption.optionValues.length > 0) {
      this.selectedOption = this.productOption.optionValues[0].name; 
    }
  }
  onOptionChange() {
    this.valueChanged.emit({ index: this.index, optionValue: this.selectedOption })
  }
}
