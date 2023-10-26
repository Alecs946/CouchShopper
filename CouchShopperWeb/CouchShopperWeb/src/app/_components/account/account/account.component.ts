import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { AbstractControl, FormBuilder, FormControl, FormGroup, ValidatorFn, Validators } from '@angular/forms';
import { AccountService } from '../../../_services/account.service';


@Component({
  selector: 'app-account',
  templateUrl: './account.component.html',
  styleUrls: ['./account.component.css']
})
export class AccountComponent {

  @Output() formSubmitted: EventEmitter<boolean> = new EventEmitter<boolean>();
  loginForm: FormGroup;
  loginFormSubmitted: boolean = false;
  registerForm: FormGroup;
  registerFormSubmitted: boolean = false;
  showLoginPassword: boolean = false;
  showRegisterPassword: boolean = false;
  showRegisterPasswordConfirm: boolean = false;

  constructor(private formBuilder: FormBuilder, private accountService: AccountService) {

    this.loginForm = this.formBuilder.group({
      username: ['', Validators.required],
      password: ['', Validators.required]
    });

    this.registerForm = this.formBuilder.group({
      fullName: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
      username: ['', [Validators.required, Validators.minLength(6)]],
      password: ['', [Validators.required, Validators.minLength(8)]],
      confirmPassword: ['', [Validators.required, this.matchValues("password")]],
      isSeller: [false]
    })
  }

  login() {
    if (this.loginForm.valid) {
      this.accountService.isActiveAccount(this.loginForm.value).subscribe(
        {
          next: (response => {
            if (response) {

              this.accountService.loginUser(this.loginForm.value).subscribe({
                next: (response => {
                  this.accountService.setCredentials(this.loginForm.value)
                  this.accountService.reloadProfilePicture()
                  this.formSubmitted.emit(false);
                })
              })
            }
            else {
              this.formSubmitted.emit(true);
            }
          })
        })
    }
    else {
      this.loginFormSubmitted = true;
    }
  }

  register() {
    if (this.registerForm.valid) {
      this.registerFormSubmitted = false
      this.accountService.registerUser(this.registerForm.value).subscribe({
        next: () => {
          this.accountService.setCredentials(this.registerForm.value)
          this.formSubmitted.emit(false);
        }
      })
    }
    else {
      this.registerFormSubmitted = true;
    }
  }

  matchValues(matchTo: string): ValidatorFn {
    return (control: AbstractControl) => {
      return control.value === control.parent?.get(matchTo)?.value ? null : { isMatching: true }
    }
  }
}

