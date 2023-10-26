import { Component, OnInit } from '@angular/core';
import { OrdersOverviewResponse } from '../../../../../_models/_order/response/order-response';
import { OrderService } from '../../../../../_services/order.service';

@Component({
  selector: 'app-order-delivered-orders',
  templateUrl: './order-delivered-orders.component.html',
  styleUrls: ['./order-delivered-orders.component.css']
})
export class OrderDeliveredOrdersComponent implements OnInit {
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

    this.orderService.getDeliveredOrders(this.currentPage).subscribe({
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
  
}
