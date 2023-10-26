export interface AccountProfilePictureResponse {
  profilePicture: string
}

export interface AccountPaymentMethodResponse {
  id: string 
  cardNumber: string 
  cardName: string 
  nameOnCard: string 
  expiryDate: string 
  email: string 
  paymentMethodType: string 
  isCard: boolean
  isPrimary: boolean
}

export interface AccountPasswordUpdateRequest {
  username?: string;
  currentPassword: string;
  newPassword: string;
}

export interface AccountResponse {
  username: string;
  fullName: string;
  email: string;
  country: string;
  city: string;
  address: string;
  phone: string;
  zipCode: string;
}
