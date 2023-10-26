import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Observable } from 'rxjs';
import { User } from '../../../_models/Account/User';
import { CartProduct, ShippingInfo } from '../../../_models/_cart/response/cart-response';
import { Dropdown } from '../../../_models/_common/response/home-content-response';
import { ShoppingCartService } from '../../../_services/shopping-cart.service';

@Component({
  selector: 'app-checkout-details',
  templateUrl: './checkout-details.component.html',
  styleUrls: ['./checkout-details.component.css']
})
export class CheckoutDetailsComponent implements OnInit {
  user!: User;
  shippingInfoForm!: FormGroup;
  shippingInfoModel!: ShippingInfo;
  @Input() products!: CartProduct[];
  @Input() subtotal!: number;
  @Output() stepChanged: EventEmitter<number> = new EventEmitter<number>();

  countries$!: Observable<Dropdown[]>
  cities$!: Observable<Dropdown[]>

  constructor(private formBuilder: FormBuilder, private shoppingCartService: ShoppingCartService) { }

  ngOnInit(): void {
    this.getCountries();
    this.getUserDetaisl();
  }
  saveShippingInfo() {
    this.shippingInfoForm.valueChanges.subscribe((formValue) => {
      this.shippingInfoModel = formValue;
    })
    this.shoppingCartService.addCheckoutDetails(this.shippingInfoForm.value as ShippingInfo)
    this.stepChanged.emit(this.shoppingCartService.getCurentStep() + 1)
  }

  back() {
    this.stepChanged.emit(this.shoppingCartService.getCurentStep() - 1)
  }

  getCountries() {
    this.countries$ = this.shoppingCartService.getCountryDropdown()
  }

  getCities(selectedCountry: string) {
    this.cities$ = this.shoppingCartService.getCityDropdown(selectedCountry)
  }

  onCountryChanged() {
    const selectedCountry = this.shippingInfoForm.get("country").value;
    this.getCities(selectedCountry)
  }

  getUserDetaisl() {
    this.shoppingCartService.getUserDetails().subscribe(
      {
        next: (response => {
          this.user = {
            fullName: response.fullName,
            username: response.username,
            email: response.email,
            imageBase64: ''
          }
          this.shoppingCartService.getUserPhoto().subscribe({
            next: (response => {
              this.user.imageBase64 = response;
            }),
            error: (
              error => {
                console.log(error.error.message)
              })
          })

          if (this.shoppingCartService.getCheckoutDetails()) {
            const info = this.shoppingCartService.getCheckoutDetails()
            this.shippingInfoModel = info;
          }
          else {
            this.shippingInfoModel = {
              fullName: response.fullName,
              address: response.address,
              city: response.city,
              country: response.country,
              emailAddress: response.email,
              phoneNumber: response.phone,
              zipCode: response.zipCode
            }
          }
          this.getCities(this.shippingInfoModel.country);
          this.shippingInfoForm = this.formBuilder.group({
            fullName: [this.shippingInfoModel.fullName, Validators.required],
            address: [this.shippingInfoModel.address, Validators.required],
            city: [this.shippingInfoModel.city, Validators.required],
            country: [this.shippingInfoModel.country, Validators.required],
            emailAddress: [this.shippingInfoModel.emailAddress, Validators.required],
            phoneNumber: [this.shippingInfoModel.phoneNumber, Validators.required],
            zipCode: [this.shippingInfoModel.zipCode, Validators.required],
          })
        }),
        error: (
          error => {
            console.log(error.error.message)
          })
      })
  }
}
