export interface Product {
  id: string
  name: string;
  rating: number;
  imageBase64: string;
  numberOfSales: number;
  price: number;
}

export interface PhotoAddRequest {
  content: string;
  isDefault: boolean;
}
export interface DropDownItem {
  name: string;
  value?: string | number | null;
  iconName: string;
}
export interface ProductOption {
  optionName: string;
  optionValues: DropDownItem[];
}

export interface ProductAddRequest {
  userId: string;
  name: string;
  brand: string;
  model: string;
  price: number;
  salePrice: number | null;
  productDescription: string;
  category: string;
  featureProduct: boolean;
  quantity: number;
  options: ProductOption[];
  photos: PhotoAddRequest[];
}
export interface ProductUpdateRequest {
  id: string;
  userId: string;
  name: string;
  brand: string;
  model: string;
  price: number;
  salePrice: number | null;
  productDescription: string;
  category: string;
  featureProduct: boolean;
  quantity: number;
  options: ProductOption[];
  photos: PhotoAddRequest[];
}

export interface NewProductReview {
  userId?: string;
  itemId?: string;
  orderId?: string
  sellerId?: string;
  sellerRating?: number
  productComment?: string;
  productRating?: number;
}
