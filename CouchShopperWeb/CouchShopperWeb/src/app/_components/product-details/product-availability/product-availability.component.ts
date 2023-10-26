import { Component, Input } from '@angular/core';

@Component({
  selector: 'app-product-availability',
  templateUrl: './product-availability.component.html',
  styleUrls: ['./product-availability.component.css']
})
export class ProductAvailabilityComponent {
  @Input() numberOfAvailableProducts?: number;
}
