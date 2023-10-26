import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor
} from '@angular/common/http';
import { Observable, take } from 'rxjs';
import { AccountService } from '../_services/account.service';
import { LoginResponse } from '../_models/Account/response/user-response';

@Injectable()
export class JwtInterceptor implements HttpInterceptor {

  constructor() {}
  public getUser(): LoginResponse {
    const user: LoginResponse = JSON.parse(localStorage.getItem('user')) ?? ""

    return user;
  }
  intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {
    let user = this.getUser();
    if (user) {
      request = request.clone({
        setHeaders: {
          Authorization: `Bearer ${user.token}`
        }
      })
    }

    return next.handle(request);
  }
}
