import { Component, Input, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Observable } from 'rxjs';
import { AccountService } from '../../../_services/account.service';
import { SettingsService } from '../../../_services/settings.service';

@Component({
  selector: 'app-settings-aside',
  templateUrl: './settings-aside.component.html',
  styleUrls: ['./settings-aside.component.css']
})
export class SettingsAsideComponent implements OnInit {
  isHomeSubmenuCollapsed = !(this.route.snapshot.url[0].path === 'special-offer');
  isOrderSubmenuCollapsed: boolean = false;
  @Input() mode: number = 1;
  productCount: number = 0;
  balance$: Observable<number>
  favorites$: Observable<number>

  toggleHomeSubmenu() {
    this.isHomeSubmenuCollapsed = !this.isHomeSubmenuCollapsed;
  }
  toggleOrderSubmenu() {
    this.isOrderSubmenuCollapsed = !this.isOrderSubmenuCollapsed;
  }
  constructor(private route: ActivatedRoute, public settings: SettingsService, private accountService: AccountService) {
  }

  ngOnInit(): void {
    this.settings.usersProductCount().subscribe(
      {
        next: (response => {
          this.productCount = response
        }),
        error: (
          error => {
            console.log(error.error.message);
          })
      }
    );
    this.route.url.subscribe(url => {
      switch (url[0]?.path) {
        case 'order-new':
        case 'order-approved':
        case 'order-sent':
        case 'order-delivered':
        case 'order-declined':
          this.isOrderSubmenuCollapsed = false;
          break;
        default:
          this.isOrderSubmenuCollapsed = true;
      }
    })

    this.balance$ = this.getBalance();
    this.favorites$= this.getFavoritesCount()
  }


  public getBalance() {
    return this.accountService.getBalance();
  }

  public getFavoritesCount() {
    return this.accountService.getFavoritesCount();
  }
}
