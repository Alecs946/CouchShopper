import { inject, Injectable } from "@angular/core";
import { ActivatedRouteSnapshot, CanActivateFn, Router, RouterStateSnapshot } from "@angular/router";
import { use } from "echarts";
import { ToastrService } from "ngx-toastr";
import { LoginResponse } from "../_models/Account/response/user-response";
import { AccountService } from "../_services/account.service";


@Injectable({
  providedIn: 'root'
})
class AdminRoleService {
  public getUser(): LoginResponse {
    const user: LoginResponse = JSON.parse(localStorage.getItem('user')) ?? ""

    return user;
  }
  constructor(private router: Router, private accountService: AccountService, private toastr: ToastrService) { }

  canActivate(next: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean {
    let user = this.getUser()
    if (user && user.role==="1") {
      return true;
    }
    else {
      this.router.navigate(["/"])
      return false;
    }
  }
}

@Injectable({
  providedIn: 'root'
})
class SellerRoleService {
  public getUser(): LoginResponse {
    const user: LoginResponse = JSON.parse(localStorage.getItem('user')) ?? ""

    return user;
  }
  constructor(private router: Router, private accountService: AccountService, private toastr: ToastrService) { }

  canActivate(next: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean {
    let user = this.getUser()
    if (user && user.role === "2") {
      return true;
    }
    else {
      this.router.navigate(["/"])
      return false;
    }
  }
}

@Injectable({
  providedIn: 'root'
})
class BuyerRoleService {
  public getUser(): LoginResponse {
    const user: LoginResponse = JSON.parse(localStorage.getItem('user')) ?? ""

    return user;
  }
  constructor(private router: Router, private accountService: AccountService, private toastr: ToastrService) { }

  canActivate(next: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean {
    let user = this.getUser()
    if (user && user.role === "3") {
      return true;
    }
    else {
      this.router.navigate(["/"])
      return false;
    }
  }
}
export const AdminGuard: CanActivateFn = (next: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean => {
  return inject(AdminRoleService).canActivate(next, state);
}
export const SellerGuard: CanActivateFn = (next: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean => {
  return inject(SellerRoleService).canActivate(next, state);
}
export const BuyerGuard: CanActivateFn = (next: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean => {
  return inject(BuyerRoleService).canActivate(next, state);
}
