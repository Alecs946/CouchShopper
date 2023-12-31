import { Component, Input, OnInit } from '@angular/core';
import { SpecialOfferMain } from '../../../_models/_common/response/home-content-response';

@Component({
  selector: 'app-special-offer',
  templateUrl: './special-offer.component.html',
  styleUrls: ['./special-offer.component.css'],
})
export class SpecialOfferComponent implements OnInit {
  @Input() specialOffer!: SpecialOfferMain;
  ngOnInit(): void {
  }
} 
