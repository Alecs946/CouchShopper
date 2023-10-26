import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { CartItem } from '../_models/_cart/request/cart-request';
import { ProductCartResponseList } from '../_models/_cart/response/cart-response';
import { ProductAddRequest, ProductUpdateRequest } from '../_models/_product/request/product-request';
import { ProductResponse, ProductByUserListResponse, ProductSearchResponseList } from '../_models/_product/response/product-response';
import { ProductReviewsListResponse } from '../_models/_product/response/product-response';
import { SellerInfo} from '../_models/Account/response/seller-response';
import { AccountService } from './account.service';
import { ApiEndpointService } from './api-endpoint.service';
import { CommonService } from './common.service';

@Injectable({
  providedIn: 'root'
})
export class ProductService {

  constructor(private http: HttpClient, private apiBase: ApiEndpointService,
    private commonService: CommonService, private accountService: AccountService) { }

  public addProduct(product: ProductAddRequest) {
    product.userId = this.accountService.getUsername();
    return this.http.post<ProductAddRequest>(this.apiBase.ProductAddProduct, product);
  }

  public isAutorized() {
    return this.accountService.currentUser$;
  }

  public updateProduct(product: ProductUpdateRequest, id: string) {
    product.userId = this.accountService.getUsername();
    product.id = id;
    return this.http.put<ProductAddRequest>(this.apiBase.ProductUpdateProduct, product);
  }

  public deleteProduct(productId: string) {
    const params = new HttpParams().append('productId', productId);
    return this.http.delete(this.apiBase.ProductDeleteProduct, { params });
  }

  public getProduct(productId): Observable<ProductResponse> {
    const params = new HttpParams().append('id', productId);
    return this.http.get<ProductResponse>(this.apiBase.ProductGetProduct, { params });
  }

  public getProductCount(): Observable<number> {
    const params = new HttpParams().append('userId', this.accountService.getUsername());
    return this.http.get<number>(this.apiBase.ProductGetProductCount, { params });
  }

  public getCategories() {
    return this.commonService.getCategoriesDropdown();
  }

  public getProductsPerUser(page: number): Observable<ProductByUserListResponse> {
    const params = new HttpParams().append('page', page).append('userId', this.accountService.getUsername());
    return this.http.get<ProductByUserListResponse>(this.apiBase.ProductGetProductsByUser, { params });
  }

  public getProductReviews(page: number, productId: string): Observable<ProductReviewsListResponse> {
    const params = new HttpParams().append('page', page).append('productId', productId);
    return this.http.get<ProductReviewsListResponse>(this.apiBase.ProductGetProductReviews, { params });
  }

  public getSellerInfo(sellerId:string): Observable<SellerInfo> {
    return this.accountService.getSellerInfo(sellerId)
  }

  public getShippingOptions() {
    return this.commonService.getShippingOptions()
  }

  public getCartItems(cartItems:CartItem[]) {
    return this.http.post<ProductCartResponseList>(this.apiBase.ProductGetCartItems, cartItems);
  }

  public addToFavorites(productId: string) {
    return this.accountService.addToFavorites(productId)
  }

  public getSearchProducts(searchPhrase: string,category:string,page:number) {
    const params = new HttpParams().append('searchPhrase', searchPhrase).append('category', category).append('page', page);
    return this.http.get<ProductSearchResponseList>(this.apiBase.ProductSearch, { params });
  }
}
