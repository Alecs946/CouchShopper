import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { AccountService } from '../../../_services/account.service';

@Component({
  selector: 'app-account-activate',
  templateUrl: './account-activate.component.html',
  styleUrls: ['./account-activate.component.css']
})
export class AccountActivateComponent {
  @Output() formSubmitted: EventEmitter<boolean> = new EventEmitter<boolean>();
  activationForm!: FormGroup;
  isFormSubmitted = false;
  constructor(private formBuilder: FormBuilder, private accountService: AccountService) {
    this.activationForm = this.formBuilder.group({
      activationCode: ['', Validators.required],
    });
  }

  activateAccount() {
    if (this.activationForm.valid) {
      this.isFormSubmitted = false;
      this.accountService.activeAccount(this.activationForm.value).subscribe({
        next: (() => {
          this.accountService.loginAfterAcctivation().subscribe({
            next: (() => {
              this.formSubmitted.emit(true)
            })
          })
          this.formSubmitted.emit(false)
        }),
      })
    }
    else {
      this.isFormSubmitted = true;
    }
  }
}
