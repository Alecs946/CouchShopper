import { Component, Input} from '@angular/core';

@Component({
  selector: 'app-cart-aside',
  templateUrl: './cart-aside.component.html',
  styleUrls: ['./cart-aside.component.css']
})
export class CartAsideComponent {
  @Input() subtotal!: number;
  promoCodeCollapsed: boolean = false;
}
