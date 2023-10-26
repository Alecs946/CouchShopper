import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable, of, ReplaySubject } from 'rxjs';
import { map } from 'rxjs/operators';
import { UserActivationRequest, LoginRequest, UserAddRequest } from '../_models/Account/request/user-request';
import { LoginResponse, UserResponse, UserIsActiveResponse, ActivationForm } from '../_models/Account/response/user-response';
import { ApiEndpointService } from './api-endpoint.service';
import { PaymentMethod } from '../_models/_settings/response/settings-response';
import {
  AccountPasswordUpdateRequest, AccountPaymentMethodResponse,
  AccountProfilePictureResponse, AccountResponse
} from '../_models/Account/response/account-response';
import {
  AccountAddPaymentCreditCardRequest, AccountAddPayPal,
  AccountPaymentMethodDeleteRequest, AccountPaymentMethodUpdateRequest,
  AccountProfilePictureUploadRequest,
  LoginForm
} from '../_models/Account/request/account-request';
import { SellerDetailsResponse, SellerInfoResponse, SellerPayoutsResponseList, SellerSalesInfoResponse } from '../_models/Account/response/seller-response';
import { FavoritesAddRequest } from '../_models/Account/request/favorites-request';
import { UserFavoritesResponse } from '../_models/Account/response/favorites-response';
import { use } from 'echarts';

@Injectable({
  providedIn: 'root'
})
export class AccountService {
  private currentUserSource: ReplaySubject<LoginResponse | null> = new ReplaySubject<LoginResponse | null>(1);
  public currentUser$: Observable<LoginResponse | null> = this.currentUserSource.asObservable();

  private profilePictureSubject: BehaviorSubject<string | null> = new BehaviorSubject<string | null>(null);
  public profilePicture$: Observable<string | null> = this.profilePictureSubject.asObservable();

  private username: string = '';
  private password: string = '';

  constructor(private http: HttpClient, private apiBase: ApiEndpointService) {
    this.getProfilePicture().subscribe();
  }

  public getUsername(): string {
    const user: LoginResponse = JSON.parse(localStorage.getItem('user')) ?? ""

    return user.username;

  }
  public isAutorized(): boolean {
    const user: LoginResponse = JSON.parse(localStorage.getItem('user')) ?? ""
    if (user) {
      return true
    }
    return false;
  }

  /***********************************************************************************************************************/
  ///User
  /***********************************************************************************************************************/
  public setCurrentUser(user: LoginResponse | null): void {
    if (user && user.token) {
      user.role = this.getDecodedToken(user.token).role
    }
    localStorage.setItem('user', JSON.stringify(user));
    this.currentUserSource.next(user);
    this.currentUser$ = this.currentUserSource.asObservable();
  }

  public registerUser(model: UserAddRequest): Observable<UserResponse> {
    return this.http.post<UserResponse>(this.apiBase.UserRegister, model);
  }

  public setCredentials(user: LoginForm): void {
    this.username = user.username;
    this.password = user.password;
  }

  public isActiveAccount(user: LoginForm): Observable<UserIsActiveResponse> {
    this.setCredentials(user);
    return this.http.post<UserIsActiveResponse>(this.apiBase.UserIsActive, user);
  }

  public activeAccount(form: ActivationForm) {
    const user: UserActivationRequest = {
      username: this.username,
      code: form.activationCode
    };
    return this.http.post(this.apiBase.UserActivateUser, user)
  }
  public loginAfterAcctivation() {
    return this.loginUser({ username: this.username, password: this.password })
  }

  public loginUser(model: LoginForm): Observable<void> {
    return this.http.post<LoginResponse>(this.apiBase.UserLogin, model).pipe(
      map((user: LoginResponse) => {
        if (user) {

          this.setCurrentUser(user);
        }
      })
    );
  }

  public logout(): void {
    localStorage.removeItem('user');
    this.profilePictureSubject.next(null);
    this.setCurrentUser(null);
  }

  /***********************************************************************************************************************/
  ///Account
  /***********************************************************************************************************************/
  public getProfileDetails(): Observable<AccountResponse> {
    const params = new HttpParams().set('username', this.getUsername());

    return this.http.get<AccountResponse>(this.apiBase.UserGetProfileDetails, { params })
  }

  public updateProfileDetails(userDetails: AccountResponse) {
    return this.http.put<AccountResponse>(this.apiBase.UserUpdateProfileDetails, userDetails)
  }

  public reloadProfilePicture() {
    this.profilePictureSubject.next(null);

    return this.getProfilePicture();
  }

  private getProfilePicture(): Observable<void> {
    if (this.isAutorized()) {
      let params = new HttpParams().set('username', this.getUsername());
      return this.http.get<AccountProfilePictureResponse>(this.apiBase.UserGetProfilePicture, { params }).pipe(
        map((picure: AccountProfilePictureResponse) => {
          if (picure.profilePicture && picure.profilePicture.trim() !== "") {
            const imageUrl = 'data:image/jpeg;base64,' + picure.profilePicture;
            this.profilePictureSubject.next(imageUrl);
          }
          else {
            this.profilePictureSubject.next(null);
          }
        })
      );
    }
    return of();
  }

  public uploadProfilePicture(picture: string): Observable<boolean> {
    const pictureInfo: AccountProfilePictureUploadRequest = {
      username: this.getUsername(),
      profilePicture: picture
    }

    return this.http.post<boolean>(this.apiBase.UserUploadProfilePicture, pictureInfo)
  }

  public updatePassword(password: AccountPasswordUpdateRequest) {
    password.username = this.getUsername();

    return this.http.put<AccountPasswordUpdateRequest>(this.apiBase.UserUpdatePassword, password)
  }

  public getPaymentMethods(): Observable<PaymentMethod[]> {
    const params = new HttpParams().set('username', this.getUsername());

    return this.http.get<AccountPaymentMethodResponse[]>(this.apiBase.UserPaymentMethod, { params })
  }

  public createCard(card: AccountAddPaymentCreditCardRequest) {
    card.username = this.getUsername();

    return this.http.post(this.apiBase.UserAddCard, card)
  }

  public createPaypal(paypal: AccountAddPayPal) {
    paypal.username = this.getUsername();

    return this.http.post(this.apiBase.UserAddPaypal, paypal)
  }

  public updatePrimaryPaymentMethod(paymentId: string) {
    const primaryPayment: AccountPaymentMethodUpdateRequest = {
      userId: this.getUsername(),
      paymentId: paymentId
    };

    return this.http.put(this.apiBase.UserUpdatePrimaryPaymentMethod, primaryPayment)
  }

  public deletePaymentMethod(paymentId: string) {
    const payment: AccountPaymentMethodDeleteRequest = {
      username: this.getUsername(),
      cardId: paymentId,
    };

    return this.http.delete(this.apiBase.DeletePaymentMethod, { body: payment });
  }

  /***********************************************************************************************************************/
  ///Seller
  /***********************************************************************************************************************/
  public getSellerInfo(sellerId: string): Observable<SellerInfoResponse> {
    const params = new HttpParams().append('userId', sellerId);
    return this.http.get<SellerInfoResponse>(this.apiBase.UserGetSellerInfo, { params });
  }

  public getSellerDetails(sellerId: string, page: number) {
    const params = new HttpParams().set('userId', sellerId).append('page', page);
    return this.http.get<SellerDetailsResponse>(this.apiBase.UserGetUserDetails, { params })
  }

  public getSalesInfo(periodId: number) {
    const params = new HttpParams().set('userId', this.getUsername()).append('periodId', periodId);
    return this.http.get<SellerSalesInfoResponse>(this.apiBase.UserGetSalesInfo, { params })
  }

  public getPayouts(page: number) {
    const params = new HttpParams().set('userId', this.getUsername()).append('page', page)
    return this.http.get<SellerPayoutsResponseList>(this.apiBase.UserGetPayouts, { params })
  }

  public getBalance() {
    const params = new HttpParams().set('userId', this.getUsername())
    return this.http.get<number>(this.apiBase.UserGetBalance, { params })
  }

  public withdrawal() {
    const userId = this.getUsername();
    return this.http.post(this.apiBase.UserWithdrawal, { userId })
  }
  /***********************************************************************************************************************/
  //Favorites
  /***********************************************************************************************************************/
  public addToFavorites(productId: string) {
    const favorite: FavoritesAddRequest = {
      ProductId: productId,
      UserId: this.getUsername()
    }
    return this.http.put<string>(this.apiBase.UserAddToFavorites, favorite)
  }

  public getFavorites() {
    const params = new HttpParams().set('userId', this.getUsername());
    return this.http.get<UserFavoritesResponse[]>(this.apiBase.UserGetFavorites, { params })
  }

  public getFavoritesCount() {
    const params = new HttpParams().set('userId', this.getUsername())
    return this.http.get<number>(this.apiBase.UserGetFavoritesCount, { params })
  }

  public getDecodedToken(token: string) {
    return JSON.parse(atob(token.split('.')[1]));
  }
}
