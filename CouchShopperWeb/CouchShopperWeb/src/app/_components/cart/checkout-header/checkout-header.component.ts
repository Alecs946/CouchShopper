import { Component, EventEmitter, Input, Output } from '@angular/core';

@Component({
  selector: 'app-checkout-header',
  templateUrl: './checkout-header.component.html',
  styleUrls: ['./checkout-header.component.css']
})
export class CheckoutHeaderComponent {
  
  @Input() title!: string;
  @Input() stepCount!: number;
  @Output() stepChanged: EventEmitter<number> = new EventEmitter<number>();

  goToStep(event: number): void {
    this.stepChanged.emit(event);
  }
}
