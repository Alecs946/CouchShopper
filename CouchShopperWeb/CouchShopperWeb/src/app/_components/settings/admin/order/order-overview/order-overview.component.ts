import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { OrderService } from '../../../../../_services/order.service';

@Component({
  selector: 'app-order-overview',
  templateUrl: './order-overview.component.html',
  styleUrls: ['./order-overview.component.css']
})
export class OrderOverviewComponent implements OnInit {
  mode: number = 15;
  constructor(private route: ActivatedRoute,public orederService:OrderService) { }

  ngOnInit(): void {
    this.route.url.subscribe(url => {
      switch (url[0]?.path) {
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
          this.mode = 15;
      }
    })
    
  }
}

