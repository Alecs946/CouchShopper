import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { AbstractControl, FormBuilder, FormGroup, ValidatorFn, Validators } from '@angular/forms';
import { PaypalPaymentMethod, CardPaymentRequest } from '../../../_models/_settings/request/settings-request';
import {  PaymentMethod } from '../../../_models/_settings/response/settings-response';
import { SettingsService } from '../../../_services/settings.service';

@Component({
  selector: 'app-settings-account-payment',
  templateUrl: './settings-account-payment.component.html',
  styleUrls: ['./settings-account-payment.component.css']
})
export class SettingsAccountPaymentComponent {
  @Output() formSubmitted: EventEmitter<boolean> = new EventEmitter<boolean>();
  isCreditDebitCardPaymentSelected: boolean = true;
  isPayPalAccountSelected: boolean = false;
  cardForm: FormGroup;
  paymentModel!: PaymentMethod;
  paypalForm!: FormGroup;
  selectedPaymentType: string = "creditDebitCard"
  paymentFormSubmitted: boolean = false;

  constructor(private formBuilder: FormBuilder, public settingsService: SettingsService) {
    this.cardForm = this.formBuilder.group({
      cardNumber: ['', Validators.required],
      nameOnCard: ['', Validators.required],
      expiryDate: ['', [Validators.required, Validators.pattern(/^\d{2}\/\d{2}$/), this.validCard()]],
      cardName: [null, Validators.required],
      isPrimary: [false]
    });
    this.paypalForm = this.formBuilder.group({
      email: ['', [Validators.required, Validators.email]],
      password: ['', Validators.required],
      isPrimary: [false]
    });
  }

  paymentMethodChanged() {
    if (this.selectedPaymentType === 'creditDebitCard') {
      this.isCreditDebitCardPaymentSelected = true;
      this.isPayPalAccountSelected = false;
    }
    else if (this.selectedPaymentType === 'paypalAccount') {
      this.isCreditDebitCardPaymentSelected = false;
      this.isPayPalAccountSelected = true;
    }
  }


  submitCardForm() {
    if (this.cardForm.valid) {
      this.paymentFormSubmitted = false
      this.settingsService.addCard(this.cardForm.value).subscribe({
        next: (response => {
          this.cardForm.reset();
          this.formSubmitted.emit(true);
        }),
        error: (error => {
          console.log(error.error.message);
        })
      })
    }
    else {
      this.paymentFormSubmitted = true;
    }
  }
  submitPayPalForm() {
    if (this.paypalForm.valid) {
      this.settingsService.addPaypal(this.paypalForm.value).subscribe({
        next: (response => {
          this, this.paypalForm.reset();
          this.formSubmitted.emit(true);
        }),
        error: (error => {
          console.log(error.error.message);
        })
      })
    }
  }
  validCard(): ValidatorFn {
    return (control: AbstractControl): { [key: string]: any } | null => {
      if (!control || !control.value) {
        return null; 
      }

      const inputValue: string = control.value;
      const inputParts: string[] = inputValue.split('/');

      if (inputParts.length !== 2) {
        return { invalidFormat: true };
      }

      const inputMonth: number = parseInt(inputParts[0], 10);
      const inputYear: number = parseInt(inputParts[1], 10);
      if (
        isNaN(inputMonth) ||
        isNaN(inputYear) ||
        inputMonth < 1 ||
        inputMonth > 12 ||
        inputYear < 0 ||
        inputYear > 99
      ) {
        return { invalidValues: true };
      }
      const currentDate = new Date();
      const currentYear = currentDate.getFullYear() % 100; 
      const currentMonth = currentDate.getMonth() + 1; 

      if (
        inputYear < currentYear ||
        (inputYear === currentYear && inputMonth < currentMonth)
      ) {
        return { cardExpired: true }; 
      }

      return null; 
    };
  }
}
