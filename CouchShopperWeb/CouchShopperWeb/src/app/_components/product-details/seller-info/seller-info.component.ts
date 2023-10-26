import { Component, Input } from '@angular/core';
import { SellerInfo } from '../../../_models/Account/response/seller-response';

@Component({
  selector: 'app-seller-info',
  templateUrl: './seller-info.component.html',
  styleUrls: ['./seller-info.component.css']
})
export class SellerInfoComponent {
  @Input() sellerInfo!: SellerInfo;
}
