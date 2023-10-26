import { Component, OnInit } from '@angular/core';
import { PaymentMethod, UserPayoutResponse } from '../../../_models/_settings/response/settings-response';
import { SettingsService } from '../../../_services/settings.service';

@Component({
  selector: 'app-settings-payouts',
  templateUrl: './settings-payouts.component.html',
  styleUrls: ['./settings-payouts.component.css']
})
export class SettingsPayoutsComponent implements OnInit {
  numberOfPayouts: number
  itemsPerPage: number = 5;
  currentPage: number = 1
  maxSizePagination: number = 5
  balance: number = -1;
  payouts: UserPayoutResponse[]
  paymentMethod: PaymentMethod

  constructor(private settingsService: SettingsService) { }
  ngOnInit(): void {
    this.getPayouts()
  }

  getPayouts() {
    this.settingsService.getPayouts(this.currentPage).subscribe({
      next: (response => {
        this.numberOfPayouts = response.totalEntities;
        this.balance = response.balance;
        this.payouts = response.payouts;
        this.paymentMethod = response.paymentMethod
      }),
      error: (
        error => {
          console.log(error.error.message)
        })
    })
  }

  payoutsPageChanged(event) {
    this.currentPage = event.page;
    this.getPayouts()
  }
  withdraw() {
    this.settingsService.withdrawal().subscribe({
      next: (response => {
        this.getPayouts()
      }),
      error: (
        error => {
          console.log(error.error.message)
        })
    });
  }
}
