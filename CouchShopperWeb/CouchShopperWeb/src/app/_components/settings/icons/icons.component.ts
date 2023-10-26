import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Icon } from '../../../_models/_common/request/icon-request';
import { CommonService } from '../../../_services/common.service';

@Component({
  selector: 'app-icons',
  templateUrl: './icons.component.html',
  styleUrls: ['./icons.component.css']
})
export class IconsComponent implements OnInit {
  selectedIcon: Icon | null = null;
  icons: Icon[] = [];
  iconForm !: FormGroup;
  iconFormSubmitted: boolean = false;
  showIconForm: boolean = false;
  numberOfIcons = 0;
  iconCurrentPage = 1;
  itemsPerPage = 5;
  maxSizePagination = 5;


  constructor(private formBuilder: FormBuilder, private commonService: CommonService) { }

  ngOnInit(): void {
    this.getIcons();
    this.iconForm = this.formBuilder.group({
      name: ['', Validators.required],
    })
  }

  getIcons(): void {
    this.commonService.getIcons(this.iconCurrentPage)
      .subscribe(
        {
          next: (response => {
            this.numberOfIcons = response.totalEntities;
            this.icons = response.icons;
          }),
          error: (
            error => {
              console.log(error.error.message)
            })
        }
      );
  }

  addIcon(): void {
    if (this.iconForm.valid) {
      this.iconFormSubmitted = false;
      this.commonService.addIcon(this.iconForm.value).subscribe(
        {
          next: (response => {
            this.getIcons()
            this.iconForm.reset();
            this.showIconForm = false;
          }),
          error: (
            error => {
              console.log(error.error.message)
            })
        }
      );
    }
    else {
      this.iconFormSubmitted=true
    }
  }

  deleteIcon(iconId: string): void {
    this.commonService.deleteIcon(iconId)
      .subscribe(
        {
          next: (response => {
            this.getIcons()
          }),
          error: (
            error => {
              console.log(error.error.message)
            })
        }
      );
  }

  editIcon(icon: Icon): void {
    this.selectedIcon = icon;
  }

  saveIcon(icon: Icon): void {
    console.log(icon)
    if (icon.name.trim() !== "") {
      this.commonService.updateIcon(icon).subscribe(
        {
          next: (response => {
            this.getIcons()
          }),
          error: (
            error => {
              console.log(error.error.message)
            })
        }
      );
    }
    this.selectedIcon = null;
  }

  cancelEditIcon(): void {
    this.selectedIcon = null;
  }

  iconPageChanged(event: any): void {
    this.iconCurrentPage = event.page;
    this.getIcons();
  }

}
