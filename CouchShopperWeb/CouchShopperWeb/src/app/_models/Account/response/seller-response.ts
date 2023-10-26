import { ProductByUserListResponse } from "../../_product/response/product-response";

export interface SellerInfoResponse {
  sellerName: string;
  imageBase64: string;
  memberSince: string;
}
export interface SellerDetailsResponse {
  sellerName: string;
  memberSince: string;
  imageBase64: string;
  totalSales: number;
  rating: number;
  numberOfReviews: number;
  products: ProductByUserListResponse
}

export interface SellerSalesProducts {
  productName: string
  quantity: number
  revenue: number
}
export interface SellerSalesDetails {
  date: string
  revenue: number
}

export interface SellerSalesInfoResponse {
  salesDetails: SellerSalesDetails[]
  salesProducts: SellerSalesProducts[]
}

export interface SellerPaymentMethodResponse {
  id: string;
  cardName: string;
  cardNumber: string;
  nameOnCard: string;
  expiryDate: string;
  email: string;
  paymentMethodType: string;
  isCard: boolean;
  isPrimary: boolean;

}
export interface SellerPayoutResponse {
  onDate: string
  payoutMethod: string
  amount: number
}

export interface SellerPayoutsResponseList {
  balance: number
  totalEntities: number
  paymentMethod: SellerPaymentMethodResponse
  payouts: SellerPayoutResponse[]
}
export interface Seller {
  sellerName: string;
  memberSince: string;
  imageBase64: string;
  totalSales: number;
  rating: number;
  numberOfReviews: number;
  products: ProductByUserListResponse
}
export interface SellerInfo {
  sellerName: string;
  imageBase64: string;
  memberSince: string;
}

