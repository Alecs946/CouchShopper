import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { OrderAddRequest, OrderStatusChangeRequest, OrderDeclineRequest } from '../_models/_order/request/order-request';
import { OrdersOverviewResponse, OrderUserOrdersListResponse } from '../_models/_order/response/order-response';
import { NewProductReview } from '../_models/_product/request/product-request';
import { AccountService } from './account.service';
import { ApiEndpointService } from './api-endpoint.service';

@Injectable({
  providedIn: 'root'
})
export class OrderService {
  constructor(private http: HttpClient, private apiBase: ApiEndpointService, private accountService: AccountService) { }

  public addProduct(order: OrderAddRequest) {
    order.buyerUsername = this.accountService.getUsername();
    return this.http.post<OrderAddRequest>(this.apiBase.OrderAddOrder, order);
  }

  public getNewOrders(page) {
    const params = new HttpParams().append('page', page);
    return this.http.get<OrdersOverviewResponse>(this.apiBase.OrderGetNewOrders, { params });
  }

  public getDeclinedOrders(page) {
    const params = new HttpParams().append('page', page);
    return this.http.get<OrdersOverviewResponse>(this.apiBase.OrderGetDeclinedOrders, { params });
  }

  public getApprovedOrders(page) {
    const params = new HttpParams().append('page', page);
    return this.http.get<OrdersOverviewResponse>(this.apiBase.OrderGetApprovedOrders, { params });
  }

  public getSentOrders(page) {
    const params = new HttpParams().append('page', page);
    return this.http.get<OrdersOverviewResponse>(this.apiBase.OrderGetSentOrders, { params });
  }

  public getDeliveredOrders(page) {
    const params = new HttpParams().append('page', page);
    return this.http.get<OrdersOverviewResponse>(this.apiBase.OrderGetDeliveredOrders, { params });
  }

  public confirmOrder(product: OrderStatusChangeRequest) {
    return this.http.put(this.apiBase.OrderConfirmOrder, product);
  }

  public declineOrder(reson: OrderDeclineRequest) {
    return this.http.put(this.apiBase.OrderDeclineOrder, reson);
  }

  public sendOrder(reson: OrderStatusChangeRequest) {
    return this.http.put(this.apiBase.OrderSentOrder, reson);
  }

  public deliverOrder(reson: OrderStatusChangeRequest) {
    return this.http.put(this.apiBase.OrderDeliveredOrder, reson);
  }

  public getUserOrders(page: number) {
    const params = new HttpParams().append('userId', this.accountService.getUsername()).append('page', page);
    return this.http.get<OrderUserOrdersListResponse>(this.apiBase.OrderUserOrders, { params });
  }

  public orderRating(rating: NewProductReview) {
    rating.userId = this.accountService.getUsername();
    return this.http.put<OrderUserOrdersListResponse>(this.apiBase.OrderRateProduct, rating);
  }

  public getActiveOrdersCount() {
    const params = new HttpParams().append('userId', this.accountService.getUsername());
    return this.http.get<number>(this.apiBase.OrderActiveOrdersCount, { params });
  }


}
