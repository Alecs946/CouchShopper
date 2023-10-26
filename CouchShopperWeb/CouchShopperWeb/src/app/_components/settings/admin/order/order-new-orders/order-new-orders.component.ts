import { Component, OnInit } from '@angular/core';
import { OrderItemResponse, OrderOverviewResponse, OrdersOverviewResponse } from '../../../../../_models/_order/response/order-response';
import { OrderService } from '../../../../../_services/order.service';

@Component({
  selector: 'app-order-new-orders',
  templateUrl: './order-new-orders.component.html',
  styleUrls: ['./order-new-orders.component.css']
})
export class OrderNewOrdersComponent implements OnInit {
  numberOfOrders: number
  itemsPerPage: number = 5;
  currentPage: number = 1
  maxSizePagination: number = 5

  orders: OrdersOverviewResponse;
  constructor(public orderService: OrderService) { }
  ngOnInit(): void {
    this.getOrders()
  }
  getOrders() {
    this.orderService.getNewOrders(this.currentPage).subscribe({
      next: (response => {
        this.numberOfOrders = response.totalEntities
        this.orders = response
      }),
      error: (
        error => {
          console.log(error.error.message)
        })
    })
  }

  ordersPageChanged(event) {
    this.currentPage = event.page;
    this.getOrders();
  }

  approveProduct(order: OrderOverviewResponse, product: any) {
    product.isApproved = true;
    product.isDeclined = false
    order.showConfirmation = order.orderItems.every(product => product.isApproved);
  }

  declineProduct(order: OrderOverviewResponse, product: OrderItemResponse) {
    product.isApproved = false;
    product.isDeclined = true;
    order.showReason = true;
  }

  confirmApproval(order: OrderOverviewResponse) {
    this.orderService.confirmOrder({orderId:order.orderId }).subscribe(
      {
        next: (() => {
          this.currentPage = 1;
          this.getOrders()
        }),
        error: (
          error => {
            console.log(error.error.message)
          })
      })
  }

  confirmDecline(order: OrderOverviewResponse) {
    this.orderService.declineOrder({ orderId: order.orderId, reason: order.declineReason }).subscribe(
      {
        next: (() => {
          this.currentPage = 1;
          this.getOrders()
        }),
        error: (
          error => {
            console.log(error.error.message)
          })
      })
  }
}
