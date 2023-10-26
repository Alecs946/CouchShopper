import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { IconPickerService } from '../../_services/icon-picker.service';
@Component({
  selector: 'app-icon-picker',
  templateUrl: './icon-picker.component.html',
  styleUrls: ['./icon-picker.component.css']
})
export class IconPickerComponent implements OnInit {
  @Output() iconSelected: EventEmitter<string> = new EventEmitter<string>();
  isPopupVisible: boolean = false;
  selectedIconName: string = '';
  fontAwesomeIcons!: string[];

  constructor(public iconPickerService: IconPickerService) {}

  ngOnInit(): void {
    this.iconPickerService.getIcons().subscribe(
      {
        next: (response => {
          this.fontAwesomeIcons = response.map(item => item.value);
        }),
        error: (
          error => {
            console.log(error.error.message)
          })
      }
    );
    }

  openIconPicker() {
    this.isPopupVisible = true;
  }

  closeIconPicker() {
    this.isPopupVisible = false;
  }

  selectIcon(iconName: string) {
    this.iconSelected.emit(iconName);
    this.closeIconPicker();
  }
}
