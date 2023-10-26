import { Injectable } from '@angular/core';
import { CommonService } from './common.service';

@Injectable({
  providedIn: 'root'
})
export class HomeService {

  constructor(private commonService: CommonService) { }

  public getSpecialOffers() {
    return this.commonService.getPublishedSpecialOffer()
  }

  public getContent() {
    return this.commonService.getHomePageContent()
  }
}
