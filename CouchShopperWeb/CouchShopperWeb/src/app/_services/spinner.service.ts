import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class SpinnerService {
  private spinnerStatusSubject = new BehaviorSubject<boolean>(false);
  spinnerStatus = this.spinnerStatusSubject.asObservable();

  public showSpinner() {
    this.spinnerStatusSubject.next(true);
  }

  public hideSpinner() {
    this.spinnerStatusSubject.next(false);
  }
}
