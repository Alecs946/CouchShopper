import { Observable } from "rxjs";
export interface DropDownItem {
  name: string;
  value?: string | number | null;
  iconName: string;
}

export interface Dropdown {
  key: string;
  value: string;
}

export interface DropDownItemWithNotification {
  name: string;
  iconName: string;
  action: string;
  alwaysShow: boolean;
  numberOfNotifications?: Observable<any>;
  role:string[]
}
export interface AvatarDropDownGroup {
  groupName: string;
  dropDownItems: DropDownItemWithNotification[];
  role:string[]
}
export interface SpecialOffer {
  id?: string
  photoId?: string
  title: string;
  description: string;
  backgroundColor: string;
  imageBase64: string;
  textColor: string
  published: boolean;
}
export interface PhotoResponse {
  id: string;
  content: string;
  isDefault: boolean;
}

export interface FeaturedProductResponse {
  id: string
  userName: string
  name: string
  price: number
  rating: number
  numberOfSales: number
  photoId: string
  photo: PhotoResponse
}
export interface ProductRecentAddsResponse {
  id: string
  userName: string
  name: string
  price: number
  rating: number
  numberOfSales: number
  photoId: string
  photo: PhotoResponse
}
export interface Product {
  id: string
  name: string;
  rating: number;
  imageBase64: string;
  numberOfSales: number;
  price: number;
}

export interface TopSeller {
  sellerName: string;
  imageBase64: string;
  products: Product[];
  memberSince: string;
}
export interface HomeContentResponse {

  specialOffers: SpecialOffer[];
  featuredProducts: FeaturedProductResponse[];
  recentProducts: ProductRecentAddsResponse[];
  topSeller: TopSeller;
}

export interface SpecialOfferSlide {
  title: string;
  description: string;
  backgroundColor: string;
  imageBase64: string;
  textColor: string
}

export interface SpecialOfferMain {
  slides: SpecialOfferSlide[];
  isAnimated: boolean;
  showIndicators: boolean;
  interval: number;
}

export interface Main {
  specialOffer: SpecialOfferMain;
  featuredProducts: Product[];
  mostRecentProducts: Product[];
  topSeller: TopSeller;
}
