import { Component, OnInit, ViewChild } from '@angular/core';
import { UserOrdersResponse } from '../../../_models/_order/response/order-response';
import { NewProductReview } from '../../../_models/_product/request/product-request';
import { SettingsService } from '../../../_services/settings.service';

@Component({
  selector: 'app-settings-purchases',
  templateUrl: './settings-purchases.component.html',
  styleUrls: ['./settings-purchases.component.css']
})
export class SettingsPurchasesComponent implements OnInit {
  constructor(private settingsService: SettingsService) { }
  @ViewChild('rate', { static: true }) rateTemplate: any;
  numberOfOrders: number
  itemsPerPage: number = 5;
  currentPage: number = 1
  maxSizePagination: number = 5
  purchases: UserOrdersResponse[];
  newComment: NewProductReview =
    {
      productRating: 0,
      productComment: '',
      sellerRating: 0,
    }
  ngOnInit(): void {
    this.getPurchases()
  }
  getPurchases() {
    this.settingsService.getUserOrders(this.currentPage).subscribe({
      next: (response => {
        this.numberOfOrders = response.totalEntities
        this.purchases = response.orders
      }),
      error: (
        error => {
          console.log(error.error.message)
        })
    });
  }

  purchasesPageChanged(event) {
    this.currentPage = event.page;
    this.getPurchases()
  }
  addComment() {
    this.settingsService.rateProduct(this.newComment).subscribe({
      next: (() => {
        this.settingsService.modelClose();
        this.getPurchases();
      }),
      error: (
        error => {
          console.log(error.error.message)
        })
    })
  }

  openModal(id: string, orderId: string, sellerId: string) {
    this.newComment.itemId = id;
    this.newComment.sellerId = sellerId
    this.newComment.orderId = orderId
    this.settingsService.ratingModalOpen(this.rateTemplate);
  }

}
