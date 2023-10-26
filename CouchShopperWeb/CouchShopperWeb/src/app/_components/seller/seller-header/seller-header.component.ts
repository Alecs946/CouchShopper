import { Component, Input } from '@angular/core';
import { Seller } from '../../../_models/Account/response/seller-response';

@Component({
  selector: 'app-seller-header',
  templateUrl: './seller-header.component.html',
  styleUrls: ['./seller-header.component.css']
})
export class SellerHeaderComponent {
  @Input() sellerHeader!:Seller
}
