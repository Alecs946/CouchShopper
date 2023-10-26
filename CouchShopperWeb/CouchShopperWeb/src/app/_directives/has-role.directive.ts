import { Directive, Input, OnInit, TemplateRef, ViewContainerRef } from '@angular/core';
import { LoginResponse } from '../_models/Account/response/user-response';
import { AccountService } from '../_services/account.service';

@Directive({
  selector: '[appHasRole]'
})
export class HasRoleDirective implements OnInit {
  @Input() appHasRole: string[] = [];
  user: LoginResponse = {} as LoginResponse;

  constructor(private viewContainerRef: ViewContainerRef, private templateRef: TemplateRef<any>,
    private accountService: AccountService) {
    this.user = JSON.parse(localStorage.getItem('user')) ?? ""
  }

  ngOnInit(): void {
    if (this.appHasRole.includes(this.user.role)) {
      this.viewContainerRef.createEmbeddedView(this.templateRef);
    }
    else {
      this.viewContainerRef.clear()
    }
  }

}
