export interface PaymentMethod {
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
export interface UserPayoutResponse {
  onDate: string
  payoutMethod: string
  amount: number
}

export interface UserPayoutsResponse {
  balance: number
  totalEntities: number
  paymentMethod: PaymentMethod
  payouts: UserPayoutResponse[]
}
export interface UserSalesDetails {
  date: string
  revenue: number
}

export interface UserSalesInfoResponse {
  salesDetails: UserSalesDetails[]
  salesProducts: UserSalesProducts[]
}
export interface UserSalesProducts {
  productName: string
  quantity: number
  revenue: number
}
