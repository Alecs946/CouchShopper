import { HttpClient, HttpParams } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { catchError, map, Observable, of, switchMap } from 'rxjs';
import { LoginResponse } from '../../../_models/Account/response/user-response';
import { Main, DropDownItem } from '../../../_models/_common/response/home-content-response';
import { AccountService } from '../../../_services/account.service';
import { ApiEndpointService } from '../../../_services/api-endpoint.service';
import { HomeService } from '../../../_services/home.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
  private getScreenWidth!: number;
  public main!: Main;
  public categories: DropDownItem[] = []
  imageUrl: Observable<string | null> | undefined;


  constructor(private http: HttpClient, private apiBase: ApiEndpointService, private accountSerice: AccountService, public homeService: HomeService) { }

  ngOnInit(): void {
    this.getScreenWidth = window.innerWidth;
    this.homeService.getContent().subscribe(
      {
        next: (response => {
          this.main = {
            featuredProducts: response.featuredProducts.map(x => {
              return {
                id: x.id,
                name: x.name,
                rating: x.rating,
                imageBase64: x.photo.content,
                numberOfSales: x.numberOfSales,
                price: x.price
              }
            }),
            mostRecentProducts: response?.recentProducts?.map((x) => {
              return {
                id: x.id,
                name: x.name,
                rating: x.rating,
                imageBase64: x.photo.content,
                numberOfSales: x.numberOfSales,
                price: x.price
              }
            }) ?? null,
            specialOffer: {
              slides: response.specialOffers,
              interval: 2000,
              isAnimated: true,
              showIndicators: true
            },
            topSeller: {
              sellerName: response?.topSeller.sellerName,
              imageBase64: response.topSeller.imageBase64,
              memberSince: response.topSeller.memberSince,
              products: response.topSeller?.products?.map((x) => {
                return {
                  id: x.id,
                  name: x.name,
                  rating: x.rating,
                  imageBase64: x.imageBase64,
                  numberOfSales: x.numberOfSales,
                  price: x.price
                }
              }) ?? null
            }
          }
        }),
        error: (
          error => {
            console.log(error.error.message)
          })
      })


    this.categories = [
      { name: "All", iconName: "fas fa-search" },
      { name: "Arts & Crafts", iconName: "fa-solid fa-palette" },
      { name: "Automotive", iconName: "fa-solid fa-car" },
      { name: "Baby", iconName: "fa-solid fa-baby" },
      { name: "Beauty & Personal Care", iconName: "fa-solid fa-spa" },
      { name: "Books", iconName: "fa-solid fa-book-open" },
      { name: "Boys' Fashion", iconName: "fa-solid fa-child-reaching" },
      { name: "Computers", iconName: "fa-solid fa-computer" },
      { name: "Digital Music", iconName: "fa-solid fa-music" },
      { name: "Electronics", iconName: "fa-solid fa-microchip" },
      { name: "Girls' Fashion", iconName: "fa-solid fa-child-dress" },
      { name: "Health & Household", iconName: "fa-solid fa-house-chimney-medical" },
      { name: "Home & Kitchen", iconName: "fa-solid fa-kitchen-set" },
      { name: "Men's Fashion", iconName: "fa-solid fa-person" },
      { name: "Movies & TV", iconName: "fa-solid fa-film" },
      { name: "Pet Supplies", iconName: "fa-solid fa-paw" },
      { name: "Software", iconName: "fa-solid fa-floppy-disk" },
      { name: "Sports & Outdoors", iconName: "fa-solid fa-person-biking" },
      { name: "Tools & Home Improvement", iconName: "fa-solid fa-screwdriver-wrench" },
      { name: "Toys & Games", iconName: "fa-brands fa-space-awesome" },
      { name: "Video Games", iconName: "fa-solid fa-gamepad" },
      { name: "Women's Fashion", iconName: "fa-solid fa-person-dress" },
    ]
    this.imageUrl = this.accountSerice.currentUser$.pipe(
      switchMap((user: LoginResponse | null) => {
        if (user) {
          const currentUsername = user.username;
          let params = new HttpParams().set('username', currentUsername);
          return this.http.get(this.apiBase.UserGetProfilePicture, { params, responseType: 'text' }).pipe(
            map((base64String: string) => 'data:image/jpeg;base64,' + base64String),
            catchError((error: any) => {
              console.log("Error fetching profile picture:", error);
              return of(null);
            })
          );
        } else {
          console.log("No logged-in user");
          return of(null);
        }
      })
    );
  }
  GetImageToBase64(fileName: string) {
    return './assets/products/' + fileName
  }
  GetNumberOfProducts() {
    if (this.getScreenWidth < 910) {
      return 1;
    }
    if (this.getScreenWidth < 1440) {
      return 2;
    }
    if (this.getScreenWidth < 1850) {
      return 2;
    }
    return 3;
  }

}
