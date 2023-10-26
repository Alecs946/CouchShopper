import { Injectable } from '@angular/core';
import {HttpRequest, HttpHandler, HttpEvent, HttpInterceptor, HttpErrorResponse} from '@angular/common/http';
import { Observable, catchError } from 'rxjs';
import { NavigationExtras, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';

@Injectable()
export class ErrorInterceptor implements HttpInterceptor {

  constructor(private router: Router, private toastr: ToastrService) { }

  intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {
    return next.handle(request).pipe(
      catchError((error: HttpErrorResponse) => {
        if (error) {
          switch (error.status) {
            case 400:
                this.toastr.error(error.error.message)
              break;
            case 401:
              this.toastr.error('Unauthorised');
              break;
            case 404:
              this.toastr.error('Not Found');
              break;
            case 500:
              this.toastr.error('Something unexpected went wrong');
              break;
            default:
              this.toastr.error('Something unexpected went wrong');
              break;
          }
        }
        throw error;
      })
    )
  }
}
