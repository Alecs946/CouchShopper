export interface OrderItemOptionResponse {
  optionName: string;
  optionValue: string;
}

export interface OrderItemResponse {
  productId: string
  sellerId: string
  productName: string
  photoId: string
  imageBase64: string
  price: number
  quantity: number
  selectedOptions: OrderItemOptionResponse[]
  isApproved: boolean
  isDeclined: boolean,
}

export interface OrderOverviewResponse {
  orderId: string
  shippingType: string
  buyerFullName: string
  buyerPhone: string
  shippingAddress: string
  paymentMethodId: string
  orderStatus: number
  createdOn: Date
  orderItems: OrderItemResponse[],
  showConfirmation: boolean
  showReason: boolean
  declineReason: string
}
export interface OrdersOverviewResponse {
  totalEntities: number;
  offset: number
  orders: OrderOverviewResponse[];
}


export interface UserOrdersResponse {
  id: string;
  orderId: string
  sellerId: string
  productId: string
  customerId: string
  photoId: string
  productName: string
  quantity: number
  price: number
  rated: boolean
  imageBase64: string
  orderStatus: number
  orderStatusString: string
  declineReason: string
  selectedOptions: OrderItemOptionResponse[]
}

export interface OrderUserOrdersListResponse {
  totalEntities: number
  offset: number
  orders: UserOrdersResponse[]
}

export interface OrderInformation {
  ProductName: string;
  Rating: number;
  Price: number;
  SalePrice?: number | null;
  NumberOfReviews: number;
}
