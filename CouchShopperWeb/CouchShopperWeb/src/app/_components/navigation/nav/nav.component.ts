import { Component, OnInit, TemplateRef, ViewChild } from '@angular/core';
import { Observable } from 'rxjs';
import { AccountComponent } from '../../account/account/account.component';
import { LoginResponse } from '../../../_models/Account/response/user-response';
import { DropDownItem, AvatarDropDownGroup } from '../../../_models/_common/response/home-content-response';
import { NavigationService } from '../../../_services/navigation.service';
import { AccountActivateComponent } from '../../account/account-activate/account-activate.component';
import { Router } from '@angular/router';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent {
  @ViewChild('login') loginComponent!: AccountComponent;
  @ViewChild('activate') activateAccountComponent!: AccountActivateComponent;

  avatarDropDownItems!: AvatarDropDownGroup[] | null;
  categories$!: Observable<DropDownItem[]>;
  avatarDropdown$!: Observable<AvatarDropDownGroup[]>;
  activeOrdersCount$!: Observable<number>;
  searchBoxCollapsed = true;
  navbarCollapsed = true;
  currentUser$!: Observable<LoginResponse | null>;
  searchPhrase: string = ''
  buttonText ='Categories'
  searchCategory=''

  constructor(public navigationService: NavigationService, private router: Router) {
    this.currentUser$ = this.navigationService.currentUser();
    this.avatarDropdown$ = this.navigationService.getAvatarDropdown();
    this.categories$ = this.navigationService.getCategories();
  }

  openModal() {
    this.navigationService.modalOpen(this.loginComponent);
  }

  loginFormSubmitted(activateAccount: boolean) {
    this.navigationService.modelClose();
    if (activateAccount) {
      this.navigationService.activateAccount(this.activateAccountComponent)
    }
    else {
      location.reload();
    }
  }

  activationFormSubmitted(activated: boolean) {
    if (activated) {
      this.navigationService.modelClose();
      location.reload();
    }
  }

  logout() {
    this.navigationService.logoutUser();
    this.router.navigate(['/']);
  }

  search() {
    this.router.navigate(['search', this.searchPhrase, this.searchCategory]);
  }
  selectCategory(item) {
    this.buttonText = item.name
    this.searchCategory=item.name
  }
}
