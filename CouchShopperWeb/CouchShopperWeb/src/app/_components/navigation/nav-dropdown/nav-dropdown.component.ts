import { Component, Input, OnInit } from '@angular/core';
import { AvatarDropDownGroup } from '../../../_models/_common/response/home-content-response';

@Component({
  selector: 'app-nav-dropdown',
  templateUrl: './nav-dropdown.component.html',
  styleUrls: ['./nav-dropdown.component.css']
})
export class NavDropdownComponent {
  @Input() DropDownItem!: AvatarDropDownGroup;
  }
