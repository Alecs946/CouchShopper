import { HttpClient } from '@angular/common/http';
import { Component, OnInit} from '@angular/core';
import { NavigationEnd, Router } from '@angular/router';
import { LoginResponse } from './_models/Account/response/user-response';
import { AccountService } from './_services/account.service';


@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  user!: LoginResponse;
  constructor(private router: Router, private http: HttpClient,private accountService:AccountService) { }
  scrollToTop() {
    window.scrollTo(0, 0);
  }

  title = 'Couch Shopper';

  ngOnInit() {
    this.router.events.subscribe((event) => {
      if (event instanceof NavigationEnd) {
        this.scrollToTop();
      }
      this.setCurrentUser();
    });
  }

  setCurrentUser(): void {
    const userString: string | null = localStorage.getItem('user');
    if (userString) {
      const user: LoginResponse = JSON.parse(userString);
      this.accountService.setCurrentUser(user);
    } else {
      this.accountService.setCurrentUser(null);
    }
  }

}
