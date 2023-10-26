import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor,
  HttpResponse
} from '@angular/common/http';
import { finalize, Observable, tap } from 'rxjs';
import { SpinnerService } from '../_services/spinner.service';

@Injectable()
export class LoadingInterceptor implements HttpInterceptor {
  private activeRequests = 0;

  constructor(private spinnerService: SpinnerService) { }

  intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    this.activeRequests++;
    if (this.activeRequests === 1) {
      this.spinnerService.showSpinner();
    }

    return next.handle(request).pipe(
      tap(
        (event) => {
          if (event instanceof HttpResponse) {
            this.activeRequests--;
            if (this.activeRequests === 0) {
              this.spinnerService.hideSpinner();
            }
          }
        },
        () => {
          this.activeRequests--;
          if (this.activeRequests === 0) {
            this.spinnerService.hideSpinner();
          }
        }
      )
    );
  }
}
