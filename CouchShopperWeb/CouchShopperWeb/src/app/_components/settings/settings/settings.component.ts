import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-settings',
  templateUrl: './settings.component.html',
  styleUrls: ['./settings.component.css']
})
export class SettingsComponent implements OnInit {

  constructor(private route: ActivatedRoute) { }
  mode!: number;
  id:string=""
  ngOnInit(): void {
    this.route.params.subscribe(params => {
      this.id = params['id'];
    })
    this.route.url.subscribe(url => {
      switch (url[0]?.path) {
        case 'settings':
          this.mode = 1;
          break;
        case 'purchases':
          this.mode = 2;
          break;
        case 'favorites':
          this.mode = 3;
          break;
        case 'sales':
          this.mode = 4;
          break;
        case 'products':
          this.mode = 5;
          break;
        case 'orders':
          this.mode = 6;
          break;
        case 'new-product':
          this.mode = 7;
          break;
        case 'payouts':
          this.mode = 8;
          break;
        case 'countries':
          this.mode = 9;
          break;
        case 'categories':
          this.mode = 10;
          break;
        case 'icons':
          this.mode = 11;
          break;
        case 'edit-product':
          this.mode = 12;
          break;
        case 'special-offer':
          this.mode = 13;
          break;
        case 'featured-products':
          this.mode = 14;
          break;
        case 'order-new':
          this.mode = 15;
          break;
        case 'order-approved':
          this.mode = 16;
          break;
        case 'order-sent':
          this.mode = 17;
          break;
        case 'order-delivered':
          this.mode = 18;
          break;
        case 'order-declined':
          this.mode = 19;
          break;
        default:
          this.mode = 1;
      }
    });
  }

}
