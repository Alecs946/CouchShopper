import { Component, OnInit } from '@angular/core';
import { OrderOverviewResponse, OrdersOverviewResponse } from '../../../../../_models/_order/response/order-response';
import { OrderService } from '../../../../../_services/order.service';

@Component({
  selector: 'app-order-sent-orders',
  templateUrl: './order-sent-orders.component.html',
  styleUrls: ['./order-sent-orders.component.css']
})
export class OrderSentOrdersComponent implements OnInit {
  orders: OrdersOverviewResponse;
  numberOfOrders: number
  itemsPerPage: number = 5;
  currentPage: number = 1
  maxSizePagination: number = 5

  constructor(public orderService: OrderService) { }

  ngOnInit(): void {
    this.getOrders()
  }

  getOrders() {
    this.orderService.getSentOrders(this.currentPage).subscribe({
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
  deliverOrder(order: OrderOverviewResponse) {
    this.orderService.deliverOrder({ orderId: order.orderId }).subscribe(
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
