import { Component, Input } from '@angular/core';
import { TopSeller } from '../../../_models/_common/response/home-content-response';

@Component({
  selector: 'app-top-seller',
  templateUrl: './top-seller.component.html',
  styleUrls: ['./top-seller.component.css']
})
export class TopSellerComponent {
  @Input() sellerInfo!: TopSeller;
  
}
