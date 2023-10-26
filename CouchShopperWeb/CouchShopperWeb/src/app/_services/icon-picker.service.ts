import { Injectable } from '@angular/core';
import { CommonService } from './common.service';

@Injectable({
  providedIn: 'root'
})
export class IconPickerService {

  constructor(private commonService: CommonService) { }

  public getIcons() {
    return this.commonService.getIconsDropdown();
  }
}
