import { Component, OnInit } from '@angular/core';
import { UserFavoritesResponse } from '../../../_models/Account/response/favorites-response';
import { SettingsService } from '../../../_services/settings.service';

@Component({
  selector: 'app-settings-favorites',
  templateUrl: './settings-favorites.component.html',
  styleUrls: ['./settings-favorites.component.css']
})
export class SettingsFavoritesComponent implements OnInit {
  favorites: UserFavoritesResponse[];
  constructor(private settingsService: SettingsService) { }
  ngOnInit(): void {
    this.settingsService.getFavorites().subscribe({
      next: (response => {
        this.favorites = response
      }),
      error: (
        error => {
          console.log(error.error.message)
        })
    })
  }



}
