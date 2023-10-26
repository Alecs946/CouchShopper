import { Component, EventEmitter, Input, Output } from '@angular/core';

@Component({
  selector: 'app-checkout-steps',
  templateUrl: './checkout-steps.component.html',
  styleUrls: ['./checkout-steps.component.css']
})
export class CheckoutStepsComponent {
  //@Input() currentStep!: number;
  @Output() stepChanged: EventEmitter<number> = new EventEmitter<number>();
  
  @Input() currentStep: number = 0;

  goToStep(stepIndex: number): void {
    this.currentStep = stepIndex;
    this.stepChanged.emit(stepIndex)
  }
}
