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

export interface ProductCartResponseList {
  subtotal: number
  cartItems: CartProduct[]
}
export interface ShippingInfo {
  fullName: string;
  emailAddress: string;
  phoneNumber: string;
  country: string;
  city: string;
  zipCode: string;
  address: string;
}
