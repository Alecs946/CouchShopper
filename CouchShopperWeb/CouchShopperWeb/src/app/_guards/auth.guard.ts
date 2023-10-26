import { inject, Injectable } from "@angular/core";
import { ActivatedRouteSnapshot, CanActivateFn, Router, RouterStateSnapshot } from "@angular/router";
import { ToastrService } from "ngx-toastr";
import { map, Observable } from "rxjs";
import { LoginResponse } from "../_models/Account/response/user-response";
import { AccountService } from "../_services/account.service";


@Injectable({
  providedIn: 'root'
})
class PermissionsService {
  public getUser(): LoginResponse {
    const user: LoginResponse = JSON.parse(localStorage.getItem('user')) ?? ""

    return user;
  }
  constructor(private router: Router, private accountService: AccountService, private toastr: ToastrService) {}

  canActivate(next: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean {
    let user = this.getUser()
    if (user) {
      return true;
    }
    else {
      this.toastr.error("Login in order to contionue")
      return false; 
    }
  }
}

export const AuthGuard: CanActivateFn = (next: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean => {
  return inject(PermissionsService).canActivate(next, state);
}
