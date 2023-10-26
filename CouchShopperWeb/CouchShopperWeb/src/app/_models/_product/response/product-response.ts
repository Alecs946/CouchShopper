export interface Product {
  id: string
  name: string;
  rating: number;
  imageBase64: string;
  numberOfSales: number;
  price: number;
}
export interface PhotoResponse {
  id: string;
  content: string;
  isDefault: boolean;
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

export interface ProductResponse {
  id: string;
  userId: string;
  name: string;
  brand: string;
  model: string;
  price: number;
  rating: number;
  numberOfReviews: number
  salePrice: number | null;
  quantity: number;
  category: string;
  productDescription: string;
  featureProduct: boolean;
  options: ProductOptionResponse[];
  photos: PhotoResponse[];
}
export interface ProductsByUserResponse {
  id: string
  name: string
  price: number
  numberOfSales: number
  earnings: number
  photoId: string
  photo: PhotoResponse
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
export interface FeaturedProductListResponse {
  totalEntities: number;
  offset: number;
  products: FeaturedProductResponse[];
}
export interface ProductOptionResponse {
  productOptionName: string;
  productOptionValues: string[];
}

export interface ProductByUserListResponse {
  totalEntities: number;
  offset: number;
  products: ProductsByUserResponse[];
}
export interface DropDownItem {
  name: string;
  value?: string | number | null;
  iconName: string;
}
export interface ProductSearchResponse {
  id: string
  name: string
  price: number
  numberOfSales: number
  earnings: number
  photoId: string
  photo: PhotoResponse
}
export interface ProductSearchResponseList {
  totalEntities: number
  offset: number
  products: ProductSearchResponse[]
}
export interface ProductDetailsOptionResponse {
  optionName: string;
  optionValues: DropDownItem[];
}
export interface GalleryImage {
  defaultImage: boolean;
  thumbnailUrl: string;
  largeUrl: string;
}
export interface ProductReview {
  userId?: string,
  comment: string;
  rating: number;
}
export interface ProductDescription {
  brand: string;
  model?: string;
  description: string;
}
export interface ProductDetails {
  productName: string;
  rating: number;
  price: number;
  salePrice?: number;
  numberOfReviews: number;
  numberOfAvailableProducts?: number;
  productOptions: ProductDetailsOptionResponse[];
  pictures: GalleryImage[];

}
export interface ProductReviewsListResponse {
  totalEntities: number;
  offset: number;
  reviews: ProductReview[]
}
export interface ProductPanel {
  productDescription: ProductDescription;
  shippingOptions: Shipping[];
}

export interface Shipping {
  shippingMethodName: string;
  estimatedTime: string;
  shippingPrice: number;
  minNumberOfDays?: number
  maxNumberOfDays?: number
}
