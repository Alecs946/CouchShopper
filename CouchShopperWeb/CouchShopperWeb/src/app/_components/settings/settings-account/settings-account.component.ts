import { Component, OnInit, ViewChild } from '@angular/core';
import { AbstractControl, FormBuilder, FormGroup, ValidatorFn, Validators } from '@angular/forms';
import { map, Observable } from 'rxjs';
import { Dropdown } from '../../../_models/_common/response/home-content-response';
import { PaymentMethod } from '../../../_models/_settings/response/settings-response';
import { SettingsService } from '../../../_services/settings.service';
import { SettingsAccountPaymentComponent } from '../settings-account-payment/settings-account-payment.component';

@Component({
  selector: 'app-settings-account',
  templateUrl: './settings-account.component.html',
  styleUrls: ['./settings-account.component.css']
})
export class SettingsAccountComponent implements OnInit {
  @ViewChild('paymentMethod') accountPaymentComponent!: SettingsAccountPaymentComponent;
  profileForm!: FormGroup;
  passwordForm!: FormGroup;
  defaultCountry = 'Select country';
  defaultCity = 'Select City';
  countries$!: Observable<Dropdown[]>
  cities$!: Observable<Dropdown[]>
  paymentMethods$!:Observable<PaymentMethod[]>
  profileFormSubmitted = false;
  passwordFormSubmitted = false;
  showCurrentPassword: boolean = false;
  showNewPassword: boolean = false;
  showNewConfirmPassword: boolean = false;
  constructor(private formBuilder: FormBuilder, public settingsService: SettingsService) {
    this.getCountries();
    this.getPaymentMethods()
    this.profileForm = this.formBuilder.group({
      username: ['', Validators.required],
      fullName: ['', Validators.required],
      email: ['', [Validators.required,Validators.email]],
      country: [this.defaultCountry, Validators.required],
      city: [this.defaultCity, Validators.required],
      address: ['', Validators.required],
      phone: ['', Validators.required],
      zipCode: ['', Validators.required],
    });
    this.passwordForm = this.formBuilder.group({
      currentPassword: ['', Validators.required],
      newPassword: ['', [Validators.required, Validators.minLength(8)]],
      confirmNewPassword: ['', [Validators.required, this.matchValues("newPassword")]],
    })

  }

  ngOnInit(): void {
    this.settingsService.getUserInfo().subscribe({
      next: (response => {
        this.profileForm.patchValue(response);
        this.getCities(this.profileForm.value.country)
      }),
      error: (error => {
        console.log(error.error.message);
      })
    });
  }

  getCountries() {
    this.countries$ = this.settingsService.getCountryDropdown()
  }

  getCities(selectedCountry: string) {
    this.cities$ = this.settingsService.getCityDropdown(selectedCountry)
  }

  onCountryChanged() {
    const selectedCountry = this.profileForm.value.country;
    this.profileForm.get('city')?.setValue(null);
    this.getCities(selectedCountry)
  }

  saveUserInfo() {
    if (this.profileForm.valid) {
      this.profileFormSubmitted = false;
      this.settingsService.updateDetails(this.profileForm.value).subscribe()
    }
    else {
      this.profileFormSubmitted = true;
    }
  }

  onFilesUploaded(files: File[]): void {
    let file = files[0];
    if (file) {
      const reader = new FileReader();
      reader.readAsDataURL(file);
      reader.onload = (event) => {
        if (event.target) {
          const base64Data = reader.result as string;
          this.settingsService.uploadProfilePicture(base64Data.split(",")[1]).subscribe({
            next: (response => {
              this.settingsService.reloadProfilePicture().subscribe()
            }),
            error: (error => {
              console.log(error.error.message);
            })
          });
        }
      };
    }
  }

  saveNewPassword() {
    if (this.passwordForm.valid) {
      this.passwordFormSubmitted = false
      this.settingsService.saveNewPassword(this.passwordForm.value).subscribe({
        next: (response => {
          this.passwordForm.reset()
        }),
          error: (error => {
            console.log(error.error.message);
          })
        })
    }
    else {
      this.passwordFormSubmitted = true;
    }
  }

  openModal() {
    this.settingsService.modalOpen(this.accountPaymentComponent);
  }

  getPaymentMethods() {
    this.paymentMethods$ = this.settingsService.getPaymentMethods();
  }

  paymentMethodSubmitted(event: boolean) {
    if (event) {
      this.settingsService.modelClose();
      this.getPaymentMethods();
    }
  }

  deletePayment(id: string) {
    this.settingsService.deletePaymentMethod(id).subscribe({
      next: (response => {
        this.getPaymentMethods();
      }),
      error: (error => {
        console.log(error.error.message);
      })
    })
  }

  updatePrimaryPaymentMethod(paymentId: string) {
    this.settingsService.updatePrimaryPaymentMethod(paymentId).subscribe({
      next: (response => {
        this.getPaymentMethods();
      }),
      error: (error => {
        console.log(error.error.message);
      })
    })
  }
  matchValues(matchTo: string): ValidatorFn {
    return (control: AbstractControl) => {
      return control.value === control.parent?.get(matchTo)?.value ? null : { isMatching: true }
    }
  }
}
