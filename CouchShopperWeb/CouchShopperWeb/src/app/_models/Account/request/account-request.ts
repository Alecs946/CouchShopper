export interface LoginForm {
  username: string;
  password: string;
}
export interface RegisterForm {
  fullName: string;
  email: string;
  username: string;
  password: string;
}
export interface AccountAddPaymentCreditCardRequest {
  username: string;
  cardName: number;
  cardNumber: string;
  nameOnCard: string;
  expiryDate: string;
  isPrimary: boolean;
}

export interface AccountAddPayPal {
  username: string
  email: string
  password: string
  isPrimary: boolean
}

export interface AccountPaymentMethodUpdateRequest {
  userId: string
  paymentId: string
}

export interface AccountPaymentMethodDeleteRequest {
  username: string
  cardId: string
}

export interface AccountProfilePictureUploadRequest {
  username: string;
  profilePicture: string;
}


