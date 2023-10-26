import { Component, OnInit } from '@angular/core';
import { number } from 'echarts';
import { OrderOverviewResponse, OrdersOverviewResponse } from '../../../../../_models/_order/response/order-response';
import { OrderService } from '../../../../../_services/order.service';

@Component({
  selector: 'app-order-approved-orders',
  templateUrl: './order-approved-orders.component.html',
  styleUrls: ['./order-approved-orders.component.css']
})
export class OrderApprovedOrdersComponent implements OnInit {
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
    this.orderService.getApprovedOrders(this.currentPage).subscribe({
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
  sentOrder(order: OrderOverviewResponse) {
    this.orderService.sendOrder({ orderId: order.orderId }).subscribe(
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
