export interface SelectedOption {
  optionName: string;
  optionValue: string;
}

export interface CartProduct {
  productId: string
  productName: string;
  imageBase64: string;
  selectedOptions: SelectedOption[];
  sellerId: string;
  price: number;
  quantity: number;
  numberOfAvailableProducts?: number;
}

export interface OrderAddRequest {
  orderItems: CartProduct[]
  shippingType: string
  price: number
  buyerUsername: string
  buyerFullName: string
  buyerPhone: string
  shippingAddress: string
  shippingPrice: number
  paymentMethodId: string
}

export interface OrderStatusChangeRequest {
  orderId: string;
}

export interface OrderDeclineRequest {
  orderId: string,
  reason: string
}

